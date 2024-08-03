using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Dtos.Account
{
    public class ForgetPasswordDto
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
