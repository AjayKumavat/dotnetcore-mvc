using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopify.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters are allowed")]
        [MinLength(3, ErrorMessage = "Product Name must contain more than 3 letters")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product must belong to a category")]
        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
