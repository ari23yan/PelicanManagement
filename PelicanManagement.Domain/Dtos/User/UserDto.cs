using PelicanManagement.Domain.Dtos.Role;
using PelicanManagement.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Dtos.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Username { get; set; }
        public string? Email { get; set; }
        public Guid UserRoleId { get; set; }
        public string Token { get; set; }
        public GetRoleMenuDto RoleMenus { get; set; }
    }
}
