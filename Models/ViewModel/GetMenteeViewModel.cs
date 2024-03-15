using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MYMVC.Models.Entities;

namespace MYMVC.Models.ViewModel
{
    public class GetMenteeViewModel
    {
        public string Id {get; set;}
        public string MentorId {get; set;}
        public string CategoryName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; } = default!;
        public int Age { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public IEnumerable<Meeting> Meetings { get; set; }
    }
}