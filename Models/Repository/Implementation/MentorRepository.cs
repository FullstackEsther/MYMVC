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
    public class MentorRepository : IMentorRepository
    {
        private readonly MvcContext _context;

        public MentorRepository(MvcContext context)
        {
            _context = context;
        }
        public async Task<Mentor> CreateAsync(Mentor mentor)
        {
           await _context.Mentors.AddAsync(mentor);
           return mentor;
        }

        public async Task<IEnumerable<Mentor>> GetAllMentorAsync()
        {
           return await _context.Mentors
           .Include(x => x.Meetings)
           .Include(x => x.Mentees)
           .Include(x => x.Category)
           .Include(x => x.User).ThenInclude(x => x.Profile)
           .AsSplitQuery()
           .ToListAsync();
        }

        public async Task<IEnumerable<Mentor>> GetAllMentorAsync(Expression<Func<Mentor, bool>> expression)
        {
            return await _context.Mentors
           .Include(x => x.Meetings)
           .Include(x => x.Mentees)
           .Include(x => x.Category)
           .Include(x => x.User).ThenInclude(x => x.Profile)
           .AsSplitQuery()
           .Where(expression)
           .ToListAsync();
        }

        public async Task<Mentor> GetMentorAsync(Expression<Func<Mentor, bool>> expression)
        {
           var mentor = await _context.Mentors
           .Include(x => x.Meetings)
           .Include(x => x.Mentees)
           .Include(x => x.Category)
           .Include(x => x.User).ThenInclude(x => x.Profile)
           .AsSplitQuery()
           .SingleOrDefaultAsync(expression);
           return mentor;
        }

        public void Remove(Mentor mentor)
        {
            _context.Mentors.Remove(mentor);
        }

        public Mentor Update(Mentor mentor)
        {
            _context.Mentors.Update(mentor);
            return mentor;
        }
    }
}