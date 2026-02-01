

using Demo_DAL.Models.Department;
using Demo_DAL.Models.Employee;
using Demo_DAL.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Demo_DAL.Data.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)//Use the Second Overload to Add ApplicationUser props to IdentityDbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        // optionsBuilder.UseSqlServer("Connection String");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Use base.OnModelCreating => To Get The All Tables That Generated From IdentityDbContext (AspNetUsers , AspNetRoles ....)
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        //No Need to Declare DbSet for IdentityUser and IdentityRole Because They are already Declared in IdentityDbContext
        //public DbSet<IdentityUser> IdentityUsers { get; set; }
        //public DbSet<IdentityRole> IdentityRoles { get; set; }

    }

}
