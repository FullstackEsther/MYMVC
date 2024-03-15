using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MYMVC.Models.Entities;

namespace MYMVC.Models.Repository.Interface
{
    public interface IMeetingRepository
    {
        public Task<Meeting> CreateAsync(Meeting meeting);
        public Task<IEnumerable<Meeting>> GetAllMeetingAsync(Expression<Func<Meeting,bool>> expression);
        public Task<IEnumerable<Meeting>> GetMeetingByTimeAsync(Expression<Func<Meeting,bool>> expression);
        public Task<Meeting> GetMeetingAsync(Expression<Func<Meeting,bool>> expression);
        public bool ExistAsync(Expression<Func<Meeting,bool>> expression);
        public void Remove(Meeting meeting);
        public Meeting Update(Meeting meeting);
    }
}