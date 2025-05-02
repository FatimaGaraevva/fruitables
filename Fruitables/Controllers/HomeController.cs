using Fruitables.DAL;
using Fruitables.Models;
using Fruitables.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fruitables.Controllers
{
    public class HomeController : Controller
    {
        public readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
          
            HomeVM homeVM = new()
            {
                Slides = _context.Slides
                .OrderBy(s => s.Order)
                .Take(2)
                .ToList(),

                Products = _context.Products
                .Take(8)
                .Include(p => p.ProductImages.Where(pi => pi.IsPrimary != null))
                .ToList()


            };


            return View(homeVM);
        }
    }
}

