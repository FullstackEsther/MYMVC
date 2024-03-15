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
    public class MenteeService : IMenteeService
    {
        private readonly IMenteeRepository _menteeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMentorRepository _mentorRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ICategoryRepository _categoryRepository;

        public MenteeService(IMenteeRepository menteeRepository, IUserRepository userRepository, IProfileRepository profileRepository, IUnitOfWork unitOfWork, IMentorRepository mentorRepository, IRoleRepository roleRepository, ICategoryRepository categoryRepository)
        {
            _menteeRepository = menteeRepository;
            _userRepository = userRepository;
            _profileRepository = profileRepository;
            _unitOfWork = unitOfWork;
            _mentorRepository = mentorRepository;
            _roleRepository = roleRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<BaseResponse<MenteeDto>> CreateAsync(MenteeRequestModel mentee)
        {
            var exist = _userRepository.Exist(x => x.UserName == mentee.UserName);
            if (exist)
            {
                return new BaseResponse<MenteeDto>
                {
                    Data = null,
                    Message = "Already exists",
                    Status = false
                };
            }
            var role = await _roleRepository.GetRoleAsync(x => x.RoleName == "Mentee");
            var user = new User
            {
                Email = mentee.Email,
                Password = mentee.Password,
                UserName = mentee.UserName,
                RoleId = role.Id,
                Role = role
            };
            await _userRepository.CreateAsync(user);
            var profile = new Profile
            {
                Address = mentee.Address,
                Age = mentee.Age,
                FirstName = mentee.FirstName,
                LastName = mentee.LastName,
                PhoneNumber = mentee.PhoneNumber,
                UserId = user.Id,
                User = user
            };
            await _profileRepository.CreateAsync(profile);
            var category = await _categoryRepository.GetCategoryAsync(x => x.Id == mentee.CategoryId);
            var newMentee = new Mentee
            {
                CategoryId = mentee.CategoryId,
                UserId = user.Id,
                User = user,
                Category = category
            };
            await _menteeRepository.CreateAsync(newMentee);
            await _unitOfWork.SaveAsync();
            return new BaseResponse<MenteeDto>
            {
                Status = true,
                Message = "created successfully",
                Data = new MenteeDto
                {
                    Address = profile.Address,
                    Age = profile.Age,
                    CategoryName = newMentee.Category.CategoryName,
                    Email = user.Email,
                    FirstName = profile.FirstName,
                    Id = newMentee.Id,
                    LastName = profile.LastName,
                    UserName = user.UserName,
                    PhoneNumber = profile.PhoneNumber
                }
            };
        }

        public async Task<BaseResponse<IEnumerable<MenteeDto>>> GetAllMenteeAsync()
        {
            var mentees = await _menteeRepository.GetAllMenteeAsync();
            if (mentees == null)
            {
                return new BaseResponse<IEnumerable<MenteeDto>>
                {
                    Data = null,
                    Message = " No mentees available",
                    Status = false
                };
            }
            return new BaseResponse<IEnumerable<MenteeDto>>
            {
                Status = true,
                Message = "Successful",
                Data = mentees.Select(x => new MenteeDto
                {
                    Address = x.User.Profile.Address,
                    Age = x.User.Profile.Age,
                    CategoryName = x.Category.CategoryName,
                    Email = x.User.Email,
                    FirstName = x.User.Profile.FirstName,
                    LastName = x.User.Profile.LastName,
                    Id = x.Id,
                    PhoneNumber = x.User.Profile.LastName,
                    UserName = x.User.UserName,
                }).ToList()
            };
        }

        public async Task<BaseResponse<MenteeDto>> GetByIdAsync(string Id)
        {
            var getMentee = await _menteeRepository.GetMenteeAsync(x => x.Id == Id);
            if (getMentee != null)
            {
                return new BaseResponse<MenteeDto>
                {
                    Status = true,
                    Message = "successful",
                    Data = new MenteeDto
                    {
                        Address = getMentee.User.UserName,
                        Age = getMentee.User.Profile.Age,
                        CategoryName = getMentee.Category.CategoryName,
                        Email = getMentee.User.Email,
                        FirstName = getMentee.User.Profile.FirstName,
                        Id = getMentee.Id,
                        LastName = getMentee.User.Profile.LastName,
                        PhoneNumber = getMentee.User.Profile.PhoneNumber,
                        UserName = getMentee.User.UserName,
                    }
                };
            }
            return new BaseResponse<MenteeDto>
            {
                Data = null,
                Message = "Not Found",
                Status = false
            };
        }

        public async Task<BaseResponse<MenteeDto>> GetMenteeAsync(string userName)
        {
            var getMentee = await _menteeRepository.GetMenteeAsync(x => x.User.UserName == userName);
            if (getMentee != null)
            {
                return new BaseResponse<MenteeDto>
                {
                    Status = true,
                    Message = "successful",
                    Data = new MenteeDto
                    {
                        Address = getMentee.User.UserName,
                        Age = getMentee.User.Profile.Age,
                        CategoryName = getMentee.Category.CategoryName,
                        Email = getMentee.User.Email,
                        FirstName = getMentee.User.Profile.FirstName,
                        Id = getMentee.Id,
                        LastName = getMentee.User.Profile.LastName,
                        PhoneNumber = getMentee.User.Profile.PhoneNumber,
                        UserName = getMentee.User.UserName
                    }
                };
            }
            return new BaseResponse<MenteeDto>
            {
                Data = null,
                Message = "Not Found",
                Status = false
            };
        }

        public void Remove(MenteeUpdateRequestModel mentee)
        {
            var getUser = _userRepository.GetUserAsync(x => x.UserName == mentee.UserName);
            _userRepository.Remove(getUser.Result);
            var getProfile = _profileRepository.GetProfileAsync(x => x.User.UserName == mentee.UserName);
            _profileRepository.Remove(getProfile.Result);
            var getMentor = _menteeRepository.GetMenteeAsync(x => x.User.UserName == mentee.UserName);
            _menteeRepository.Remove(getMentor.Result);
            _unitOfWork.SaveAsync();
        }

        public BaseResponse<MenteeDto> UpdateMentee(MenteeUpdateRequestModel update)
        {
            var existingProfile = _profileRepository.GetProfileAsync(x => x.User.Email == update.Email);
            if (existingProfile != null)
            {
                existingProfile.Result.FirstName = update.FirstName;
                existingProfile.Result.Address = update.Address;
                existingProfile.Result.Age = update.Age;
                existingProfile.Result.LastName = update.LastName;
                existingProfile.Result.PhoneNumber = update.PhoneNumber;
                existingProfile.Result.User.UserName = update.UserName;
                _profileRepository.Update(existingProfile.Result);
                _unitOfWork.SaveAsync();
                return new BaseResponse<MenteeDto>
                {
                    Status = true,
                    Message = "sucessfully updated",
                    Data = new MenteeDto
                    {
                        Address = existingProfile.Result.Address,
                        Age = existingProfile.Result.Age,
                        FirstName = existingProfile.Result.FirstName,
                        LastName = existingProfile.Result.LastName,
                        PhoneNumber = existingProfile.Result.PhoneNumber,
                        Email = existingProfile.Result.User.Email,
                        UserName = existingProfile.Result.User.UserName,
                    }
                };
            }
            return new BaseResponse<MenteeDto>
            {
                Data = null,
                Message = "not updated",
                Status = false
            };
        }

        public  BaseResponse<MenteeDto> UpdateNewMentor(string menteeId, string mentorId)
        {
            var mentee = _menteeRepository.GetMenteeAsync(x => x.Id == menteeId);
            if (mentee != null)
            {
                mentee.Result.MentorId = mentorId;
                _menteeRepository.Update(mentee.Result);
                _unitOfWork.SaveAsync();
                return new BaseResponse<MenteeDto>
                {
                    Status = true,
                    Message = "updated",
                    Data = new MenteeDto
                    {
                        Id = mentee.Result.Id,
                        MentorId = mentee.Result.MentorId,

                    }
                };
            }
            return new BaseResponse<MenteeDto>
            {
                 Status = false,
                  Data = null,
                   Message = "not updated"
            };

        }



        // public BaseResponse<ProfileDto> UpdateMenteeProfile(UpdateProfileRequestModel update)
        // {
        //     var existingProfile = _profileRepository.GetProfileAsync(x => x.User.UserName == update.UserName);
        //     if (existingProfile != null)
        //     {
        //         existingProfile.Result.FirstName = update.FirstName;
        //         existingProfile.Result.Address = update.Address;
        //         existingProfile.Result.Age = update.Age;
        //         existingProfile.Result.LastName = update.LastName;
        //         existingProfile.Result.PhoneNumber = update.PhoneNumber;
        //         return new BaseResponse<ProfileDto>
        //         {
        //             Status = false,
        //             Message = "update successfully",
        //             Data = new ProfileDto
        //             {
        //                 Address = existingProfile.Result.Address,
        //                 Age = existingProfile.Result.Age,
        //                 FirstName = existingProfile.Result.FirstName,
        //                 LastName = existingProfile.Result.LastName,
        //                 PhoneNumber = existingProfile.Result.PhoneNumber
        //             }
        //         };
        //     }
        //     return new BaseResponse<ProfileDto>
        //     {
        //         Data = null,
        //         Message = "not updated",
        //         Status = false
        //     };
        // }
        // private async Task<List<Mentor>> GetMentorsInCategory(string categoryId)
        // {
        //     var mentorsInCategory = await _mentorRepository.GetAllMentorAsync(x => x.CategoryId ==categoryId && x.IsAvailable == true);
        //     int numberOfMentees = mentorsInCategory.Min(x => x.Mentees.Count());
        //     var mentorsWithLowestMentees = mentorsInCategory.Where(x => x.Mentees.Count() == numberOfMentees).ToList();
        //     return mentorsWithLowestMentees;
        // }
        // private string AssignMentor(string categoryId)
        // {
        //     var assignableMentors = GetMentorsInCategory(categoryId);
        //     var mentorAtIndex = new Random().Next(0,assignableMentors.Result.Count());
        //     var assignedMentor = assignableMentors.Result[mentorAtIndex];
        //     return assignedMentor.Id;
        // }

        // public Task<BaseResponse<MentorDto>> ChangeMentorAsync(string Id)
        // {
        //     throw new NotImplementedException();
        // }
    }
}