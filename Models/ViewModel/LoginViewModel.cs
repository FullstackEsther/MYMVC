using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYMVC.Models.ViewModel
{
    public class LoginViewModel
    {
        public string Password { get; set; } = default!;
        public string UserName { get; set; } = default!;
    }
}