using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MYMVC.Models.DTO;

namespace MYMVC.Models.Service.Interface
{
    public interface IUserService
    {
         public Task<BaseResponse<IEnumerable<UserDto>>> GetAllUserAsync();
        public Task<BaseResponse<UserDto>> GetUserMenteeAsync(string Id);
        public Task<BaseResponse<UserDto>> GetUserMentorAsync(string Id);
        public Task<BaseResponse<UserDto>> GetByUserNameAsync(string userName);
        public Task<BaseResponse<UserDto>> LoginAsync(string userName, string password);
    }
}