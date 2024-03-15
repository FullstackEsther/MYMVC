using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MYMVC.Models.DTO;

namespace MYMVC.Models.Service.Interface
{
    public interface IMeetingService
    {
        public Task<BaseResponse<MeetingDto>> CreateAsync(MeetingRequestModel mentee);
        public Task<BaseResponse<IEnumerable<MeetingDto>>> GetAllScheduledMeetingAsync(string MentorId);
        public Task<BaseResponse<MeetingDto>> RescheduleMeeting(UpdateMeeting update);
        public Task<BaseResponse<IEnumerable<MeetingDto>>>  GetMeetingByTime();
        public Task<BaseResponse<MeetingDto>>  GetMeeting(string Id);
        public Task<BaseResponse<MeetingDto>> CancelMeeting(string MeetingId);
    }
}