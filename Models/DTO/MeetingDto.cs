using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MYMVC.Models.DTO
{
    public class MeetingDto
    {
        public string Id {get; set;}
        public DateTime DateAndTime { get; set; } = default!;
        public string MenteeUserName { get; set; } = default!;
        public string MentorUserName { get; set; } = default!;
    }
    public class MeetingRequestModel
    {
        public DateTime DateAndTime { get; set; } = default!;
         [Required(ErrorMessage ="This field is required")]
        public string MenteeId { get; set; } = default!;
         [Required(ErrorMessage ="This field is required")]
        public string MentorId { get; set; } = default!;
    }
    public class UpdateMeeting
    {
        public string Id {get; set;}
        public DateTime DateAndTime { get; set; } = default!;
    }
}