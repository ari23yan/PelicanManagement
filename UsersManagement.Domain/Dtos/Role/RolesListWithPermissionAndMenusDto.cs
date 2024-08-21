using UsersManagement.Domain.Dtos.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Domain.Dtos.Role
{
    public class RolesListWithPermissionAndMenusDto
    {
        public List<RoleDto> Role { get; set; }
        public List<RoleMenusDto> Menus { get; set; }
        public List<PermissionsDto> Permission { get; set; }
    }
}
