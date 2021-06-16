using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CookieFactory.Models
{
    public class Cookie
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Text Required")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Cookie Flavor Required")]
        public string CookieFlavor { get; set; }

        [Required(ErrorMessage = "Chips Required")]
        public string Chips { get; set; }

        [Required(ErrorMessage = "Filling Required")]
        public string Filling { get; set; }

        [Required(ErrorMessage = "Topping Required")]
        public string Topping { get; set; }

        [Required(ErrorMessage = "Image Required")]
        public string Image { get; set; }

        public Cookie()
        {

        }
    }
}
