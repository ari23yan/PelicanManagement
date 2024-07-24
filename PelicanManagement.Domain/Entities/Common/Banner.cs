using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities.Common
{
    [Table("Banner", Schema = "Cmn")]
    public class Banner:BaseEntity
    {
        [MaxLength(128)]
        public string Tittle { get; set; }
        [MaxLength(256)]
        public string Image { get; set; }
        [MaxLength(500)]
        public string Url { get; set; }
    }
}
