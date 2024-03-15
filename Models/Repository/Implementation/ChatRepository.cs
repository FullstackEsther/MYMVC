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
    public class ChatRepository : IChatRepository
    {
        private readonly MvcContext _context;

        public ChatRepository(MvcContext context)
        {
            _context = context;
        }

        public async Task<Chat> CreateAsync(Chat chat)
        {
           await _context.Chats.AddAsync(chat);
           return chat;
        }

        public bool ExistAsync(Expression<Func<Chat, bool>> expression)
        {
            var exist =  _context.Chats.Any(expression);
            return exist;
        }

        public async Task<IEnumerable<Chat>> GetAllChatAsync()
        {
           return await _context.Chats
           .Include(x => x.Messages)
           .AsSplitQuery()
           .ToListAsync();
        }

        public async Task<IEnumerable<Chat>> GetAllChatAsync(Expression<Func<Chat, bool>> expression)
        {
            return await _context.Chats
           .Include(x => x.Messages)
           .AsSplitQuery()
           .Where(expression)
           .ToListAsync();
        }

        public async Task<Chat> GetChatAsync(Expression<Func<Chat, bool>> expression)
        {
            var chat = await _context.Chats
            .Include(x => x.Messages)
           .AsSplitQuery()
           .SingleOrDefaultAsync(expression);
           return chat;
        }
    }
}