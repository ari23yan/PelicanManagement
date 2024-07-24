using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Dtos.Sender
{
    public record SendMailDto
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string Username { get; set; }
    }
}
