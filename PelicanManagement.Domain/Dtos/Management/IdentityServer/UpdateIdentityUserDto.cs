using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Dtos.Management.IdentityServer
{
    public class UpdateIdentityUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string PersonalCode { get; set; }
        public long[] PermissionIds { get; set; }
        public int[] UnitIds { get; set; }
    }
}
