using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYMVC.Models.Entities
{
    public class Message : BaseEntity
    {
        public string ChatId { get; set; }
        public string ChatContent { get; set; }= default!;
        public string SenderUserName { get; set; } = default!;
        public TimeSpan TimeSent {get; set;} = DateTime.Now.TimeOfDay;
        public Chat Chat {get;set;}
    }
}