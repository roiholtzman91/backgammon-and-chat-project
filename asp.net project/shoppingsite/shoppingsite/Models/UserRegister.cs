using shoppingsite.validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace shoppingsite.Models
{   
    public class UserRegister
    {
        [Required(ErrorMessage = "please enter first name")]
        [MaxLength(10 , ErrorMessage = "please enter max of 10 charecters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "please enter last name")]
        [MaxLength(20, ErrorMessage = "please enter max of 20 charecters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "please enter date of birth")]
        [DataType(DataType.Date)]        
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "please enter email adress")]
        [DataType(DataType.EmailAddress)]     
        [EmailAddress(ErrorMessage = "invalid email adress")]
        public string Email { get; set; }

        [Required(ErrorMessage = "please enter user name")]
        [RegisterValidation]
        [MaxLength(20 , ErrorMessage = "please enter max of 20 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "please enter password")]
        [DataType(DataType.Password), MaxLength(15, ErrorMessage = "please enter max of 15 characters"),
            MinLength(4, ErrorMessage ="please enter minimum of 4 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "please enter password again")]
        [DataType(DataType.Password), MaxLength(15, ErrorMessage = "please enter max of 15 characters"),
            MinLength(4, ErrorMessage = "please enter minimum of 4 characters"), 
            Compare("Password", ErrorMessage ="password doesnt match")]
        public string PasswordConfirmation { get; set; }
        
    }
}