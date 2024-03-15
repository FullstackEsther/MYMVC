namespace MYMVC.Models.ViewModel
{
    public class GetMessageViewmodel
    {
         public string Id {get; set;}
        public string ChatId { get; set; }
        public string ChatContent { get; set; } = default!;
        public string SenderUserName { get; set; } = default!;
        public TimeSpan TimeSent { get; set; }
    }
}