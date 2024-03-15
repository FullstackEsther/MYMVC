using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MYMVC.Models.BackGroundConfiguration;
using MYMVC.Models.MailBox;
using MYMVC.Models.Repository.Interface;
using NCrontab;

namespace MYMVC.Models.BackGroundTask
{
    public class BackgroundTask :BackgroundService
    {
        private readonly ReminderMailConfiguration _config;
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private readonly ILogger<BackgroundTask> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public BackgroundTask(IOptions<ReminderMailConfiguration> config, ILogger<BackgroundTask> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _config = config.Value;
            _schedule = CrontabSchedule.Parse(_config.CronExpression);
            _nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow);
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.UtcNow;
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var mail = scope.ServiceProvider.GetRequiredService<IMailMessage>();
                    var _meetingRepository = scope.ServiceProvider.GetRequiredService<IMeetingRepository>();
                    var _mentorRepository = scope.ServiceProvider.GetRequiredService<IMentorRepository>();
                    var _menteeRepository = scope.ServiceProvider.GetRequiredService<IMenteeRepository>();
                    var time = DateTime.Now;
                    var timeLimit = time.AddMinutes(5);
                    var meetings = await  _meetingRepository.GetMeetingByTimeAsync(x => x.DateAndTime >= time && x.DateAndTime <= timeLimit);
                    foreach (var item in meetings)
                    {
                        var mentor = await _mentorRepository.GetMentorAsync(a => a.Id == item.MentorId);
                        var mentee = await _menteeRepository.GetMenteeAsync(a => a.Id == item.MenteeId);
                        mail.SendEmailWhenItIsTime(mentor.User.Email);
                        mail.SendEmailWhenItIsTime(mentee.User.Email);

                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error occured reading Reminder Table in database.{ex.Message}");
                    _logger.LogError(ex, ex.Message);
                }
                _logger.LogInformation($"Background Hosted Service for {nameof(BackgroundTask)} is stopping");
                var timeSpan = _nextRun - now;
                await Task.Delay(timeSpan, stoppingToken);
                _nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow);

            }
        }
    }
}