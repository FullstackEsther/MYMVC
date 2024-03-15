using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MYMVC.Models.Context;
using MYMVC.Models.Repository.Interface;

namespace MYMVC.Models.Repository.Implementation
{
    public class CommunityRepository : ICommunityRepository
    {
        private readonly MvcContext _context;

        public CommunityRepository(MvcContext context)
        {
            _context = context;
        }
        public async Task<Community> CreateAsync(Community community)
        {
          await _context.Communities.AddAsync(community);
           return community;
        }

        public async Task<IEnumerable<Community>> GetAllCommunityAsync()
        {
           return await _context.Communities
           .Include(x=>x.CommunityMembers)
           .AsSplitQuery()
           .ToListAsync();
        }

        public async Task<Community> GetCommunityAsync(Expression<Func<Community, bool>> expression)
        {
            var community =await _context.Communities
           .Include(x=>x.CommunityMembers)
           .AsSplitQuery()
           .SingleOrDefaultAsync(expression);
           return community;
        }
    }
}