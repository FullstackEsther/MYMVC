using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MYMVC.Models.DTO;
using MYMVC.Models.Entities;
using MYMVC.Models.Service.Interface;
using MYMVC.Models.ViewModel;

namespace MYMVC.Controllers
{
    public class MenteeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMenteeService _menteeService;
        private readonly ICategoryService _categoryService;

        public MenteeController(IUserService userService, IMenteeService menteeService, ICategoryService categoryService)
        {
            _menteeService = menteeService;
            _categoryService = categoryService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllCategoryAsync();
            var model = new CreateMenteeViewModel
            {
                Category = categories.Data.Select(x => new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.Id
                })
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateMenteeViewModel viewModel)
        {
            var menteeRequestModel = new MenteeRequestModel
            {
                Address = viewModel.Address,
                Age = viewModel.Age,
                Email = viewModel.Email,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Password = viewModel.Password,
                PhoneNumber = viewModel.PhoneNumber,
                UserName = viewModel.UserName,
                CategoryId = viewModel.SelectedCategory
            };
            // if (ModelState.IsValid)
            // {
            var createdMentee = await _menteeService.CreateAsync(menteeRequestModel);
            if (createdMentee.Status)
            {
                return RedirectToAction("index", "home");
            }
            // }

            return RedirectToAction("create");
        }
        [HttpGet]
        public async Task<IActionResult> GetMentee([FromRoute] string id)
        {
            var Id = User.FindFirst("menteeId").Value;
            var mentee = await _menteeService.GetByIdAsync(Id);
            if (mentee.Status)
            {
                var menteeViewModel = new GetMenteeViewModel
                {
                    Address = mentee.Data.Address,
                    Age = mentee.Data.Age,
                    CategoryName = mentee.Data.CategoryName,
                    Email = mentee.Data.Email,
                    FirstName = mentee.Data.FirstName,
                    Id = mentee.Data.Id,
                    LastName = mentee.Data.LastName,
                    PhoneNumber = mentee.Data.PhoneNumber,
                    UserName = mentee.Data.UserName,
                    Meetings = mentee.Data.Meetings,
                };
                return View(menteeViewModel);
            }
            return RedirectToAction("create");
        }
        public async Task<IActionResult> GetAllMentees()
        {
            var mentee = await _menteeService.GetAllMenteeAsync();
            if (mentee.Status)
            {
                var menteesViewModel = mentee.Data.Select(x => new GetMenteeViewModel
                {
                    Address = x.Address,
                    Age = x.Age,
                    CategoryName = x.CategoryName,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    Id = x.Id,
                    LastName = x.LastName,
                    Meetings = x.Meetings,
                    PhoneNumber = x.PhoneNumber,
                    UserName = x.UserName,
                });
                return View(menteesViewModel);
            }
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public async Task<IActionResult> EditMenteeProfile(string id)
        {
            var Id = User.FindFirst("menteeId").Value;
            var categories = await _categoryService.GetAllCategoryAsync();
            var mentee = await _menteeService.GetByIdAsync(Id);
            if (mentee.Status)
            {
                var viewModel = new EditMenteeProfileViewModel
                {
                    Address = mentee.Data.Address,
                    Age = mentee.Data.Age,
                    Email = mentee.Data.Email,
                    FirstName = mentee.Data.FirstName,
                    LastName = mentee.Data.LastName,
                    PhoneNumber = mentee.Data.PhoneNumber,
                    UserName = mentee.Data.UserName
                };
                return View(viewModel);
            }
            return View(null);
        }
        [HttpPost]
        public IActionResult EditMenteeProfile(EditMenteeProfileViewModel viewModel)
        {
            var update = new MenteeUpdateRequestModel
            {
                Address = viewModel.Address,
                Age = viewModel.Age,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                PhoneNumber = viewModel.PhoneNumber,
                UserName = viewModel.UserName,
                Email = viewModel.Email
            };
            var updatedProfile = _menteeService.UpdateMentee(update);
            if (updatedProfile.Status)
            {
                return RedirectToAction("MenteeDashboard", "User");
            }
            return View();
        }
        [Authorize("SuperAdmin")]
        public  IActionResult Delete(EditMenteeProfileViewModel viewModel)
        {
            var delete = new MenteeUpdateRequestModel
            {
                 Email = viewModel.Email,
                Address = viewModel.Address,
                Age = viewModel.Age,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                PhoneNumber = viewModel.PhoneNumber,
                UserName = viewModel.UserName,
            };
            _menteeService.Remove(delete);
            
            return RedirectToAction("GetAllMentees");
        }
        public IActionResult UpdateMentorId(string mentorId)
        {
            var menteeId = User.FindFirst("menteeId");
            var update = _menteeService.UpdateNewMentor(menteeId.Value, mentorId);
            if (update.Status)
            {
                return RedirectToAction("MenteeDashboard", "User");
            }
            return RedirectToAction("ViewMentorsInCategory", "Mentor");
        }
    }
}