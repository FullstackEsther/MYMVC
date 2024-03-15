using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MYMVC.Models.Entities;

namespace MYMVC.Models.DTO
{
    public class MenteeDto
    {
        public string Id {get; set;}
        public string MentorId {get; set;}
        public string CategoryName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; } = default!;
        public int Age { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public IEnumerable<Meeting> Meetings { get; set; }

    }
    public class MenteeRequestModel
    {
         [Required(ErrorMessage ="This field is required")]
        public string CategoryId { get; set; }
        [EmailAddress(ErrorMessage ="Invalid Email Format")]
        [Required]
        public string Email { get; set; } = default!;
        [MinLength(3, ErrorMessage ="Password too short, minimum of 3 characters")]
        [Required(ErrorMessage ="This field is required")]
        public string Password { get; set; } = default!;
        [Required(ErrorMessage ="This field is required")]
        public string UserName { get; set; } = default!;
        [Required(ErrorMessage ="This field is required")]
        public int Age { get; set; } = default!;
        [Required(ErrorMessage ="This field is required")]
        public string FirstName { get; set; } = default!;
        [Required(ErrorMessage ="This field is required")]
        public string LastName { get; set; } = default!;
        [Required(ErrorMessage ="This field is required")]
        public string Address { get; set; } = default!;
        [Phone]
        public string PhoneNumber { get; set; } = default!;
    }
    public class MenteeUpdateRequestModel
    {
        public string Email {get; set;}
        public string UserName {get; set;}
        public int Age {get; set;} = default!;
        public string FirstName {get; set;} = default!;
        public string LastName {get; set;} = default!;
        public string Address{get; set;} = default!;
        public string PhoneNumber{get; set;} = default!;
    }

}