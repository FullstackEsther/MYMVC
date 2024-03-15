using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MYMVC.Models.DTO;
using MYMVC.Models.Entities;
using MYMVC.Models.Repository.Interface;
using MYMVC.Models.Service.Interface;
using MYMVC.Models.ViewModel;

namespace MYMVC.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;
        private readonly IMessageService _messageService;
        private readonly IUserService _userservice;


        public ChatController(IChatService chatService, IMessageService messageService, IUserService userService)
        {
            _chatService = chatService;
            _messageService = messageService;
            _userservice = userService;

        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateChatViewModel model)
        {
            var chat = new CreateChatRequestModel
            {
                ReceiverId = model.ReceiverId,
                SenderId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                ChatContent = model.ChatContent,
                SenderUserName = User.FindFirst(ClaimTypes.Name).Value,
            };
            var createChat = await _chatService.CreateAsync(chat);
            if (createChat.Status)
            {
                return View();
            }
            return RedirectToAction("Index", "user");
        }

        public async Task<IActionResult> GetChat(string receiverId)
        {
            BaseResponse<UserDto> user = null;
            var id = User.FindFirst(ClaimTypes.NameIdentifier);
            if (User.FindFirst(ClaimTypes.Role)?.Value == "Mentor")
            {
                user = await _userservice.GetUserMenteeAsync(receiverId);
            }
            if (User.FindFirst(ClaimTypes.Role)?.Value == "Mentee")
            {
                user = await _userservice.GetUserMentorAsync(receiverId);
            }
            var chat = await _chatService.GetChatAsync(id.Value, user.Data.Id);
            if (chat.Data != null)
            {
                var messagesInChat = await _messageService.GetAllMessageAsync(chat.Data.Id);
                var viewModel = new GetChatViewModel
                {
                    ReceiverId = chat.Data.ReceiverId,
                    SenderId = chat.Data.SenderId,
                    Messages = messagesInChat.Data.Select(x => new GetMessageViewmodel
                    {
                        ChatContent = x.ChatContent,
                        ChatId = x.ChatId,
                        SenderUserName = x.ChatContent,
                        TimeSent = x.TimeSent,

                    }).ToList()
                };
                return View(viewModel);
            }
            return View(null);

            
        }
    }
}