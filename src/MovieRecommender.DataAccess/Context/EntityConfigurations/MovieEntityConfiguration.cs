using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieRecommender.Core.Entities;

namespace MovieRecommender.DataAccess.Context.EntityConfigurations
{
    public class MovieEntityConfiguration : BaseEntityConfiguration<Movie>
    {
        public override void Configure(EntityTypeBuilder<Movie> builder)
        {
            base.Configure(builder);

            builder.Property(i => i.Popularity)
                .HasColumnType(MssqlColumnTypes.Double)
                .HasDefaultValue(0m)
                .IsRequired();

            builder.Property(i => i.Adult)
                .HasColumnType(MssqlColumnTypes.Boolean)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(i => i.Overview)
                .HasColumnType(MssqlColumnTypes.Nvarchar)
                .HasMaxLength(4000)
                .HasDefaultValue(String.Empty)
                .IsRequired();

            builder.Property(i => i.PosterPath)
                .HasColumnType(MssqlColumnTypes.Nvarchar)
                .HasMaxLength(200)
                .HasDefaultValue(String.Empty)
                .IsRequired();

            builder.Property(i => i.BackdropPath)
                .HasColumnType(MssqlColumnTypes.Nvarchar)
                .HasMaxLength(200)
                .HasDefaultValue(String.Empty)
                .IsRequired();

            builder.Property(i => i.OriginalLanguage)
                .HasColumnType(MssqlColumnTypes.Nvarchar)
                .HasMaxLength(200)
                .HasDefaultValue(String.Empty)
                .IsRequired();

            builder.Property(i => i.OriginalTitle)
                .HasColumnType(MssqlColumnTypes.Nvarchar)
                .HasMaxLength(500)
                .HasDefaultValue(String.Empty)
                .IsRequired();

            builder.Property(i => i.ReleaseDate)
                .HasColumnType(MssqlColumnTypes.SmallDateTime)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired(false);

            builder.Property(i => i.Title)
                .HasColumnType(MssqlColumnTypes.Nvarchar)
                .HasMaxLength(500)
                .HasDefaultValue(String.Empty)
                .IsRequired();

            builder.Property(i => i.Video)
                .HasColumnType(MssqlColumnTypes.Boolean)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(i => i.TmdbId)
                .HasColumnType(MssqlColumnTypes.Int)
                .HasDefaultValue(0)
                .IsRequired(false);

            builder.ToTable("Movies", MovieDbContext.DEFAULT_SCHEME);
        }
    }
}
