using DalFile;
using shoppingsite.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace shoppingsite.validation
{
    public class RegisterValidationAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {        
            if(value!=null)
            {
                UserRepo ur = new UserRepo();
                return (ur.CheckUserName(value.ToString()));
            }
            return false;
        }
    }
}