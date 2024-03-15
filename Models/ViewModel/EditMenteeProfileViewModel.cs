using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MYMVC.Models.ViewModel
{
    public class EditMenteeProfileViewModel
    {
        public string Email { get; set; } = default!;
        public string MentorId { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public int Age { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
    }
}