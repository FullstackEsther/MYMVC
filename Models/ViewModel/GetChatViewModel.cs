using MailKit;
using MYMVC.Models.Entities;

namespace MYMVC.Models.ViewModel
{
    public class GetChatViewModel
    {
        public string SenderId {get;set;}= default!;
        public string ReceiverId {get;set;}= default!;
        public List<GetMessageViewmodel> Messages {get; set;}
    }
}