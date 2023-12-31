using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatCore.Entities.OrderAggregate;
using Order = TalabatCore.Entities.OrderAggregate.Order;

namespace TalabatRepository.Context.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O=>O.ShipingAddress,NP=>NP.WithOwner());
            builder.Property(o=>o.status).HasConversion(
                ostatus=>ostatus.ToString(),
                ostatus=>(OrderStatus)Enum.Parse(typeof(OrderStatus),ostatus));
            builder.HasMany(o => o.Items).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Property(O => O.SubTotal).HasColumnType("decimal(18,2)");

        }
    }
}
