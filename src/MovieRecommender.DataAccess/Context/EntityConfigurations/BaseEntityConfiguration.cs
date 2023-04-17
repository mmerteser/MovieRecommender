using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MovieRecommender.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommender.DataAccess.Context.EntityConfigurations
{
    public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id)
                .IsClustered();

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.CreatedDate)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnType(MssqlColumnTypes.DateTime)
                .HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.UpdatedDate)
                .ValueGeneratedOnUpdate()
                .IsRequired()
                .HasColumnType(MssqlColumnTypes.DateTime)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
