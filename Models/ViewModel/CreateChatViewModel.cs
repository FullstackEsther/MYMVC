namespace MYMVC.Models.ViewModel
{
    public class CreateChatViewModel
    {
        public string SenderId {get;set;}= default!;
        public string ReceiverId {get;set;}= default!;
        public string ChatContent { get; set; } = default!;
        public string SenderUserName { get; set; } = default!;
    }
}