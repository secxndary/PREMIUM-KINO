using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PREMIUM_KINO.EFCore.Entities;

namespace PREMIUM_KINO.EFCore.Configs
{
    public class OrdersConfig : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> entity)
        {
            entity.ToTable(nameof(Orders));

            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id_User).IsRequired();
            entity.Property(x => x.Id_Schedule).IsRequired();
            entity.Property(x => x.Number_Of_Seats).IsRequired();
            entity.Property(x => x.Order_Status).IsRequired();

            entity.HasOne(o => o.User).WithMany(u => u.Orders).HasForeignKey(o => o.Id_User);
        }
    }
}
