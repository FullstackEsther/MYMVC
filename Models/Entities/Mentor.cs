using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYMVC.Models.Entities
{
    public class Mentor : BaseEntity
    {
        public string CategoryId{get;set;}
        public string UserId{get;set;}
        public bool IsAvailable{get;set;} = true;
         public int YearsOfExperience {get; set; } = default!;
         public User User {get; set;}
         public Category Category{get; set;}
         public IEnumerable<Mentee> Mentees {get;set;}
         public IEnumerable<Meeting> Meetings {get;set;}
    }
}