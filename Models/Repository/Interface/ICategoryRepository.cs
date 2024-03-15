using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MYMVC.Models.Entities;

namespace MYMVC.Models.Repository.Interface
{
    public interface ICategoryRepository
    {
        public Task<Category> CreateAsync(Category category);
        public Task<IEnumerable<Category>> GetAllCategoryAsync();
        public Task<Category> GetCategoryAsync(Expression<Func<Category,bool>> expression);
        public Task<bool> ExistAsync(Expression<Func<Category,bool>> expression);
        public Category Update(Category category);
        public void Remove(Category category);
    }
}