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
    public class MeetingRepository : IMeetingRepository
    {
        private readonly MvcContext _context;

        public MeetingRepository(MvcContext context)
        {
            _context = context;
        }
        public async Task<Meeting> CreateAsync(Meeting meeting)
        {
            await _context.Meetings.AddAsync(meeting);
            return meeting;
        }

        public bool ExistAsync(Expression<Func<Meeting, bool>> expression)
        {
           var exist =  _context.Meetings.Any(expression);
           return exist;
        }

        public async Task<IEnumerable<Meeting>> GetAllMeetingAsync(Expression<Func<Meeting,bool>> expression)
        {
            return await _context.Meetings
            .Include(x=>x.Mentor).ThenInclude(x => x.User)
            .Include(x=>x.Mentees).ThenInclude(x => x.User)
            .AsSplitQuery()
            .Where(expression)
            .ToListAsync();
        }

        public async Task<Meeting> GetMeetingAsync(Expression<Func<Meeting, bool>> expression)
        {
              var meeting = await _context.Meetings
            .Include(x=>x.Mentor).ThenInclude(x => x.User)
            .Include(x=>x.Mentees).ThenInclude(x => x.User)
            .AsSplitQuery()
            .SingleOrDefaultAsync(expression);
            return meeting;
        }

        public async Task<IEnumerable<Meeting>> GetMeetingByTimeAsync(Expression<Func<Meeting, bool>> expression)
        {
            var meeting = await _context.Meetings
            .Include(x=>x.Mentor).ThenInclude(x => x.User)
            .Include(x=>x.Mentees).ThenInclude(x => x.User)
            .AsSplitQuery()
            .Where(expression).ToListAsync();
            return meeting;
        }

        public void Remove(Meeting meeting)
        {
           _context.Meetings.Remove(meeting);
        }

        public Meeting Update(Meeting meeting)
        {
            _context.Meetings.Update(meeting);
            return meeting;
        }
    }
}