using DalFile;
using DalFile.model;
using shoppingsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoppingsite.Controllers
{
    public class UserController : Controller
    {
        UserRepo ur = new UserRepo();
        HttpCookie mycookie = new HttpCookie("registered");

        public ActionResult Register(UserRegister users)
        {
            if (ModelState.IsValid)
            {
                User u = new User
                {
                    FirstName = users.FirstName,
                    BirthDate = users.BirthDate,
                    Email = users.Email,
                    LastName = users.LastName,
                    Password = users.Password,
                    UserName = users.UserName
                };
                ur.AddUserToDb(u);
                mycookie["UserName"] = u.UserName;
                mycookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(mycookie);
                return RedirectToAction("Index", "Home");
            }
            return View(new UserRegister());            
        }

        public ActionResult LoginVerify(SignIn user)
        {
            if (ModelState.IsValid)
            {
                User datauser = new User { UserName = user.NameOfUser, Password = user.PassKey };


                if (ur.LoginVerification(datauser.UserName, datauser.Password))
                {
                    mycookie["UserName"] = user.NameOfUser;
                    mycookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(mycookie);
                }
                else
                {
                    ViewBag.ErrorLogin = "one of the details is incorrect";
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOFF()
        {
            mycookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(mycookie);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LoadUserDetails()
        {
            string username = Request.Cookies["registered"]["UserName"];
            User user = ur.FindUser(username);
            UpdateUserDetails ud = new UpdateUserDetails
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate1 = user.BirthDate,
                Email = user.Email
            };
            return View(ud);
        }

        public ActionResult UpdateUserDetails(UpdateUserDetails ud)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = Request.Cookies["registered"]["UserName"],
                    FirstName = ud.FirstName,
                    LastName = ud.LastName,
                    BirthDate = ud.BirthDate1,
                    Email = ud.Email
                };
                ur.UpdateUser(user);
                ViewBag.Success = "details updated";
                return RedirectToAction("Index", "Home");
            }

           return View("Index", "Home");
        }
    }
}