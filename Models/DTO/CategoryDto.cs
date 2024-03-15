using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MYMVC.Models.DTO
{
    public class CategoryDto
    {
        public string Id {get; set;}
        public string CategoryName { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
    public class CategoryRequestModel
    {
        [Required(ErrorMessage ="This field is required")]
        public string CategoryName { get; set; } = default!;
        public string Description { get; set; }
    }
}