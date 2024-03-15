namespace MYMVC.Models.DTO
{
    public class ChatDto
    {
        
    }
    public class CreateChatRequestModel
    {
        public string SenderId {get;set;}= default!;
        public string ReceiverId {get;set;}= default!;
         public string ChatContent { get; set; } = default!;
        public string SenderUserName { get; set; } = default!;
        public string ChatId { get; set; } = default!;
    }
}