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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalCode { get; set; }
        public string Password { get; set; }
        public long[] PermissionIds { get; set; }
        public int[] UnitIds { get; set; }
        public string Email { get; set; } = null;
        public string NormalizedEmail { get; set; } = null;
        public string PhoneNumber { get; set; } = null;
        public bool EmailConfirmed { get; set; } = false;
        public bool PhoneNumberConfirmed { get; set; } = false;
        public bool TwoFactorEnabled { get; set; } = false;
        public DateTime? LockoutEnd { get; set; } = null;
    }
}
