using UsersManagement.Domain.Dtos.Role;
using UsersManagement.Domain.Entities.UsersManagement.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Domain.Dtos.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName
        {
            get
            {
                return (FirstName ?? string.Empty) + " " + (LastName ?? string.Empty);
            }
        }
        public string? Email { get; set; }
        public Guid RoleId { get; set; }
        public string Token { get; set; }
        //public RoleMenuDto RoleMenus { get; set; }
    }
}
