using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopify.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category Name is required")]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters are allowed")]
        [MinLength(3, ErrorMessage = "Category Name must contain more than 3 characters")]
        public string CategoryName { get; set; }
    }
}
