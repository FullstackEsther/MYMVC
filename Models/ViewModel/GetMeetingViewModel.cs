using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYMVC.Models.ViewModel
{
    public class GetMeetingViewModel
    {
         public string Id {get; set;}
        public DateTime DateAndTime { get; set; } = default!;
        public string MenteeUserName { get; set; } = default!;
        public string MentorUserName { get; set; } = default!;
    }
}