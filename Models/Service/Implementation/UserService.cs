using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MYMVC.Models.DTO;
using MYMVC.Models.Repository.Implementation;
using MYMVC.Models.Repository.Interface;
using MYMVC.Models.Service.Interface;

namespace MYMVC.Models.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<BaseResponse<IEnumerable<UserDto>>> GetAllUserAsync()
        {
            var getAllUser = await _userRepository.GetAllUserAsync();
            if (getAllUser != null)
            {
                return new BaseResponse<IEnumerable<UserDto>>
                {
                    Message = "available",
                    Status = true,
                    Data = getAllUser.Select(x => new UserDto
                    {
                        Email = x.Email,
                        Id = x.Id,
                        UserName = x.UserName
                    })
                };
            }
            return new BaseResponse<IEnumerable<UserDto>>
            {
                Data = null,
                Message = "not available",
                Status = false
            };
        }

        public async Task<BaseResponse<UserDto>> GetUserAsync(string id)
        {
            var user = await _userRepository.GetUserAsync(x => x.Mentee.Id == id);
            if (user != null)
            {
                return new BaseResponse<UserDto>
                {
                    Message = "successful",
                    Status = true,
                    Data = new UserDto
                    {
                        Email = user.Email,
                        Id = user.Id,
                        UserName = user.UserName
                    }
                };
            }
            return new BaseResponse<UserDto>
            {
                Data = null,
                Message = "Not Found",
                Status = false
            };
        }

        public async Task<BaseResponse<UserDto>> GetByUserNameAsync(string userName)
        {
            var user = await _userRepository.GetUserAsync(x => x.UserName == userName);
            if (user != null)
            {
                return new BaseResponse<UserDto>
                {
                    Message = "successful",
                    Status = true,
                    Data = new UserDto
                    {
                        Email = user.Email,
                        Id = user.Id,
                        UserName = user.UserName
                    }
                };
            }
            return new BaseResponse<UserDto>
            {
                Data = null,
                Message = "Not Found",
                Status = false
            };

        }

        public async Task<BaseResponse<UserDto>> LoginAsync(string userName, string password)
        {
            var user = await _userRepository.GetUserAsync(x => x.UserName == userName && x.Password == password);
            if (user != null)
            {
                return new BaseResponse<UserDto>
                {
                    Message = "successful",
                    Status = true,
                    Data = new UserDto
                    {
                        Email = user.Email,
                        Id = user.Id,
                        UserName = user.UserName,
                        RoleName = user.Role.RoleName,
                        CategoryId = user?.Mentee?.CategoryId,
                        MenteeId = user?.Mentee?.Id,
                        MentorId = user?.Mentor?.Id
                    }
                };
            }
            return new BaseResponse<UserDto>
            {
                Data = null,
                Message = "Not Found",
                Status = false
            };
        }

        public async Task<BaseResponse<UserDto>> GetUserMenteeAsync(string id)
        {
             var user = await _userRepository.GetUserAsync(x => x.Mentee.Id == id);
            if (user != null)
            {
                return new BaseResponse<UserDto>
                {
                    Message = "successful",
                    Status = true,
                    Data = new UserDto
                    {
                        Email = user.Email,
                        Id = user.Id,
                        UserName = user.UserName
                    }
                };
            }
            return new BaseResponse<UserDto>
            {
                Data = null,
                Message = "Not Found",
                Status = false
            };
        }

        public async Task<BaseResponse<UserDto>> GetUserMentorAsync(string id)
        {
             var user = await _userRepository.GetUserAsync(x => x.Mentor.Id == id);
            if (user != null)
            {
                return new BaseResponse<UserDto>
                {
                    Message = "successful",
                    Status = true,
                    Data = new UserDto
                    {
                        Email = user.Email,
                        Id = user.Id,
                        UserName = user.UserName
                    }
                };
            }
            return new BaseResponse<UserDto>
            {
                Data = null,
                Message = "Not Found",
                Status = false
            };
        }
    }


}
