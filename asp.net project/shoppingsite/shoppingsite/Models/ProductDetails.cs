using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shoppingsite.Models
{
    public class ProductDetails
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public double Price { get; set; }
        public DateTime? birthDate { get; set; }
        public byte[] Picture1 { get; set; }
        public byte[] Picture2 { get; set; }
        public byte[] Picture3 { get; set; }      
        public string Email { get; set; }
        public string UserName { get; set; }        
    }
}