using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MYMVC.Models.Entities;

namespace MYMVC.Models.Repository.Interface
{
    public interface IRoleRepository
    {
        public Task<Role> CreateAsync(Role role);
        public Task<IEnumerable<Role>> GetAllRoleAsync();
        public Task<Role> GetRoleAsync(Expression<Func<Role,bool>> expression);
        public void Remove(Role role);
    }
}