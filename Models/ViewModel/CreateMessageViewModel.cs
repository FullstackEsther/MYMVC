namespace MYMVC.Models.ViewModel
{
    public class CreateMessageViewModel
    {
        public string ChatContent { get; set; } = default!;
        public string SenderUserName { get; set; } = default!;
        public string ChatId { get; set; } = default!;
    }
}