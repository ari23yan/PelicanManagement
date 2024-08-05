using Microsoft.AspNetCore.Identity;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Dtos.Management.IdentityServer
{
    public class AddIdentityUserDto: IdentityUser
    {
        public string UserName { get; set; }
        public string Firsname { get; set; }
        public string LastName { get; set; }
        public string PersonalCode { get; set; }
        public string Password { get; set; }
    }
}
