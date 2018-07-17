using System;
using System.Collections.Generic;
using System.Text;
using eShop.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.DataAccess.EntityFramework.Configuration
{
    class OrderConfig :IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne<Customer>();
            builder.HasMany<OrderItem>();
        }
    }
}
