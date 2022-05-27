using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PREMIUM_KINO.EFCore.Entities;


namespace PREMIUM_KINO.EFCore.Configs
{
    public class ScheduleConfig : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> entity)
        {
            entity.ToTable(nameof(Schedule));

            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id_Movie).IsRequired();
            entity.Property(x => x.DateTime).IsRequired();
            entity.Property(x => x.Aviable_Seats).IsRequired();

            entity.HasOne(o => o.Movie).WithMany(u => u.Schedule).HasForeignKey(o => o.Id_Movie);
        }
    }
}
