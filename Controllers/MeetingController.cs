using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MYMVC.Models.DTO;
using MYMVC.Models.Entities;
using MYMVC.Models.Service.Interface;
using MYMVC.Models.ViewModel;
using Org.BouncyCastle.Asn1.Cmp;

namespace MYMVC.Controllers
{
    public class MeetingController : Controller
    {
        private readonly IMeetingService _meetingService;
        private readonly IMentorService _mentorService;

        public MeetingController(IMeetingService meetingService, IMentorService mentorService)
        {
            _meetingService = meetingService;
            _mentorService = mentorService;
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var mentorId = User.FindFirst("mentorId");
            var assignedMentees = await _mentorService.GetAllAssignedMenteeAsync(mentorId.Value);
            var meetingViewModel = new CreateMeetingViewmodel
            {
                Mentee = assignedMentees.Data.Select(x => new SelectListItem
                {
                    Text = x.UserName,
                    Value = x.Id
                })
            };
            return View(meetingViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateMeetingViewmodel viewmodel)
        {
            DateOnly date = viewmodel.Date;
            if (new DateTime(date.Year, date.Month, date.Day) <= DateTime.Now)
            {
                ViewData["Error"] = "Invalid date";
                return View(viewmodel);
            }
            var meetingRequest = new MeetingRequestModel
            {
                DateAndTime = new DateTime(date.Year, date.Month, date.Day) + viewmodel.Time,
                MentorId = User.FindFirst("mentorId").Value,
                MenteeId = viewmodel.SelectedMentee
            };
            var meeting = await _meetingService.CreateAsync(meetingRequest);
            if (meeting.Status)
            {
                return RedirectToAction("MentorDashboard", "user");
            }
            return View(viewmodel);
        }

        public async Task<IActionResult> GetAllScheduledMeetings(string id)
        {
             var Id = User.FindFirst("mentorId")?.Value;
            var meeting = await _meetingService.GetAllScheduledMeetingAsync(Id);
            if (meeting.Status)
            {
                var viewmodel = meeting.Data.Select(x => new GetMeetingViewModel
                {
                    DateAndTime = x.DateAndTime,
                    Id = x.Id,
                    MenteeUserName = x.MenteeUserName,
                    MentorUserName = x.MentorUserName
                });
                return View(viewmodel);
            }
            return View("Create");
        }

        [HttpGet]
        public async Task<IActionResult> RescheduleMeeting(string id)
        {
            var meeting = await _meetingService.GetMeeting(id);
            if (meeting.Status)
            {
                var viewModel = new EditMeetingViewModel
                {
                    Date = DateOnly.Parse(meeting.Data.DateAndTime.ToShortDateString()),
                    Time = meeting.Data.DateAndTime.TimeOfDay,
                };
                return View(viewModel);
            }
            return View(null);
        }
        [HttpPost]
        public IActionResult RescheduleMeeting(EditMeetingViewModel viewModel)
        {
            DateOnly date = viewModel.Date;
            if (new DateTime(date.Year, date.Month, date.Day) <= DateTime.Now)
            {
                ViewData["Error"] = "Invalid date";
                return View(viewModel);
            }
            var update = new UpdateMeeting
            {
                DateAndTime = new DateTime(date.Year, date.Month, date.Day) + viewModel.Time,
                Id = viewModel.Id

            };
            var updatedMeeting = _meetingService.RescheduleMeeting(update);
            if (updatedMeeting.Result.Status)
            {
                return RedirectToAction("MentorDashboard", "User");
            }
            return View();
        }
        [HttpGet]
        public IActionResult CancelMeeting(string id)
        {
            var getMeeting = _meetingService.GetMeeting(id);
            if (getMeeting != null)
            {
                var viewModel = new GetMeetingViewModel
                {
                    DateAndTime = getMeeting.Result.Data.DateAndTime,
                    Id = getMeeting.Result.Data.Id,
                    MenteeUserName = getMeeting.Result.Data.MenteeUserName,
                    MentorUserName = getMeeting.Result.Data.MentorUserName
                };
                return View(viewModel);
            }
            ViewData["Error"] = getMeeting.Result.Message;
            return View();
        }

        [HttpPost]
        public IActionResult CancelMeeting(GetMeetingViewModel viewModel)
        {
           var cancel= _meetingService.CancelMeeting(viewModel.Id);
            if (cancel != null)
            {
                return RedirectToAction("GetAllScheduledMeetings", new { id = viewModel.Id });
            }
             ViewData["Cancel"] = cancel.Result.Message;
             return View();
        }

    }
}