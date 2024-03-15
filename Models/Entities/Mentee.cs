using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYMVC.Models.Entities
{
    public class Mentee : BaseEntity
    {
        public string MentorId {get;set;}
        public string UserId{get;set;}
        public string CategoryId{get;set;}
        public User User {get; set;}
        public Category Category {get; set;}
        public Mentor Mentor {get;set;}
        public IEnumerable<Meeting> Meetings {get;set;}
    }
}