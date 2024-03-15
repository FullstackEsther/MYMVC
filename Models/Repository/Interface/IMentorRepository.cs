using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MYMVC.Models.Entities;

namespace MYMVC.Models.Repository.Interface
{
    public interface IMentorRepository
    {
        public Task<Mentor> CreateAsync(Mentor mentor);
        public Task<IEnumerable<Mentor>> GetAllMentorAsync();
        public Task<Mentor> GetMentorAsync(Expression<Func<Mentor,bool>> expression);
        public Task<IEnumerable<Mentor>> GetAllMentorAsync(Expression<Func<Mentor,bool>> expression);
        public Mentor Update(Mentor mentor);
        public void Remove(Mentor user);
    }
}