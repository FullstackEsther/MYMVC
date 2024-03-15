using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MYMVC.Models.DTO;
using MYMVC.Models.Entities;

namespace MYMVC.Models.Service.Interface
{
    public interface ICategoryService
    {
        public Task<BaseResponse<CategoryDto>> CreateAsync(CategoryRequestModel category);
        public Task<BaseResponse<IEnumerable<CategoryDto>>> GetAllCategoryAsync();
        public Task<BaseResponse<CategoryDto>> GetCategoryAsync(string id);
    }
}