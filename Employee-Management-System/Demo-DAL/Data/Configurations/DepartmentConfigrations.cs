
using Demo_DAL.Models.Department;
using Microsoft.EntityFrameworkCore;
namespace Demo_DAL.Data.Configurations
{
    internal class DepartmentConfigrations : BaseEntityConfiguration<Department>, IEntityTypeConfiguration<Department>
    {
        public new void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Department> builder)
        {
            builder.Property(d=>d.Id).UseIdentityColumn(10,10);
            builder.Property(d => d.Name).HasColumnType("varchar(20)");
            builder.Property(d => d.Code).HasColumnType("varchar(20)");        
            base.Configure(builder);
            builder.HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
