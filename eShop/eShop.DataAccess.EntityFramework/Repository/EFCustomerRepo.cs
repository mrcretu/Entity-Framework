using System;
using System.Collections.Generic;
using System.Text;
using eShop.DataAccess.EntityFramework.Context;
using eShop.Domain;
using Microsoft.EntityFrameworkCore;

namespace eShop.DataAccess.EntityFramework.Repository
{
    public class EFCustomerRepo : EFRepo<Customer>
    {
        public EFCustomerRepo(OnlineShopContext context) : base(context)
        {
        }
    }
}
