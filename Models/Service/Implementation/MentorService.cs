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
    public class MentorService : IMentorService
    {
        private readonly IMentorRepository _mentorRepository;
        private readonly IMenteeRepository _menteeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MentorService(IMentorRepository mentorRepository, IMenteeRepository menteeRepository, IUserRepository userRepository, IProfileRepository profileRepository, IRoleRepository roleRepository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _mentorRepository = mentorRepository;
            _menteeRepository = menteeRepository;
            _userRepository = userRepository;
            _profileRepository = profileRepository;
            _roleRepository = roleRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<MentorDto>> CreateAsync(MentorRequestModel mentor)
        {
            var exist = _userRepository.Exist(x => x.UserName == mentor.UserName || x.Email == mentor.Email);
            if (exist)
            {
                return new BaseResponse<MentorDto>
                {
                    Data = null,
                    Message = "already exist",
                    Status = false
                };
            }
            var role = await _roleRepository.GetRoleAsync(x => x.RoleName == "Mentor");
            var category = await _categoryRepository.GetCategoryAsync(x => x.Id == mentor.CategoryId);
            var user = new User
            {
                Email = mentor.Email,
                Password = mentor.Password,
                UserName = mentor.UserName,
                RoleId = role.Id,
                // Role = role,
            };
            await _userRepository.CreateAsync(user);
            var profile = new Profile
            {
                Address = mentor.Address,
                Age = mentor.Age,
                FirstName = mentor.FirstName,
                LastName = mentor.LastName,
                PhoneNumber = mentor.PhoneNumber,
                UserId = user.Id,
                User = user
            };
            await _profileRepository.CreateAsync(profile);

            var mentorObj = new Mentor
            {
                CategoryId = mentor.CategoryId,
                YearsOfExperience = mentor.YearsOfExperience,
                UserId = user.Id,
                IsAvailable = true,
                User = user,
                Category = category
            };
            await _mentorRepository.CreateAsync(mentorObj);
            await _unitOfWork.SaveAsync();
            return new BaseResponse<MentorDto>
            {
                Status = true,
                Message = "successfully created",
                Data = new MentorDto
                {
                    Address = profile.Address,
                    Age = profile.Age,
                    CategoryName = mentorObj.Category.CategoryName,
                    Email = user.Email,
                    FirstName = profile.FirstName,
                    Id = mentorObj.Id,
                    IsAvailable = mentorObj.IsAvailable,
                    LastName = profile.LastName,
                    PhoneNumber = profile.PhoneNumber,
                    UserName = user.UserName,
                    YearsOfExperience = mentor.YearsOfExperience,
                }
            };
        }

        public async Task<BaseResponse<IEnumerable<MentorDto>>> GetAllMentorAsync()
        {
            var allMentors = await _mentorRepository.GetAllMentorAsync();
            if (allMentors != null)
            {
                return new BaseResponse<IEnumerable<MentorDto>>
                {
                    Status = true,
                    Message = "found",
                    Data = allMentors.Select(x => new MentorDto
                    {
                        UserName = x.User.UserName,
                        Address = x.User.Profile.Address,
                        Age = x.User.Profile.Age,
                        CategoryName = x.Category.CategoryName,
                        YearsOfExperience = x.YearsOfExperience,
                        Email = x.User.Email,
                        FirstName = x.User.Profile.FirstName,
                        Id = x.Id,
                        LastName = x.User.Profile.LastName,
                        IsAvailable = x.IsAvailable,
                        PhoneNumber = x.User.Profile.PhoneNumber
                    }).ToList()
                };
            }
            return new BaseResponse<IEnumerable<MentorDto>>
            {
                Data = null,
                Message = "NotFound",
                Status = false
            };
        }

        public async Task<BaseResponse<MentorDto>> GetMentorAsync(string userName)
        {
            var getMentor = await _mentorRepository.GetMentorAsync(x => x.User.UserName == userName);
            if (getMentor != null)
            {
                return new BaseResponse<MentorDto>
                {
                    Status = true,
                    Message = "Found",
                    Data = new MentorDto
                    {
                        Address = getMentor.User.Profile.Address,
                        UserName = getMentor.User.UserName,
                        Age = getMentor.User.Profile.Age,
                        CategoryName = getMentor.Category.CategoryName,
                        YearsOfExperience = getMentor.YearsOfExperience,
                        Email = getMentor.User.Email,
                        FirstName = getMentor.User.Profile.FirstName,
                        Id = getMentor.Id,
                        LastName = getMentor.User.Profile.LastName,
                        IsAvailable = getMentor.IsAvailable,
                        PhoneNumber = getMentor.User.Profile.PhoneNumber
                    }
                };
            }
            return new BaseResponse<MentorDto>
            {
                Data = null,
                Message = " Not Found",
                Status = false
            };
        }

        public void Remove(MentorUpdateRequestModel mentor)
        {
            var getUser = _userRepository.GetUserAsync(x => x.Email == mentor.Email);
            _userRepository.Remove(getUser.Result);
            var getProfile = _profileRepository.GetProfileAsync(x => x.User.Email == mentor.Email);
            _profileRepository.Remove(getProfile.Result);
            var getMentor = _mentorRepository.GetMentorAsync(x => x.User.Email == mentor.Email);
            _mentorRepository.Remove(getMentor.Result);
            _unitOfWork.SaveAsync();
        }

        public BaseResponse<MentorDto> UpdateMentorProfile(MentorUpdateRequestModel update)
        {
            var existingProfile = _profileRepository.GetProfileAsync(x => x.User.Email == update.Email);
            var existingMentor = _mentorRepository.GetMentorAsync(x => x.User.Email == update.Email);
            if (existingProfile != null)
            {
                existingProfile.Result.Address = update.Address;
                existingProfile.Result.PhoneNumber = update.PhoneNumber;
                existingProfile.Result.LastName = update.LastName;
                existingProfile.Result.FirstName = update.FirstName;
                existingProfile.Result.Age = update.Age;
                existingMentor.Result.YearsOfExperience = update.YearsOfExperience;
                _mentorRepository.Update(existingMentor.Result);
                _profileRepository.Update(existingProfile.Result);
                _unitOfWork.SaveAsync();
                return new BaseResponse<MentorDto>
                {
                    Status = true,
                    Message = "updated",
                    Data = new MentorDto
                    {
                        Address = existingProfile.Result.Address,
                        Age = update.Age,
                        FirstName = existingProfile.Result.FirstName,
                        LastName = existingProfile.Result.LastName,
                        PhoneNumber = existingProfile.Result.PhoneNumber,
                        CategoryName = existingMentor.Result.Category.CategoryName,
                        Email = existingMentor.Result.User.Email,
                        YearsOfExperience = existingMentor.Result.YearsOfExperience,
                        UserName = existingMentor.Result.User.UserName,
                    }
                };
            }
            return new BaseResponse<MentorDto>
            {
                Data = null,
                Message = "Not Found!",
                Status = false
            };
        }

        public async Task<BaseResponse<MentorDto>> GetIdAsync(string Id)
        {
            var getMentor = await _mentorRepository.GetMentorAsync(x => x.Id == Id);
            if (getMentor != null)
            {
                return new BaseResponse<MentorDto>
                {
                    Status = true,
                    Message = "successful",
                    Data = new MentorDto
                    {
                        Address = getMentor.User.Profile.Address,
                        UserName = getMentor.User.UserName,
                        Age = getMentor.User.Profile.Age,
                        CategoryName = getMentor.Category.CategoryName,
                        YearsOfExperience = getMentor.YearsOfExperience,
                        Email = getMentor.User.Email,
                        FirstName = getMentor.User.Profile.FirstName,
                        Id = getMentor.Id,
                        LastName = getMentor.User.Profile.LastName,
                        IsAvailable = getMentor.IsAvailable,
                        PhoneNumber = getMentor.User.Profile.PhoneNumber
                    }
                };
            }
            return new BaseResponse<MentorDto>
            {
                Data = null,
                Message = " Not Found",
                Status = false
            };
        }

        public async Task<BaseResponse<IEnumerable<MentorDto>>> GetAllMentorInCategoryAsync(string categoryId)
        {

            var allMentors = await _mentorRepository.GetAllMentorAsync(x => x.CategoryId == categoryId);
            if (allMentors != null)
            {
                return new BaseResponse<IEnumerable<MentorDto>>
                {
                    Status = true,
                    Message = "found",
                    Data = allMentors.Select(x => new MentorDto
                    {
                        UserName = x.User.UserName,
                        Address = x.User.Profile.Address,
                        Age = x.User.Profile.Age,
                        CategoryName = x.Category.CategoryName,
                        YearsOfExperience = x.YearsOfExperience,
                        Email = x.User.Email,
                        FirstName = x.User.Profile.FirstName,
                        Id = x.Id,
                        LastName = x.User.Profile.LastName,
                        IsAvailable = x.IsAvailable,
                        PhoneNumber = x.User.Profile.PhoneNumber
                    }).ToList()
                };
            }
            return new BaseResponse<IEnumerable<MentorDto>>
            {
                Data = null,
                Message = "NotFound",
                Status = false
            };
        }

        public async Task<BaseResponse<IEnumerable<MenteeDto>>> GetAllAssignedMenteeAsync(string mentorId)
        {
            var getMentees = await _menteeRepository.GetAllMenteeAsync(x => x.MentorId == mentorId);
            if (getMentees != null)
            {
                return new BaseResponse<IEnumerable<MenteeDto>>
                {
                    Status = true,
                    Message = "successful",
                    Data = getMentees.Select(x => new MenteeDto
                    {
                        Email = x.User.Email,
                        UserName = x.User.UserName,
                        FirstName = x.User.Profile.FirstName,
                        PhoneNumber = x.User.Profile.PhoneNumber,
                        Address = x.User.Profile.Address,
                         MentorId = x.MentorId,
                          Id = x.Id
                    })
                };
            }
            return new BaseResponse<IEnumerable<MenteeDto>>
            {
                Data = null,
                Status = false,
                Message = "not found"
            };
        }
    }
}