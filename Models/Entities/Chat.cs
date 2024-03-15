using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYMVC.Models.Entities
{
    public class Chat :BaseEntity
    {
        public string SenderId {get;set;}= default!;
        public string ReceiverId {get;set;}= default!;
        public IEnumerable<Message> Messages {get; set;}
    }
}