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
    [Table("Category", Schema = "Prd")]

    public class Category: BaseEntity
    {
        [Required]
        [MaxLength(64)]

        public string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
        public List<Category> Subcategories { get; set; }
        public List<Product> Products { get; set; }
    }
}
