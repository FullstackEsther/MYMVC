using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MYMVC.Models.DTO;
using MYMVC.Models.Entities;

namespace MYMVC.Models.Service.Interface
{
    public interface IChatService
    {
        public Task<BaseResponse<Chat>> CreateAsync(CreateChatRequestModel chat);
        public Task<BaseResponse<IEnumerable<Chat>>> GetAllChatAsync(string id);
        public Task<BaseResponse<Chat>> GetChatAsync(string SenderId , string ReceiverId);
    }
}