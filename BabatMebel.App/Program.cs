using BabatMebel.App.Constants;
using BabatMebel.App.Context;
using BabatMebel.App.Entities;
using BabatMebel.App.Repository.Abstracts.RContact;
using BabatMebel.App.Repository.Abstracts.REmployee;
using BabatMebel.App.Repository.Abstracts.RFurniture;
using BabatMebel.App.Repository.Abstracts.RPosition;
using BabatMebel.App.Repository.Concretes.RContact;
using BabatMebel.App.Repository.Concretes.REmployee;
using BabatMebel.App.Repository.Concretes.RFurniture;
using BabatMebel.App.Repository.Concretes.RPosition;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BabatMebel.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IContactReadRepository, ContactReadRepository>();
            builder.Services.AddScoped<IContactWriteRepository, ContactWriteRepository>();

            builder.Services.AddScoped<IPositonReadRepository, PositionReadRepository>();
            builder.Services.AddScoped<IPositionWriteRepository, PositionWriteRepository>();

            builder.Services.AddScoped<IEmployeeReadRepository, EmployeeReadRepository>();
            builder.Services.AddScoped<IEmployeeWriteRepository, EmployeeWriteRepository>();

            builder.Services.AddScoped<IFurnitureReadRepository, FurnitureReadRepository>();
            builder.Services.AddScoped<IFurnitureWriteRepository, FurnitureWriteRepository>();

            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString(ConnectionNames.DefaultConnectionName));
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<AppDbContext>();

            builder.Services.Configure<IdentityOptions>(opt =>
            {
                // password
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequiredLength = 8;
                opt.Password.RequiredUniqueChars = 1;

                //lockout
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                opt.Lockout.MaxFailedAccessAttempts = 3;
                opt.Lockout.AllowedForNewUsers = true;

                // User settings.
                opt.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                opt.User.RequireUniqueEmail = false;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();


            app.MapAreaControllerRoute(
                name: "admin",
                areaName: "admin",
                pattern: "admin/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");


            app.Run();
        }
    }
}
