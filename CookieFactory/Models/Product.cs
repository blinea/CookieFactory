using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CookieFactory.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name Required")]
        [StringLength(40, ErrorMessage = "{0} must be between {2} and {1} characters", MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product Price Required")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Product Category Required")]
        public int CategoryRefId { get; set; }

        [ForeignKey("CategoryRefId")]
        public Category Category { get; set; }

        [Required(ErrorMessage = "Product Description Required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Product Quantity Required")]
        public int Quantity { get; set; }

        public Product()
        {

        }
    }
}
