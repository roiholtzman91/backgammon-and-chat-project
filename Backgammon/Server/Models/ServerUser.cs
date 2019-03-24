using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public class ServerUser : IUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsGame { get; set; }

        public ServerUser(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}