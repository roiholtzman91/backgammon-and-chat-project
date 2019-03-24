using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace shoppingsite.Models
{
    public class ProductRegister
    {
        [Required(ErrorMessage ="please enter title")]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "please enter Short Description")]
        [MaxLength(200)]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "please enter Long Description")]
        [MaxLength(500)]
        public string LongDescription { get; set; }

        [Required(ErrorMessage = "please enter Price")]
        public double Price { get; set; }
    }
}