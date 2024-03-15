using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MYMVC.Models.Entities;

namespace MYMVC.Models.DTO
{
    public class MentorDto
    {
        public string Id {get; set;}
        public string CategoryName { get; set; }
        public string UserName { get; set; }
        public bool IsAvailable { get; set; }
        public int YearsOfExperience { get; set; } = default!;
        public string Email { get; set; } = default!;
        public int Age { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public IEnumerable<Mentee> Mentees { get; set; }
        public IEnumerable<Meeting> Meetings { get; set; }
    }
    public class MentorRequestModel
    {
        public string CategoryId { get; set; }
        public int YearsOfExperience { get; set; } = default!;
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
    public class MentorUpdateRequestModel
    { 
        public string Email {get; set;}
        public string UserName {get; set;}
        public int Age {get; set;} = default!;
        public string FirstName {get; set;} = default!;
        public string LastName {get; set;} = default!;
        public string Address{get; set;} = default!;
        public string PhoneNumber{get; set;} = default!; 
        public int YearsOfExperience { get; set; } = default!;
    }
}