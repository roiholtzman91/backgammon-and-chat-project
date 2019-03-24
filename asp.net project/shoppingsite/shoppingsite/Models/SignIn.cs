using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace shoppingsite.Models
{
    public class SignIn
    {
        [DisplayName("User Name")]    
        public string NameOfUser { get; set; }
        [DisplayName("Password")]
        public string PassKey { get; set; }
    }
}