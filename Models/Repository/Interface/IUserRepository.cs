using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MYMVC.Models.Entities;

namespace MYMVC.Models.Repository.Interface
{
    public interface IUserRepository
    {
        public Task<User> CreateAsync(User user);
        public Task<IEnumerable<User>> GetAllUserAsync();
        public Task<User> GetUserAsync(Expression<Func<User,bool>> expression);
        public bool Exist(Expression<Func<User,bool>> expression);
        public User Update(User user);
        public void Remove(User user);
    }
}