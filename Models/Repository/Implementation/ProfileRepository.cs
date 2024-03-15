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
    public class ProfileRepository : IProfileRepository
    {
        private readonly MvcContext _context;

        public ProfileRepository(MvcContext context)
        {
            _context = context;
        }
        public async Task<Profile> CreateAsync(Profile profile)
        {
           await  _context.Profiles.AddAsync(profile);
           return profile;
        }

        public async Task<IEnumerable<Profile>> GetAllProfileAsync()
        {
           return await _context.Profiles
           .Include(x => x.User)
           .AsSplitQuery()
           .ToListAsync();
        }

        public async Task<Profile> GetProfileAsync(Expression<Func<Profile, bool>> expression)
        {
            var profile = await _context.Profiles
            .Include(x => x.User)
            .AsSplitQuery()
            .SingleOrDefaultAsync(expression);
            return profile;
        }

        public void Remove(Profile profile)
        {
            _context.Profiles.Remove(profile);
        }

        public Profile Update(Profile profile)
        {
           _context.Profiles.Update(profile);
           return profile;
        }
    }
}