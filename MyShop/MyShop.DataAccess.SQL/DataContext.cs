using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.SQL
{
    public class DataContext: DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> PorductCategories { get; set; }

        // now need to start to migration in View -> Other windows -> Package Manager Console:
            //Enable-Migrations
            //Add-Migration InitialMigration
            //Update-Database

    }
}
