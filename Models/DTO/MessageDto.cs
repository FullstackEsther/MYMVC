using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYMVC.Models.DTO
{
    public class MessageDto
    {
        public string Id {get; set;}
        public string ChatId { get; set; }
        public string ChatContent { get; set; } = default!;
        public string SenderUserName { get; set; } = default!;
        public TimeSpan TimeSent { get; set; }
    }
    public class MessageRequestModel
    {
        public string ChatContent { get; set; } = default!;
        public string SenderUserName { get; set; } = default!;
        public string ChatId { get; set; } = default!;
    }
}