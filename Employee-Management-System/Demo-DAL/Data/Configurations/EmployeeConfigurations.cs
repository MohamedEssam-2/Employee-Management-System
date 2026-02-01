using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_DAL.Models.Employee;
using Demo_DAL.Models.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo_DAL.Data.Configurations
{
    internal class EmployeeConfigurations : BaseEntityConfiguration<Employee>, IEntityTypeConfiguration<Employee>

    {
        public new void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name).HasColumnType("varchar(50)");
            builder.Property(e => e.Address).HasColumnType("varchar(50)");
            builder.Property(e=>e.Salary).HasColumnType("decimal(10,2)");
            builder.Property(e =>e.Gender).HasConversion(empGender=>empGender.ToString(),gender=>(Gender)Enum.Parse(typeof(Gender), gender));
            builder.Property(e => e.EmployeeType).HasConversion(empType => empType.ToString(), type => (EmployeeType)Enum.Parse(typeof(EmployeeType), type));
            base.Configure(builder);

        }
    }
}
