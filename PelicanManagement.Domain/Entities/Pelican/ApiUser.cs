using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Entities.Pelican
{
    public class ApiUser
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? FirstName { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? PersonalCode { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
