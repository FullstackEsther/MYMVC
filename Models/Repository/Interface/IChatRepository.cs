using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MYMVC.Models.Entities;

namespace MYMVC.Models.Repository.Interface
{
    public interface IChatRepository
    {
        public bool ExistAsync(Expression<Func<Chat,bool>> expression);
        public Task<Chat> CreateAsync(Chat chat);
        public Task<IEnumerable<Chat>> GetAllChatAsync();
        public Task<IEnumerable<Chat>> GetAllChatAsync(Expression<Func<Chat,bool>> expression);
        public Task<Chat> GetChatAsync(Expression<Func<Chat,bool>> expression);
    }
}