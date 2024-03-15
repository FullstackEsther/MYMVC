using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MYMVC.Models.DTO
{
    public class ProfileDto
    {
        public string Id {get; set;}
        public int Age {get; set;} = default!;
        public string FirstName {get; set;} = default!;
        public string LastName {get; set;} = default!;
        public string Address{get; set;} = default!;
        public string PhoneNumber{get; set;} = default!;
    }
    public class ProfileRequestModel
    {
        [Required]
        public int Age {get; set;} = default!;
        [Required]
        public string FirstName {get; set;} = default!;
        [Required]
        public string LastName {get; set;} = default!;
        [Required]
        public string Address{get; set;} = default!;
        [Phone]
        public string PhoneNumber{get; set;} = default!;
    }
    public class UpdateProfileRequestModel
    {
        
        public string UserName {get; set;}
        public int Age {get; set;} = default!;
        public string FirstName {get; set;} = default!;
        public string LastName {get; set;} = default!;
        public string Address{get; set;} = default!;
        public string PhoneNumber{get; set;} = default!;
    }
}