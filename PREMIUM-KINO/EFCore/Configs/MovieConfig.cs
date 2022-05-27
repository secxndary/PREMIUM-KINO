using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PREMIUM_KINO.EFCore.Entities;

namespace PREMIUM_KINO.EFCore.Configs
{
    public class MovieConfig : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> entity)
        {
            entity.ToTable(nameof(Movie));

            entity.HasKey(x => x.Id);
            entity.Property(x => x.Title).IsRequired().HasMaxLength(128);
            entity.Property(x => x.Director).IsRequired().HasMaxLength(128);
            entity.Property(x => x.Genre).IsRequired().HasMaxLength(64);
            entity.Property(x => x.Duration).IsRequired();
            entity.Property(x => x.Rating).HasColumnType("real");
            entity.Property(x => x.Photo).IsRequired();

            entity.HasMany(x => x.Schedule);
        }
    }
}
