using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Domain.Dtos.Management.HisClinic
{
    public class AddClinicUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string MedicalNo { get; set; }
    }
}
