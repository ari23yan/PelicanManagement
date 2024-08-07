using PelicanManagement.Domain.Dtos.Management.Pelican;
using PelicanManagement.Domain.Entities.IdentityServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Dtos.Management.IdentityServer
{
    public class IdentityUserDetailDto
    {
        public PelicanManagement.Domain.Entities.IdentityServer.User User { get; set; }
        public List<PelicanUserUnitsDto> UserUnits { get; set; }
        public List<PelicanUserPermissionsDto> UserPermissions { get; set; }

    }
}
