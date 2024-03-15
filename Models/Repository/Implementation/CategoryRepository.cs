using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MYMVC.Models.Context;
using MYMVC.Models.Entities;
using MYMVC.Models.Repository.Interface;

namespace MYMVC.Models.Repository.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MvcContext _context;
        public CategoryRepository(MvcContext context)
        {
            _context = context;
        }
        public async Task<Category> CreateAsync(Category category)
        {
           await _context.Categories.AddAsync(category);
           return category;
        }

        public async Task<bool> ExistAsync(Expression<Func<Category, bool>> expression)
        {
            return await _context.Categories.AnyAsync(expression);
        }

        public async Task<IEnumerable<Category>> GetAllCategoryAsync()
        {
            return await _context.Categories
            .Include(x => x.Mentor)
            .Include(x => x.Mentee)
            .AsSplitQuery()
            .ToListAsync();
        }

        public async Task<Category> GetCategoryAsync(Expression<Func<Category, bool>> expression)
        {
            var category = await _context.Categories
            .Include(x => x.Mentor)
            .Include(x => x.Mentee)
            .AsSplitQuery()
            .SingleOrDefaultAsync(expression);
            return category;
        }

        public void Remove(Category category)
        {
           _context.Categories.Remove(category);
        }

        public Category Update(Category category)
        {
            _context.Categories.Update(category);
            return category;
        }
    }
}