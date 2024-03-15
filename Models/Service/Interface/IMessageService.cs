using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MYMVC.Models.DTO;
using MYMVC.Models.Entities;

namespace MYMVC.Models.Service.Interface
{
    public interface IMessageService
    {
        public Task<BaseResponse<MessageDto>> CreateAsync(MessageRequestModel message);
        public Task<BaseResponse<IEnumerable<MessageDto>>> GetAllMessageAsync(string ChatId);
        public Task<BaseResponse<MessageDto>> GetMessageAsync(string id);
    }
}