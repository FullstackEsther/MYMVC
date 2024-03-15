using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MYMVC.Models.DTO;
using MYMVC.Models.Entities;

namespace MYMVC.Models.Service.Interface
{
    public interface IMentorService
    {
        public Task<BaseResponse<MentorDto>> CreateAsync(MentorRequestModel mentor);
        public Task<BaseResponse<IEnumerable<MentorDto>>> GetAllMentorAsync();
        public Task<BaseResponse<IEnumerable<MentorDto>>> GetAllMentorInCategoryAsync(string CategoryId);
        public Task<BaseResponse<IEnumerable<MenteeDto>>> GetAllAssignedMenteeAsync(string mentorId);
        public Task<BaseResponse<MentorDto>> GetMentorAsync(string userName);
        public Task<BaseResponse<MentorDto>> GetIdAsync(string Id);
        public BaseResponse<MentorDto> UpdateMentorProfile(MentorUpdateRequestModel update);
        public void Remove(MentorUpdateRequestModel mentor);
    }
}