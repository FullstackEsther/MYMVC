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
    public class MenteeRepository : IMenteeRepository
    {
        private readonly MvcContext _context;

        public MenteeRepository(MvcContext context)
        {
            _context = context;
        }
        public async Task<Mentee> CreateAsync(Mentee mentee)
        {
           await _context.Mentees.AddAsync(mentee);
           return mentee;
        }

        public async Task<IEnumerable<Mentee>> GetAllMenteeAsync()
        {
           var users = await _context.Mentees
            .Include(x => x.User).ThenInclude(x => x.Profile)
            .Include(x => x.Mentor)
            .Include(x => x.Category)
            .Include(x => x.Meetings)
            .ToListAsync();
            return users;
        }

        public async Task<IEnumerable<Mentee>> GetAllMenteeAsync(Expression<Func<Mentee, bool>> expression)
        {
            var mentees = await _context.Mentees
            .Include(x => x.User).ThenInclude(x => x.Profile)
            .Include(x => x.Mentor)
            .Include(x => x.Category)
            .Include(x => x.Meetings)
            .Where(expression)
            .ToListAsync();
            return mentees;
        }

        public async Task<Mentee> GetMenteeAsync(Expression<Func<Mentee, bool>> expression)
        {
           var user = await _context.Mentees
            .Include(x => x.User).ThenInclude(x => x.Profile)
            .Include(x => x.Mentor)
            .Include(x => x.Category)
            .Include(x => x.Meetings)
            .SingleOrDefaultAsync(expression);
            return user;
        }

        public void Remove(Mentee mentee)
        {
            _context.Mentees.Remove(mentee);
        }

        public Mentee Update(Mentee mentee)
        {
            _context.Mentees.Update(mentee);
            return mentee;
        }
    }
}