using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Entities.IdentityServer
{
    public class User : IdentityUser<int>
    {
        public string? PersonalCode { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
