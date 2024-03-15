using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MYMVC.Models.Entities
{
    public class User: BaseEntity
    {
        public string? CommunityId{get;set;}
        public Community Community {get;set;}
        public string? RoleId{get;set;}
        public string Email {get;set;} = default!;
        public string Password {get;set;} = default!;
        public string UserName {get;set;} = default!;
        public Role Role {get;set;}
        public Profile Profile {get;set;}
        public Mentee Mentee{get;set;}
        public Mentor Mentor{get;set;}

    }
}