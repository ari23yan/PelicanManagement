using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Domain.Dtos.Management.Pelican
{
    public class PelicanUserUnitsDto
    {
        public int Id { get; set; }
        public int unitId { get; set; }
        public string UnitName { get; set; }
        public bool HaveUnit { get; set; }
    }
}
