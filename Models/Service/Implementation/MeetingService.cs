using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MYMVC.Models.DTO;
using MYMVC.Models.Entities;
using MYMVC.Models.Repository.Interface;
using MYMVC.Models.Service.Interface;

namespace MYMVC.Models.Service.Implementation
{
    public class MeetingService : IMeetingService
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MeetingService(IMeetingRepository meetingRepository, IUnitOfWork unitOfWork)
        {
            _meetingRepository = meetingRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<MeetingDto>> CancelMeeting(string MeetingId)
        {
            var meeting = await _meetingRepository.GetMeetingAsync(x => x.Id == MeetingId);
            if (meeting != null)
            {
                _meetingRepository.Remove(meeting);
                await _unitOfWork.SaveAsync();
                return new BaseResponse<MeetingDto>
                {
                    Status = true,
                    Message = "Deleted",
                    Data = new MeetingDto
                    {
                        DateAndTime = meeting.DateAndTime,
                        Id = meeting.Id,
                        MenteeUserName = meeting.Mentees.User.UserName,
                        MentorUserName = meeting.Mentor.User.UserName
                    }
                };
            }
            return new BaseResponse<MeetingDto>
            {
                Data = null,
                Message = "Unable to Delete",
                Status = false
            };

        }

        public async Task<BaseResponse<MeetingDto>> CreateAsync(MeetingRequestModel meeting)
        {
            var exist = _meetingRepository.ExistAsync(x => x.MentorId == meeting.MentorId && x.MenteeId == meeting.MenteeId);
            if (exist)
            {
                return new BaseResponse<MeetingDto>
                {
                    Status = false,
                    Data = null,
                    Message = "Meeting Already exists"
                };
            }
            var newMeeeting = new Meeting
            {
                DateAndTime = meeting.DateAndTime,
                MenteeId = meeting.MenteeId,
                MentorId = meeting.MentorId
            };
            await _meetingRepository.CreateAsync(newMeeeting);
            await _unitOfWork.SaveAsync();
            return new BaseResponse<MeetingDto>
            {
                Status = true,
                Message = "created successfully",
                Data = new MeetingDto
                {
                    DateAndTime = newMeeeting.DateAndTime,
                    // MenteeUserName = newMeeeting.Mentees.User.UserName,
                    // MentorUserName = newMeeeting.Mentor.User.UserName
                }
            };
        }

        public async Task<BaseResponse<IEnumerable<MeetingDto>>> GetAllScheduledMeetingAsync(string mentorId)
        {
            var getMeeting = await _meetingRepository.GetAllMeetingAsync(x => x.MentorId == mentorId);
            if (getMeeting != null)
            {
                return new BaseResponse<IEnumerable<MeetingDto>>
                {
                    Status = true,
                    Message = "successful",
                    Data = getMeeting.Select(x => new MeetingDto
                    {
                        DateAndTime = x.DateAndTime,
                        Id = x.Id,
                        MenteeUserName = x.Mentor.User.UserName,
                        MentorUserName = x.Mentees.User.UserName
                    })
                };
            }
            return new BaseResponse<IEnumerable<MeetingDto>>
            {
                Data = null,
                Message = "not found",
                Status = false
            };
        }

        public async Task<BaseResponse<MeetingDto>> GetMeeting(string Id)
        {
            var meeting = await _meetingRepository.GetMeetingAsync(x => x.Id == Id);
            if (meeting != null)
            {
                return new BaseResponse<MeetingDto>
                {
                    Status = true,
                    Message = "successful",
                    Data = new MeetingDto
                    {
                        DateAndTime = meeting.DateAndTime,
                        Id = meeting.Id,
                        MenteeUserName = meeting.Mentees.User.UserName,
                        MentorUserName = meeting.Mentor.User.UserName
                    }
                };
            }
            return new BaseResponse<MeetingDto>
            {
                Data = null,
                Message = "not found",
                Status = false
            };
        }

        public async Task<BaseResponse<IEnumerable<MeetingDto>>> GetMeetingByTime()
        {
            var time = DateTime.Now;
            var timeLimit = time.AddMinutes(5);
            var meetings = await _meetingRepository.GetMeetingByTimeAsync(x => x.DateAndTime >= time && x.DateAndTime <= timeLimit);
            if (meetings != null)
            {
                return new BaseResponse<IEnumerable<MeetingDto>>
                {
                    Status = true,
                    Message = "successful",
                    Data = meetings.Select(x => new MeetingDto
                    {
                        DateAndTime = x.DateAndTime,
                        Id = x.Id,
                        MenteeUserName = x.Mentor.User.UserName,
                        MentorUserName = x.Mentees.User.UserName
                    })
                };
            }
            return new BaseResponse<IEnumerable<MeetingDto>>
            {
                Data = null,
                Message = "not found",
                Status = false
            };
        }

        public async Task<BaseResponse<MeetingDto>> RescheduleMeeting(UpdateMeeting update)
        {
            var existingMeeting = await _meetingRepository.GetMeetingAsync(x => x.Id == update.Id);
            if (existingMeeting != null)
            {
                existingMeeting.DateAndTime = update.DateAndTime;
                _meetingRepository.Update(existingMeeting);
                await _unitOfWork.SaveAsync();
                return new BaseResponse<MeetingDto>
                {
                    Status = true,
                    Message = "updated",
                    Data = new MeetingDto
                    {
                        DateAndTime = existingMeeting.DateAndTime,
                        Id = existingMeeting.Id,
                        MenteeUserName = existingMeeting.Mentees.User.UserName,
                        MentorUserName = existingMeeting.Mentor.User.UserName
                    }
                };
            }
            return new BaseResponse<MeetingDto>
            {
                Data = null,
                Message = "not found",
                Status = false
            };

        }
    }
}