using System;
using System.Collections.Generic;
using System.Text;
using eShop.Domain;
using Microsoft.EntityFrameworkCore;

namespace eShop.DataAccess.EntityFramework.Context
{
    public class OnlineShopContext : DbContext
    {
        public OnlineShopContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Customer> Customers {
            get;
            set;
        }

    }
}
