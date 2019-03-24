using DalFile.model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalFile
{
    public class SiteContext : DbContext
    {
        public SiteContext(): base("BuyDB")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
