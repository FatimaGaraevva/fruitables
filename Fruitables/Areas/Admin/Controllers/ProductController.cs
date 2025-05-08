using Fruitables.DAL;
using Fruitables.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fruitables.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
       
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }
       
        public async Task<IActionResult> Index()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return View(products);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product )
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool result = await _context.Products.AnyAsync(c => c.Name == product.Name);
            if (result)
            {
                ModelState.AddModelError(nameof(product.Name), $"{product.Name} name alredy exsts");
                return View();
            }
            product.CreateAt = DateTime.Now;
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult>Update(int? id)
        {
            if (id is null || id<=0)
            {
                return BadRequest();
            }
            Product? product = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
            if (product is null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult>Update(int?id,Product product)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool result = await _context.Products.AnyAsync(c => c.Name == product.Name && c.Id != id);
            if (result)
            {
                ModelState.AddModelError(nameof(product.Name), $"{product.Name} name already exists ");
                return View();
            }
            Product? exsited = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
            exsited.Name = product.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
