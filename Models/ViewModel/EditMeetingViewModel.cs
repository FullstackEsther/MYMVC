using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYMVC.Models.ViewModel
{
    public class EditMeetingViewModel
    {
        public string Id {get; set;}
        public DateOnly Date { get; set; } = default!;
        public TimeSpan Time { get; set; } = default!;
        public string MentorId { get; set; } = default!;
    }
}