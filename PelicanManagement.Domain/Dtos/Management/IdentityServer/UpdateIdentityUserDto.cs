using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Dtos.Management.IdentityServer
{
    public class UpdateIdentityUserDto
    {
        public string Password { get; set; }
        public string PersoanlCode { get; set; }
    }
}
