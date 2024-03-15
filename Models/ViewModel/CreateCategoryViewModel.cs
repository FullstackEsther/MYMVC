using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYMVC.Models.ViewModel
{
    public class CreateCategoryViewModel
    {
        public string CategoryName { get; set; } = default!;
        public string Description { get; set; }
    }
}