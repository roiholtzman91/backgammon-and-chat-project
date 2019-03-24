using DalFile.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalFile
{
    public class UserRepo
    {
        public IEnumerable<User> GetAllUsers()
        {
            using (var dbcontext = new SiteContext())
            {
                return dbcontext.Users.ToList();
            }
        }      

        public void AddUserToDb(User user)
        {
            using (var dbcontext = new SiteContext())
            {
                dbcontext.Users.Add(user);
                dbcontext.SaveChanges();
            }
        }

        public bool CheckUserName(string value)
        {
            using (var dbcontext = new SiteContext())
            {
                var user = dbcontext.Users.FirstOrDefault(u => u.UserName == value);
                if (user != null)
                    return false;
                else
                    return true;
            }
        }

        public bool LoginVerification(string username, string password)
        {
            using (var dbcontext = new SiteContext())
            {
                var user = dbcontext.Users.FirstOrDefault(u => u.UserName.Equals(username) && u.Password.Equals(password));
                if (user != null)
                    return true;
                else
                    return false;
            }
        }

        public User FindUser(string username)
        {
            using (var dbcontext = new SiteContext())
            {
                var user = dbcontext.Users.FirstOrDefault(u => u.UserName.Equals(username));
                return user;
            }
        }

        public User FindUserById(int id)
        {
            using (var dbcontext = new SiteContext())
            {
                var user = dbcontext.Users.FirstOrDefault(u => u.Id == id);
                return user;
            }
        }

        public void UpdateUser(User user)
        {
            using (var dbcontext = new SiteContext())
            {
                var item = dbcontext.Users.FirstOrDefault(u => u.UserName == user.UserName);
                if(item != null)
                {
                    item.FirstName = user.FirstName;
                    item.LastName = user.LastName;
                    item.Email = user.Email;
                    item.BirthDate = user.BirthDate;
                };               
                dbcontext.SaveChanges();
            }
        }

        public int FindOwnerId(string username)
        {
            using (var dbcontext = new SiteContext())
            {
                var owner = dbcontext.Users.FirstOrDefault(o => o.UserName == username);
                if (owner != null)
                    return owner.Id;
                else
                return 0;
            }
        }
    }
}
