using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieRecommender.Core.Entities;

namespace MovieRecommender.DataAccess.Context.EntityConfigurations
{
    public class MovieRatingEntityConfiguration : BaseEntityConfiguration<MovieRating>
    {
        public override void Configure(EntityTypeBuilder<MovieRating> builder)
        {
            base.Configure(builder);

            builder.Property(i => i.Note)
                .HasColumnType(MssqlColumnTypes.Nvarchar)
                .HasMaxLength(4000)
                .HasDefaultValue(String.Empty)
                .IsRequired();

            builder.Property(i => i.Rate)
                .HasColumnType(MssqlColumnTypes.Int)
                .IsRequired();

            builder.HasOne(i => i.Movie)
                .WithMany(i => i.MovieRatings)
                .HasForeignKey(i => i.MovieId);

            builder.HasOne(i => i.User)
                .WithMany(i => i.MovieRatings)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("MovieRatings", MovieDbContext.DEFAULT_SCHEME);
        }
    }
}
