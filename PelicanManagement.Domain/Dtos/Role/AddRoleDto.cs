using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Dtos.Role
{
    public class AddRoleDto
    {
        public string RoleName { get; set; }
        public string RoleName_Farsi { get; set; }
        public string? Description { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public Guid? CreatedBy { get; set; }
        public Guid[] PermissionIds { get; set; }
        public Guid[] MenuIds { get; set; }
    }
}
