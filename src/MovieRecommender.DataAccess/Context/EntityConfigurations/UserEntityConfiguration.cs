using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieRecommender.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommender.DataAccess.Context.EntityConfigurations
{
    public class UserEntityConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(i => i.Username)
               .HasColumnType(MssqlColumnTypes.Nvarchar)
               .HasMaxLength(50)
               .IsRequired();

            builder.HasIndex(i => i.Username).IsUnique();

            builder.Property(i => i.FirstLastName)
                .HasColumnType(MssqlColumnTypes.Nvarchar)
                .HasMaxLength(100)
                .HasDefaultValue(String.Empty)
                .IsRequired();

            builder.HasIndex(i => i.FirstLastName);

            builder.Property(i => i.Email)
                .HasColumnType(MssqlColumnTypes.Nvarchar)
                .HasMaxLength(200)
                .HasDefaultValue(String.Empty)
                .IsRequired();

            builder.HasIndex(i => i.Email).IsUnique();

            builder.Property(i => i.PasswordHash)
                .HasColumnType(MssqlColumnTypes.Binary)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(i => i.PasswordSalt)
                .HasColumnType(MssqlColumnTypes.Binary)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(i => i.IsBlocked)
                .HasColumnType(MssqlColumnTypes.Boolean)
                .HasDefaultValue(false)
                .IsRequired();

            builder.ToTable("Users", MovieDbContext.DEFAULT_SCHEME);
        }
    }
}
