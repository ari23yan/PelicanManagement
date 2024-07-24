using Shop.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities.Account
{
    public class LoyaltyClubUser:BaseEntity
    {
        [MaxLength(64)]
        public string? MobileNumber { get; set; }
        [MaxLength(64)]
        public string? Email { get; set; }
    }
}
