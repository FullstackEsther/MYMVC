using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MYMVC.Models.DTO;
using MYMVC.Models.Entities;
using MYMVC.Models.Repository.Interface;
using MYMVC.Models.Service.Interface;

namespace MYMVC.Models.Service.Implementation
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IMessageRepository messageRepository, IUnitOfWork unitOfWork)
        {
            _messageRepository = messageRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<MessageDto>> CreateAsync(MessageRequestModel message)
        {
            var newMessage = new Message
            {
                ChatContent = message.ChatContent,
                SenderUserName = message.SenderUserName,
                ChatId = message.ChatId
            };
            await _messageRepository.CreateAsync(newMessage);
            await _unitOfWork.SaveAsync();
            return new BaseResponse<MessageDto>
            {
                Status = true,
                Message = "sucessful",
                Data = new MessageDto
                {
                    ChatContent = newMessage.ChatContent,
                    ChatId = newMessage.ChatId,
                    Id = newMessage.Id,
                    SenderUserName = newMessage.SenderUserName,
                    TimeSent = newMessage.TimeSent
                }
            };
        }

        public async Task<BaseResponse<IEnumerable<MessageDto>>> GetAllMessageAsync(string ChatId)
        {
            var messages = await _messageRepository.GetAllMessageAsync(x => x.ChatId == ChatId);
            if (messages != null)
            {
                return new BaseResponse<IEnumerable<MessageDto>>
                {
                    Status = true,
                    Message = "successful",
                    Data = messages.Select(x => new MessageDto
                    {
                        ChatContent = x.ChatContent,
                        ChatId = x.ChatId,
                        Id = x.Id,
                        SenderUserName = x.SenderUserName,
                        TimeSent = x.TimeSent
                    })
                };
            }
            return new BaseResponse<IEnumerable<MessageDto>>
            {
                Data = null,
                Message = "Not found",
                Status = false
            };
        }

        public async Task<BaseResponse<MessageDto>> GetMessageAsync(string id)
        {
            var message = await _messageRepository.GetMessageAsync(x => x.Id == id);
            if (message != null)
            {
                return new BaseResponse<MessageDto>
                {
                    Status = true,
                    Message = "successful",
                    Data = new MessageDto
                    {
                        ChatContent = message.ChatContent,
                        ChatId = message.ChatId,
                        Id = message.Id,
                        SenderUserName = message.SenderUserName,
                        TimeSent = message.TimeSent
                    }
                };
            }
            return new BaseResponse<MessageDto>
            {
                Data = null,
                Message = "Not found",
                Status = false
            };
        }
    }
}