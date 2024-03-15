using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYMVC.Models.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName{get; set;} = default!;
        public string Description{get; set;} = default!;
        public IEnumerable<Mentee> Mentee{get;set;}
        public IEnumerable<Mentor> Mentor{get;set;}
    }
}