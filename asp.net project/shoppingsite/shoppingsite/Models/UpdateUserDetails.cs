using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace shoppingsite.Models
{
    public class UpdateUserDetails
    {
        [Required(ErrorMessage = "please enter first name")]
        [MaxLength(10, ErrorMessage = "please enter max of 10 charecters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "please enter last name")]
        [MaxLength(20, ErrorMessage = "please enter max of 20 charecters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "please enter date of birth")]
        [DataType(DataType.DateTime)]
        [DisplayName("BirthDate")]
        public DateTime BirthDate1 { get; set; }

        [Required(ErrorMessage = "please enter email adress")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "invalid email adress")]
        public string Email { get; set; }
    }
}