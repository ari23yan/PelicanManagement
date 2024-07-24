using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Dtos.Account
{
    public class AuthenticateDto
    {
        public string Input { get; set; } // Mobile Or Username Or Email
        public string Password { get; set; }
    }
}
