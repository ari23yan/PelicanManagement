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
    [Table("Material", Schema = "Prd")]
    public class Material:BaseEntity
    {
        [Required]
        [MaxLength(64)]

        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
