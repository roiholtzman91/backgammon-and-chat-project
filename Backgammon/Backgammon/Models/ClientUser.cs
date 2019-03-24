using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.Models
{
    public class ClientUser : IUser
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        
        public string Password { get; set; }

        public bool IsGame { get; set; }
     
    }
}
