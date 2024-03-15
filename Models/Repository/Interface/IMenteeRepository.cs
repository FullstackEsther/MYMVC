using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MYMVC.Models.Entities;

namespace MYMVC.Models.Repository.Interface
{
    public interface IMenteeRepository
    {
        public Task<Mentee> CreateAsync(Mentee mentee);
        public Task<IEnumerable<Mentee>> GetAllMenteeAsync();
        public Task<Mentee> GetMenteeAsync(Expression<Func<Mentee,bool>> expression);
        public Task<IEnumerable<Mentee>> GetAllMenteeAsync(Expression<Func<Mentee,bool>> expression);
        public Mentee Update(Mentee mentee);
        public void Remove(Mentee mentee);
    }
}