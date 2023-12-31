using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatCore.Entities.OrderAggregate;

namespace TalabatRepository.Context.Config
{
    public class OderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(O => O.ProductItemOrder, NP => NP.WithOwner());
            builder.Property(O => O.Price).HasColumnType("decimal(18,2)");
            
        }
    }
}
