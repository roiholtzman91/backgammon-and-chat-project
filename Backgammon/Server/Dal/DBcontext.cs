using Common.Interfaces;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Server.Dal
{
    public class DBcontext : DbContext
    {
        public DbSet<ServerUser> UserTable { get; set; }

        public DBcontext() : base("name=BackgammonUsersDB")
        {
           
        }
    }
}