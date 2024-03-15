using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MYMVC.Models.DTO;
using MYMVC.Models.Entities;
using MYMVC.Models.Repository.Interface;
using MYMVC.Models.Service.Interface;

namespace MYMVC.Models.Service.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<CategoryDto>> CreateAsync(CategoryRequestModel category)
        {
            var exist = await _categoryRepository.ExistAsync(x => x.CategoryName == category.CategoryName);
            if (exist)
            {
                return new BaseResponse<CategoryDto>
                {
                    Status = false,
                    Message = "Already Exists",
                    Data = null
                };
            }
            var categoryObj = new Category
            {
                CategoryName = category.CategoryName,
                Description = category.Description,
            };
            await _categoryRepository.CreateAsync(categoryObj);
            await _unitOfWork.SaveAsync();
            return new BaseResponse<CategoryDto>
            {
                Status = true,
                Message = "Created Successfully",
                Data = new CategoryDto
                {
                    CategoryName = categoryObj.CategoryName,
                    Description = categoryObj.Description,
                    Id = categoryObj.Id
                }
            };

        }

        public async Task<BaseResponse<IEnumerable<CategoryDto>>> GetAllCategoryAsync()
        {
            var categories = await _categoryRepository.GetAllCategoryAsync();
            if (categories != null)
            {
                return new BaseResponse<IEnumerable<CategoryDto>>
                {
                    Status = true,
                    Message = "Successfully",
                    Data = categories.Select(x => new CategoryDto
                    {
                        CategoryName = x.CategoryName,
                        Description = x.Description,
                        Id = x.Id
                    }).ToList()
                };
            }
            return new BaseResponse<IEnumerable<CategoryDto>>
            {
                Data = null,
                Message = "No category",
                Status = false
            };
        }

        public async Task<BaseResponse<CategoryDto>> GetCategoryAsync(string id)
        {
            var category = await _categoryRepository.GetCategoryAsync(x => x.Id == id);
            if (category != null)
            {
                return new BaseResponse<CategoryDto>
                {
                    Status = true,
                    Message = "successful",
                    Data = new CategoryDto
                    {
                        CategoryName = category.CategoryName,
                        Description = category.Description,
                        Id = category.Id
                    }
                };
            }
            return new BaseResponse<CategoryDto>
            {
                Data = null,
                Message = "not found",
                Status = false
            };
        }
    }
}