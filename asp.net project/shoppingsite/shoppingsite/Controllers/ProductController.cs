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
    public class ProductController : Controller
    {
        ProductRepo pr = new ProductRepo();
        UserRepo ur = new UserRepo();
        HttpCookie mycookie = new HttpCookie("registered");

        public ActionResult AddProduct(ProductRegister product, HttpPostedFileBase image1, HttpPostedFileBase image2,
      HttpPostedFileBase image3)
        {
            if (Request.Cookies["registered"] == null)
                return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                string ownerid = Request.Cookies["registered"]["UserName"];
                Product p = new Product
                {
                    OwnerId = ur.FindOwnerId(ownerid),
                    Date = DateTime.Now,
                    ShortDescription = product.ShortDescription,
                    LongDescription = product.LongDescription,
                    Price = product.Price,
                    Title = product.Title,
                    State = Product.state.valid
                };
                if (image1 != null)
                {
                    p.Picture1 = new byte[image1.ContentLength];
                    image1.InputStream.Read(p.Picture1, 0, image1.ContentLength);
                }
                if (image2 != null)
                {
                    p.Picture2 = new byte[image2.ContentLength];
                    image1.InputStream.Read(p.Picture2, 0, image2.ContentLength);
                }
                if (image3 != null)
                {
                    p.Picture3 = new byte[image3.ContentLength];
                    image1.InputStream.Read(p.Picture3, 0, image3.ContentLength);
                }

                pr.AddProductToDb(p);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult ProductDetails(int? id)
        {
            if (id == null)
                RedirectToAction("Index", "Home");
            Product p = pr.FindProductById(id);
            User u = ur.FindUserById(p.OwnerId);
            ProductDetails pd = new ProductDetails
            {
                Title = p.Title,
                ShortDescription = p.ShortDescription,
                LongDescription = p.LongDescription,
                Price = p.Price,
                Picture1 = p.Picture1,
                Picture2 = p.Picture2,
                Picture3 = p.Picture3,
                birthDate = u.BirthDate,
                Email = u.Email,
                UserName = u.UserName
            };

            return View(pd);

        }

        public ActionResult AddToCart(int id)
        {

            Product p = pr.FindProductById(id);
            if (Request.Cookies["registered"] != null)
            {
                string ownerid = Request.Cookies["registered"]["UserName"];
                int userid = ur.FindOwnerId(ownerid);
                pr.AddToCart(p, userid);
            }
            else
            {
                Session["cart"] = new List<Product>();
                ((List<Product>)Session["cart"]).Add(p);
                pr.AddToCart(p);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult RemoveFromCart(int id)
        {
            pr.RemoveFromCart(id);

            return RedirectToAction("ShowMyCart");

        }

        public ActionResult BuyProduct(int id)
        {
            pr.BuyProduct(id);
            return RedirectToAction("ShowMyCart");
        }

        public ActionResult ShowMyCart()
        {
            IEnumerable<Product> cartlist = new List<Product>();

            if (Request.Cookies["registered"] != null)
            {
                string ownerid = Request.Cookies["registered"]["UserName"];
                int userid = ur.FindOwnerId(ownerid);
                cartlist = pr.ShowMyCart(userid);

            }
            else
            {
                cartlist = pr.ShowMyCart();
            }

            return View(cartlist);

        }     

    }
}
