using DalFile.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalFile
{
    public class ProductRepo
    {
        public IEnumerable<Product> GetAllProducts()
        {
            using (var dbcontext = new SiteContext())
            {
                List<Product> prodlist = new List<Product>();
                prodlist = dbcontext.Products.Where(p => p.State == Product.state.valid).ToList();
                return prodlist;
            }
        }

        public void AddProductToDb(Product prooduct)
        {
            using (var dbcontext = new SiteContext())
            {
                dbcontext.Products.Add(prooduct);
                dbcontext.SaveChanges();
            }
        }

        public Product FindProductById(int? id)
        {
            using (var dbcontext = new SiteContext())
            {
                var prod = dbcontext.Products.FirstOrDefault(p => p.Id == id);
                return prod;
            }
        }

        public void BuyProduct(int id)
        {
            using (var dbcontext = new SiteContext())
            {
                Product product = dbcontext.Products.Find(id);
                product.State = Product.state.sold;
                dbcontext.SaveChanges();
            }

        }

        public Product AddToCart(Product product1, int id = -1)
        {

            using (var dbcontext = new SiteContext())
            {
                Product productToCart = dbcontext.Products.FirstOrDefault(p => p.Id == product1.Id);

                if (productToCart != null)
                {
                    productToCart.State = Product.state.incart;
                    productToCart.AddToCartDate = DateTime.Now;
                    productToCart.UserId = id;
                    dbcontext.SaveChanges();
                    return productToCart;
                }
                return default(Product);
            }
        }

        public IEnumerable<Product> ShowMyCart(int userid = -1)
        {
            IEnumerable<Product> cartlist;
            using (var dbcontext = new SiteContext())
            {
                cartlist = dbcontext.Products.Where(p => p.UserId == userid && p.State == Product.state.incart).ToList();
            }

            return cartlist;
        }

        public void RemoveFromCart(int id)
        {
            using (var dbcontext = new SiteContext())
            {
                Product product = dbcontext.Products.Find(id);
                product.State = Product.state.valid;
                dbcontext.SaveChanges();
            }
        }

        public void CheckTimeInCart()
        {
            int minute = DateTime.Now.Minute;

            using (var dbcontext = new SiteContext())
            {
                var prod = dbcontext.Products.Where(p => minute - p.AddToCartDate.Value.Minute > 10);
                foreach (var item in prod)
                {
                    item.State = Product.state.valid;
                    item.UserId = null;                    
                }
                dbcontext.SaveChanges();
            }
        }

        public IEnumerable<Product> SortByName()
        {
            IEnumerable<Product> plist = GetAllProducts();
            plist.OrderBy(p => p.Title);
            return plist;
        }

    }
}
