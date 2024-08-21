using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Domain.Dtos.Account
{
    public class ConfrimOtpDto
    {
        public string PhoneNumber { get; set; }
        public string Otp { get; set; }
    }
}
