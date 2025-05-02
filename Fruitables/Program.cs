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
                opt.UseSqlServer("server=WINDOWS-8K5SI44\\SQLEXPRESS;database=FruitableDB;trusted_connection=true;integrated security=true;TrustServerCertificate=true;");
            }
                );
            var app = builder.Build();
            app.UseStaticFiles();
            app.MapControllerRoute(
                "default",
                "{controller=home}/{action=index}/{id?}"



                );


            app.Run();
        }
    }
}
