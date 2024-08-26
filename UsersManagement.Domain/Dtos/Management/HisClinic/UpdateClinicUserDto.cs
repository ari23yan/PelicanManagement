using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Domain.Dtos.Management.HisClinic
{
    public class UpdateClinicUserDto
    {
        public string Password { get; set; }
        public string MedicalNo { get; set; }
        public string UserName { get; set; }
    }
}
