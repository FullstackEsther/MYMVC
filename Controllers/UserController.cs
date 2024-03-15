using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MYMVC.Models.DTO;
using MYMVC.Models.Service.Interface;
using MYMVC.Models.ViewModel;

namespace MYMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var login = await _userService.LoginAsync(loginViewModel.UserName, loginViewModel.Password);
            if (!login.Status)
            {
                return View(loginViewModel);
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, login.Data.Id),
                new Claim(ClaimTypes.Name , login.Data.UserName),
                new Claim(ClaimTypes.Email , login.Data.Email),
                new Claim(ClaimTypes.Role , login.Data.RoleName),
            };
            if(login.Data.MenteeId != null)
            {
                claims.Add(new Claim("menteeId", login.Data.MenteeId));
            }
            if(login.Data.MentorId != null)
            {
                claims.Add(new Claim("mentorId", login.Data.MentorId));
            }
            if (login.Data.CategoryId != null)
            {
                claims.Add(new Claim("categoryId", login.Data.CategoryId));
            }
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties();

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
            if (login.Data.RoleName == "SuperAdmin")
            {
                return RedirectToAction("SuperAdminDashboard", login.Data );
            }
            if (login.Data.RoleName == "Mentor")
            {
                return RedirectToAction("MentorDashboard", login.Data);
            }
            if (login.Data.RoleName == "Mentee")
            {
                return RedirectToAction("MenteeDashboard", login.Data);
            }
            return View(loginViewModel);

        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> MentorDashboard(UserDto userDto)
        {
            return View(userDto);
        }
        [HttpGet]
        public async Task<IActionResult> MenteeDashboard(UserDto userDto)
        {
            return View(userDto);
        }
        [HttpGet]
        public async Task<IActionResult> SuperAdminDashboard(UserDto userDto)
        {
            return View(userDto);
        }

    }
}