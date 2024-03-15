using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MYMVC.Models.DTO;
using MYMVC.Models.Service.Interface;

namespace MYMVC.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
         [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

            [HttpPost]
        public async Task<IActionResult> Create(RoleRequestModel model)
        {
            var role = await _roleService.CreateAsync(model);
            if (role.Status)
            {
                return RedirectToAction("SuperAdminDashboard", "User");
            }
            return View();
        }
    }
}