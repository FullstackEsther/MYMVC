using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MYMVC.Models.DTO;

namespace MYMVC.Models.Service.Interface
{
    public interface IMenteeService
    {
        public Task<BaseResponse<MenteeDto>> CreateAsync(MenteeRequestModel mentee);
        public Task<BaseResponse<IEnumerable<MenteeDto>>> GetAllMenteeAsync();
        public Task<BaseResponse<MenteeDto>> GetMenteeAsync(string userName);
        public Task<BaseResponse<MenteeDto>> GetByIdAsync(string Id);
        public BaseResponse<MenteeDto> UpdateNewMentor(string menteeId, string mentorId);
        public BaseResponse<MenteeDto> UpdateMentee(MenteeUpdateRequestModel update);
        public void Remove(MenteeUpdateRequestModel mentor);
        
    }
}