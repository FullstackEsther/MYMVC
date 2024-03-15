using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYMVC.Models.Entities
{
    public class Role : BaseEntity
    {
        public string RoleName {get;set;} = default!;
        public string Description {get;set;}= default!;
        public IEnumerable<User> Users {get;set;}
    }
}