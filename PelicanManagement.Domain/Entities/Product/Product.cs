using Shop.Domain.Entities.Common;
using Shop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities.Product
{
    [Table("Product", Schema = "Prd")]

    public class Product : BaseEntity
    {
        [MaxLength(256)]
        public string Name { get; set; }
        [MaxLength(64)]
        public string ProductCode { get; set; }
        public bool IsAvailable { get; set; } = true;
        public bool IsTopSeller { get; set; } = false;
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int MaterialId { get; set; }
        public Material Material { get; set; }
        public List<Gallery> Gallery { get; set; }
    }
}
