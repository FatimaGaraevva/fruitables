using Fruitables.DAL;
using Microsoft.EntityFrameworkCore;
using System;

namespace Fruitables
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            }
                );
            var app = builder.Build();
            app.UseStaticFiles();
            app.MapControllerRoute(


               "admin",
               "{area:exists}/{controller=home}/{action=index}/{id?}"



               );
            app.MapControllerRoute(


                 "default",
                "{controller=home}/{action=index}/{id?}");


            app.Run();
        }
    }
}
