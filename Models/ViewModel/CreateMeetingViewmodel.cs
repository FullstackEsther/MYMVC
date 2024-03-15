using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MYMVC.Models.ViewModel
{
    public class CreateMeetingViewmodel
    {
        public DateOnly Date { get; set; } = default!;
        public TimeSpan Time { get; set; } = default!;
        public string MentorId { get; set; } = default!;
        public IEnumerable<SelectListItem> Mentee {get;set;}
        public string SelectedMentee {get;set;} = string.Empty;
    }
}