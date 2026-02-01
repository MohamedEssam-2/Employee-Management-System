using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_DAL.Models.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo_DAL.Data.Configurations
{
    internal class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(d => d.Created_On).HasDefaultValueSql("getdate()");
            builder.Property(d => d.Modified_On).HasComputedColumnSql("getdate()");
        }
    }
}
