using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using MYMVC.Models.DTO;
using MYMVC.Models.Entities;
using MYMVC.Models.Service.Interface;
using MYMVC.Models.ViewModel;

namespace MYMVC.Controllers
{
    public class MentorController : Controller
    {
        private readonly IMentorService _mentorService;
        private readonly IMenteeService _menteeService;
        private readonly ICategoryService _categoryService;      
         public MentorController(IMentorService mentorService, ICategoryService categoryService, IMenteeService menteeService)
        {
            _mentorService = mentorService;
            _menteeService = menteeService;
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllCategoryAsync();
            var model = new CreateMentorViewModel
            {
                Category = categories.Data.Select(x => new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.Id
                })
            };
            return View(model);
        }

        public async Task<IActionResult> Create(CreateMentorViewModel viewModel)
        {
            var mentorRequestModel = new MentorRequestModel
            {
                Address = viewModel.Address,
                Age = viewModel.Age,
                Email = viewModel.Email,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Password = viewModel.Password,
                PhoneNumber = viewModel.PhoneNumber,
                UserName = viewModel.UserName,
                YearsOfExperience = viewModel.YearsOfExperience,
                CategoryId = viewModel.SelectedCategory
            };
            // if (ModelState.IsValid)
            // {
            var createdMentor = await _mentorService.CreateAsync(mentorRequestModel);
            if (createdMentor.Status)
            {
                return RedirectToAction("index", "home");
            }
            // }
            return RedirectToAction("create");
        }
        [HttpGet]
        public async Task<IActionResult> GetMentor()
        {
            var id = User.FindFirst("mentorId")?.Value;
            var mentor = await _mentorService.GetIdAsync(id);
            if (mentor.Status)
            {
                var mentorViewModel = new GetMentorViewModel
                {
                    Address = mentor.Data.Address,
                    Age = mentor.Data.Age,
                    CategoryName = mentor.Data.CategoryName,
                    Email = mentor.Data.Email,
                    FirstName = mentor.Data.FirstName,
                    Id = mentor.Data.Id,
                    IsAvailable = mentor.Data.IsAvailable,
                    LastName = mentor.Data.LastName,
                    Meetings = mentor.Data.Meetings,
                    Mentees = mentor.Data.Mentees,
                    PhoneNumber = mentor.Data.PhoneNumber,
                    UserName = mentor.Data.UserName,
                    YearsOfExperience = mentor.Data.YearsOfExperience
                };
                return View(mentorViewModel);
            }
            return RedirectToAction("");
        }
        public async Task<IActionResult> GetAllMentors()
        {
            var mentor = await _mentorService.GetAllMentorAsync();
            if (mentor.Status)
            {
                var mentorsViewModel = mentor.Data.Select(x => new GetMentorViewModel
                {
                    Address = x.Address,
                    Age = x.Age,
                    CategoryName = x.CategoryName,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    Id = x.Id,
                    IsAvailable = x.IsAvailable,
                    LastName = x.LastName,
                    Meetings = x.Meetings,
                    Mentees = x.Mentees,
                    PhoneNumber = x.PhoneNumber,
                    UserName = x.UserName,
                    YearsOfExperience = x.YearsOfExperience
                });
                return View(mentorsViewModel);
            }
            return RedirectToAction("index", "home");
        }
        [HttpGet]
        public async Task<IActionResult> ViewMentorsInCategory(string id)
        {
            var categoryId = User.FindFirst("categoryId")?.Value;
            var mentor = await _mentorService.GetAllMentorInCategoryAsync(categoryId);
            if (mentor.Status)
            {
                var mentorsViewModel = mentor.Data.Select(x => new GetMentorViewModel
                {
                    Address = x.Address,
                    Age = x.Age,
                    CategoryName = x.CategoryName,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    Id = x.Id,
                    IsAvailable = x.IsAvailable,
                    LastName = x.LastName,
                    Meetings = x.Meetings,
                    Mentees = x.Mentees,
                    PhoneNumber = x.PhoneNumber,
                    UserName = x.UserName,
                    YearsOfExperience = x.YearsOfExperience
                });
                return View(mentorsViewModel);
            }
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
             var id = User.FindFirst("mentorId")?.Value;
            var categories = await _categoryService.GetAllCategoryAsync();
            var mentor = await _mentorService.GetIdAsync(id);
            if (mentor.Status)
            {
                var viewModel = new EditProfileViewModel
                {
                    Address = mentor.Data.Address,
                    Age = mentor.Data.Age,
                    Email = mentor.Data.Email,
                    FirstName = mentor.Data.FirstName,
                    LastName = mentor.Data.LastName,
                    PhoneNumber = mentor.Data.PhoneNumber,
                    UserName = mentor.Data.UserName,
                    YearsOfExperience = mentor.Data.YearsOfExperience,
                };
                return View(viewModel);
            }
            return View(null);
        }
        [HttpPost]
        public IActionResult EditProfile(EditProfileViewModel viewModel)
        {
            var update = new MentorUpdateRequestModel
            {
                Email = viewModel.Email,
                Address = viewModel.Address,
                Age = viewModel.Age,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                PhoneNumber = viewModel.PhoneNumber,
                YearsOfExperience = viewModel.YearsOfExperience,
                UserName = viewModel.UserName,
            };
            var updatedProfile = _mentorService.UpdateMentorProfile(update);
            if (updatedProfile.Status)
            {
                return RedirectToAction("MentorDashboard", "User");
            }
            return View();
        }
        [Authorize("SuperAdmin")]
        public IActionResult Delete(EditProfileViewModel viewModel)
        {
            var delete = new MentorUpdateRequestModel
            {
                Email = viewModel.Email,
                Address = viewModel.Address,
                Age = viewModel.Age,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                PhoneNumber = viewModel.PhoneNumber,
                YearsOfExperience = viewModel.YearsOfExperience,
                UserName = viewModel.UserName,
            };
            _mentorService.Remove(delete);
            return RedirectToAction("GetAllMentors");
        }
        public async Task<IActionResult> ViewAllMentees()
        {
            var Id = User.FindFirst("mentorId");
            var assignedMentees = await _mentorService.GetAllAssignedMenteeAsync(Id.Value);
            if (assignedMentees.Status)
            {
                var viewModel = assignedMentees.Data.Select(x => new GetMenteeViewModel
                {
                    Address = x.Address,
                    Age = x.Age,
                    CategoryName = x.CategoryName,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    UserName = x.UserName,
                    Id = x.Id
                });

                return View(viewModel);
            }
            return RedirectToAction("Mentordashboard", "User");
        }
    }
}