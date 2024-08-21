using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Domain.Dtos.Role
{
    public class UpdateRoleDto
    {
        public string RoleName { get; set; }
        public string RoleName_Farsi { get; set; }
        public string? Description { get; set; }
        public Guid[] PermissionIds { get; set; }
        public Guid[] MenuIds { get; set; }
    }
}
