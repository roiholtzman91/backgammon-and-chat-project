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
    public class HomeController : Controller
    {
        ProductRepo pr = new ProductRepo();
        UserRepo ur = new UserRepo();
        HttpCookie mycookie = new HttpCookie("registered");

        public ActionResult Index()
        {
            IEnumerable<Product> productlist = new List<Product>();
            productlist = pr.GetAllProducts();
            return View(productlist);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult SortByName()
        {
            IEnumerable<Product> prodlist = pr.SortByName();

            return View("Index", prodlist);
        }

    }
}