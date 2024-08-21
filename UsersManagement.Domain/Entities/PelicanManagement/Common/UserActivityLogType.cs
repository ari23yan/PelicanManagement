using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Domain.Entities.UsersManagement.Common
{
    [Table("UserActivityLogTypes", Schema = "Common")]

    public class UserActivityLogType
    {
        public long Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public string NameFa { get; set; }
    }
}
