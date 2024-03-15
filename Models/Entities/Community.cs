using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MYMVC.Models.Entities;

namespace MYMVC.Models
{
    public class Community : BaseEntity
    {
        public string CommunityName {get;set;}
        public IEnumerable<User> CommunityMembers {get;set;}
    }
}