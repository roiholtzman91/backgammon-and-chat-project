using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DalFile.model
{
    public class Product
    {
        public enum state {valid, sold, incart }
        public state State { get; set; }    
        public int Id { get; set; }

        public int OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public User Owner { get; set; }

        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }   
        
        public string Title { get; set; }       
        public string ShortDescription { get; set;}     
        public string LongDescription { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? AddToCartDate { get; set; }
        public double Price { get; set; }
        public byte[] Picture1 { get; set; }
        public byte[] Picture2 { get; set; }
        public byte[] Picture3 { get; set; }
    }
}
