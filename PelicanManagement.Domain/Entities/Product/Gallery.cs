using Shop.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities.Product
{
    [Table("Gallery", Schema = "Prd")]

    public class Gallery : BaseEntity
    {
        [Required]
        [MaxLength(256)]
        public string ImageName { get; set; }
        public bool IsMainImage { get; set; } = false;
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
