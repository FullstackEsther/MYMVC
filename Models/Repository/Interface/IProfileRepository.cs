using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MYMVC.Models.Entities;

namespace MYMVC.Models.Repository.Interface
{
    public interface IProfileRepository
    {
        public Task<Profile> CreateAsync(Profile profile);
        public Task<IEnumerable<Profile>> GetAllProfileAsync();
        public Task<Profile> GetProfileAsync(Expression<Func<Profile,bool>> expression);
        public Profile Update(Profile profile);
        public void Remove(Profile profile);
    }
}