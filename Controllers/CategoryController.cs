using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MYMVC.Models.DTO;
using MYMVC.Models.Service.Interface;
using MYMVC.Models.ViewModel;

namespace MYMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
         public async Task<IActionResult> Create(CreateCategoryViewModel createCategoryViewModel)
        {
            var categoryRequestModel = new CategoryRequestModel
            {
                 CategoryName = createCategoryViewModel.CategoryName,
                  Description = createCategoryViewModel.Description
            };
            var  category = await _categoryService.CreateAsync(categoryRequestModel);
            if (category.Status)
            {
                return RedirectToAction("SuperAdminDashboard", "User");
            }
            return View();
        }
        public async Task<IActionResult> GetCategory([FromRoute]string id)
        {
            var category = await _categoryService.GetCategoryAsync(id);
            if (category.Status)
            {
                return View(category.Data);
            }
            return RedirectToAction("Create");
        }

        public async Task<IActionResult> GetAll()
        {
            var category = await _categoryService.GetAllCategoryAsync();
            if (category.Status)
            {
                return View(category.Data);
            }
            return RedirectToAction("Create");
        }

    }
}