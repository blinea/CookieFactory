using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CookieFactory.Models
{
    public class Email
    {
        [Key]
        public int EmailId { get; set; }

        [Required(ErrorMessage = "Username Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email Required")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Message Required")]
        public string Message { get; set; }

        public string Attatchment { get; set; }

    }
}
