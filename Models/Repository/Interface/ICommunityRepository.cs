using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MYMVC.Models.Repository.Interface
{
    public interface ICommunityRepository
    {
        public Task<Community> CreateAsync(Community community);
        public Task<IEnumerable<Community>> GetAllCommunityAsync();
        public Task<Community> GetCommunityAsync(Expression<Func<Community,bool>> expression);
    }
}