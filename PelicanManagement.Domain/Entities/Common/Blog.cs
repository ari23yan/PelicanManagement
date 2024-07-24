using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities.Common
{
    [Table("Blog", Schema = "Cmn")]

    public class Blog:BaseEntity
    {
        [MaxLength(128)]
        public string Tittle { get; set; }
        public string Description { get; set; }
        [MaxLength(256)]
        public string Image { get; set; }
        public int ReadingTime { get; set; }
        [MaxLength(128)]
        public string Labels { get; set; }
        [MaxLength(64)]
        public string? Autor { get; set; }
    }
}
