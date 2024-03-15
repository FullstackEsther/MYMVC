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
    public class UserRepository : IUserRepository
    {
        private readonly MvcContext _context;

        public UserRepository(MvcContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            return user;
        }

        public bool Exist(Expression<Func<User, bool>> expression)
        {
           var exist =  _context.Users.Any(expression);
           return exist;
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
           var users =await _context.Users
           .Include(x => x.Community)
           .Include(x => x.Mentee)
           .Include(x => x.Mentee)
           .Include(x => x.Role)
           .AsSplitQuery()
           .ToListAsync();
           return users;
        }

        public async Task<User> GetUserAsync(Expression<Func<User, bool>> expression)
        {
            var user =await _context.Users
           .Include(x => x.Community)
           .Include(x => x.Mentee)
           .ThenInclude(x=>x.Category)
           .Include(x=>x.Mentor)
           .Include(x => x.Role)
           .AsSplitQuery()
           .SingleOrDefaultAsync(expression);
           return user;
        }

        public void Remove(User user)
        {
           _context.Users.Remove(user);
        }

        public User Update(User user)
        {
           _context.Users.Update(user);
           return user;
        }
    }
}