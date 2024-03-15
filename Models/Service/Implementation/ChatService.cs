using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MYMVC.Models.DTO;
using MYMVC.Models.Entities;
using MYMVC.Models.Repository.Interface;
using MYMVC.Models.Service.Interface;

namespace MYMVC.Models.Service.Implementation
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageRepository _messageRepository;

        public ChatService(IChatRepository chatRepository, IUnitOfWork unitOfWork, IMessageRepository messageRepository)
        {
            _chatRepository = chatRepository;
            _unitOfWork = unitOfWork;
            _messageRepository = messageRepository;
        }
        public async Task<BaseResponse<Chat>> CreateAsync(CreateChatRequestModel chat)
        {
            var exist = _chatRepository.ExistAsync(x => x.SenderId == chat.SenderId && x.ReceiverId == chat.ReceiverId);
            if (exist)
            {
                var newMessage = new Message
                {
                    ChatContent = chat.ChatContent,
                    ChatId = chat.ChatId,
                    SenderUserName = chat.SenderUserName,
                };
                await _messageRepository.CreateAsync(newMessage);
            }
            var newChat = new Chat
            {
                ReceiverId = chat.ReceiverId,
                SenderId = chat.SenderId
            };
            await _chatRepository.CreateAsync(newChat);
            var message = new Message
            {
                ChatContent = chat.ChatContent,
                SenderUserName = chat.SenderUserName,
                ChatId = newChat.Id
            };
            await _messageRepository.CreateAsync(message);
            await _unitOfWork.SaveAsync();
            return new BaseResponse<Chat>
            {
                Status = true,
                Message = "successful",
                Data = new Chat
                {
                    DateCreated = newChat.DateCreated,
                    Id = newChat.Id,
                    Messages = newChat.Messages,
                    ReceiverId = newChat.ReceiverId,
                    SenderId = newChat.SenderId
                }
            };
        }

        public async Task<BaseResponse<IEnumerable<Chat>>> GetAllChatAsync(string id)
        {
            var chats = await _chatRepository.GetAllChatAsync(x => x.SenderId == id);
            if (chats != null)
            {
                return new BaseResponse<IEnumerable<Chat>>
                {
                    Status = true,
                    Message = "sucessful",
                    Data = chats.Select(x => new Chat
                    {
                        DateCreated = x.DateCreated,
                        Id = x.Id,
                        Messages = x.Messages,
                        ReceiverId = x.ReceiverId,
                        SenderId = x.SenderId
                    })
                };
            }
            return new BaseResponse<IEnumerable<Chat>>
            {
                Data = null,
                Message = "notfound",
                Status = false
            };
        }

        public async Task<BaseResponse<Chat>> GetChatAsync(string SenderId, string ReceiverId)
        {
            var chat = await _chatRepository.GetChatAsync(x => x.SenderId == SenderId && x.ReceiverId == ReceiverId);
            if (chat != null)
            {
                return new BaseResponse<Chat>
                {
                    Status = true,
                    Message = "sucessful",
                    Data = new Chat
                    {
                        DateCreated = chat.DateCreated,
                        Id = chat.Id,
                        Messages = chat.Messages,
                        ReceiverId = chat.ReceiverId,
                        SenderId = chat.SenderId
                    }
                };
            }
            return new BaseResponse<Chat>
            {
                Data = null,
                Message = "notfound",
                Status = false
            };
        }
    }
}