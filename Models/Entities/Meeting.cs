using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYMVC.Models.Entities
{
    public class Meeting :BaseEntity
    {
        public DateTime DateAndTime {get;set;} = default!;
        public string MenteeId {get;set;} = default!;
        public string MentorId {get;set;} = default!;
        public Mentee Mentees {get;set;}
        public Mentor Mentor {get;set;}
    }
}