using Demo_DAL.Data.Context;
using Microsoft.EntityFrameworkCore;
using Demo_DAL.Data.Repositories.Interfaces;
using Demo_DAL.Data.Repositories.Classes;
using Demo_BLL.Services.Classes;
using Demo_BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Demo_BLL.Profiles;
using Microsoft.AspNetCore.Mvc;
using Demo_BLL.Attachment.Interface;
using Demo_BLL.Attachment.Class;
using Demo_DAL.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;

namespace Demo.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

            });

            // Register DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies();
            });

            // Register Repositories

            #region Services Lifetime
            //Services lifetime
            //Transient => DI Create One Object each time is requested , Create Many Objects With Diffrent Versions 
            //Scoped    => DI Create One Object Per Request Thats Mean All operations within the same request share the same object
            //Singlton => DI Create Only One Object Per App and Shared Across this app
            //AddDbContext  => this is a Special Type of Services for Context only and it works internally with scoped Lifetime Thats mean Create one Object of DBContext per request
            //AddAutoMapper => this is a Special Type of Services for Mapping Between Dtos and Models only and Internally it works with with Singlton (one object across all app) 
            #endregion
            //builder.Services.AddScoped<IDepartment_Repository, Department_Repository>();
            //builder.Services.AddScoped<IEmployee_Repository, Employee_Repository>();

            builder.Services.AddScoped<IDepartment_Service, Department_Service>();
            builder.Services.AddScoped<IEmployee_Services, Employee_Service>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAttachment, AttachmentService>();

            // Register Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>
                (
                  //put here all Constraints on the registration form
                  //options=>options.User.RequireUniqueEmail =true //perform by default
                )
            .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();//By default AddIdentity Can be in Defferent Database so we need to tell it to use our DbContext that contain Identity Tables


            // Register AutoMapper with the Mapping_Profile

            //builder.Services.AddAutoMapper(typeof(Demo_BLL.Profiles.Mapping_Profile).Assembly);
            builder.Services.AddAutoMapper(m=>m.AddProfile(new Mapping_Profile()));

            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.ExpireTimeSpan = TimeSpan.FromDays(2);
                config.LoginPath = "/Account/Login";
                config.LogoutPath = "/Account/Logout";
                config.AccessDeniedPath = "/Home/Error";
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();             

            app.UseAuthentication();      
            app.UseAuthorization();

            app.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Account}/{action=Register}");


            app.Run();
        }
    }
}
