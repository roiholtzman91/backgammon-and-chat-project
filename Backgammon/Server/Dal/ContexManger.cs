using Common.Interfaces;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Dal
{
    public class ContexManger
    {
        internal void AddUser(ServerUser user)
        {
            using (var dbcontext = new DBcontext())
            {
                dbcontext.UserTable.Add(user);
                dbcontext.SaveChanges();
            }
        }

        internal bool IsUserExist(ServerUser user)
        {
            using (var dbcontext = new DBcontext())
            {
                return dbcontext.UserTable.Any(U => user.UserName == U.UserName && user.Password == U.Password);
            }
        }
    }
}