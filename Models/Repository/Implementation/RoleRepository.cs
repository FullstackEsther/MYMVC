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
    public class RoleRepository : IRoleRepository
    {
        private readonly MvcContext _context;

        public RoleRepository(MvcContext context)
        {
            _context = context;
        }
        public async Task<Role> CreateAsync(Role role)
        {
          await _context.Roles.AddAsync(role);
          return role;
        }

        public async Task<IEnumerable<Role>> GetAllRoleAsync()
        {
            return await _context.Roles
            .Include(x => x.Users)
            .AsSplitQuery()
            .ToListAsync();
        }

        public async Task<Role> GetRoleAsync(Expression<Func<Role, bool>> expression)
        {
            var role = await _context.Roles
            .Include(x => x.Users)
            .AsSplitQuery()
            .SingleOrDefaultAsync(expression);
            return role;
        }

        public void Remove(Role role)
        {
            _context.Roles.Remove(role);
        }
    }
}