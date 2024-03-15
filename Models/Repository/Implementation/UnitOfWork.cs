using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MYMVC.Models.Context;
using MYMVC.Models.Repository.Interface;

namespace MYMVC.Models.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MvcContext _context;

        public UnitOfWork(MvcContext context)
        {
            _context = context;
        }
        public async Task SaveAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}