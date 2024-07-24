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
    [Table("ProductDetail", Schema = "Prd")]

    public class ProductDetail
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public ProductColor? Color { get; set; }
        [MaxLength(512)]
        public string? Description { get; set; }
        public int? Size { get; set; }
        public decimal? Weight { get; set; }
        [MaxLength(512)]
        public string? Labels { get; set; }
        [MaxLength(256)]
        public string? PurchaseLink { get; set; }

    }
}
