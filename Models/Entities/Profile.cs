using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYMVC.Models.Entities
{
    public class Profile :BaseEntity
    {
        public string UserId{get;set;}
        public int Age {get; set;} = default!;
        public string FirstName {get; set;} = default!;
        public string LastName {get; set;} = default!;
        public string Address{get; set;} = default!;
        public string PhoneNumber{get; set;} = default!;
        public User User {get;set;}
    }
}