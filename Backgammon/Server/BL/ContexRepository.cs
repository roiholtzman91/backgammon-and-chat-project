using Common.Interfaces;
using Server.Dal;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.BL
{
    public class ContexRepository
    {
        ContexManger Manger = new ContexManger();

        public bool Register(ServerUser user)
        {
            if (Manger.IsUserExist(user))
            {
                return false;
            }
            else
            {
                Manger.AddUser(new ServerUser(user.UserName, user.Password));
                return true;
            }

        }

        public bool IsUserExist(ServerUser user)
        {
            return (Manger.IsUserExist(user));
        }
    }
}