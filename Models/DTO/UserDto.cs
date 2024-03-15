using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MYMVC.Models.Entities;

namespace MYMVC.Models.DTO
{
    public class UserDto
    {
        public string Id {get; set;}
        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string RoleName { get; set; } = default!;
        public string? CategoryId {get;set;}
        public string? MentorId {get;set;}
        public string? MenteeId {get;set;}
    }
    public class UserRequestModel
    {
        [EmailAddress(ErrorMessage ="Invalid Email Format")]
        [Required]
        public string Email { get; set; } = default!;
        [MinLength(3, ErrorMessage ="Password too short, minimum of 3 characters")]
        [Required(ErrorMessage ="This field is required")]
        public string Password { get; set; } = default!;
        [Required(ErrorMessage ="This field is required")]
        public string UserName { get; set; } = default!;
    }
}