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
    public class MessageRepository : IMessageRepository
    {
        private readonly MvcContext _context;

        public MessageRepository(MvcContext context)
        {
            _context = context;
        }
        public async Task<Message> CreateAsync(Message message)
        {
           await _context.Messages.AddAsync(message);
           return message;
        }

        public async Task<IEnumerable<Message>> GetAllMessageAsync(Expression<Func<Message, bool>> expression)
        {
           return await _context.Messages
           .Include(x => x.Chat)
           .AsSplitQuery()
           .Where(expression)
           .ToListAsync();
        }

        public async Task<Message> GetMessageAsync(Expression<Func<Message, bool>> expression)
        {
           var message = await _context.Messages
           .Include(x => x.Chat)
           .AsSplitQuery()
           .SingleOrDefaultAsync(expression);
           return message;
        }
    }
}