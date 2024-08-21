using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Domain.Dtos.Sender
{
    public class SendSmsDto
    {
        public string PhoneNumber { get; set; }
        public string Code { get; set; }
        public string Username { get; set; }
    }
}
