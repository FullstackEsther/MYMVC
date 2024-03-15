using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYMVC.Models.DTO
{
    public class RoleDto
    {
        public string Id {get; set;}
        public string RoleName { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
    public class RoleRequestModel
    {
        public string RoleName { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}