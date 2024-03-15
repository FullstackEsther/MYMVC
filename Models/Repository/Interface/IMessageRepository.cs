using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MYMVC.Models.Entities;

namespace MYMVC.Models.Repository.Interface
{
    public interface IMessageRepository
    {
        public Task<Message> CreateAsync(Message message);
        public Task<IEnumerable<Message>> GetAllMessageAsync(Expression<Func<Message, bool>> expression);
        public Task<Message> GetMessageAsync(Expression<Func<Message,bool>> expression);
    }
}