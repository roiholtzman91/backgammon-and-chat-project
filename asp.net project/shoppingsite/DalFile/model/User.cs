﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DalFile.model
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName  { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }       
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
