using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MYMVC.Models.DTO;
using MYMVC.Models.Entities;
using MYMVC.Models.Service.Interface;
using MYMVC.Models.ViewModel;

namespace MYMVC.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;


        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateMessageViewModel viewModel)
        {
            var messageRequest = new MessageRequestModel
            {
                ChatContent = viewModel.ChatContent,
                ChatId = viewModel.ChatId,
                SenderUserName = User?.FindFirst(ClaimTypes.Name)?.Value,
            };
            await _messageService.CreateAsync(messageRequest);
            return View();
        }

        public async Task<IActionResult> GetAllMessages(string ChatId)
        {
            var allMessages = await _messageService.GetAllMessageAsync(ChatId);
            if (allMessages.Status)
            {
                var view = allMessages.Data.Select(x => new GetMessageViewmodel
                {
                    ChatContent = x.ChatContent,
                    ChatId = x.ChatId,
                    Id = x.Id,
                    SenderUserName = x.SenderUserName,
                    TimeSent = x.TimeSent
                });
                return View(view);
            }
            return View(null);
        }

    }
}