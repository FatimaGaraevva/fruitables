using Fruitables.DAL;
using Fruitables.Models;
using Fruitables.Utilities.Enams;
using Fruitables.Utilities.Extensions;
using Fruitables.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fruitables.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlideController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SlideController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Slide> slides = await _context.Slides.ToListAsync();
            return View(slides);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSlideVM slideVM)
        {
            if (!slideVM.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError(nameof(CreateSlideVM.Photo), "File type is incorrect");
                return View();
            }
            if (slideVM.Photo.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError(nameof(CreateSlideVM.Photo), "File size Sholud be less than 2 mb");
                return View();
            }

            string fileName = await slideVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "img");
            Slide slide = new Slide
            {
                Title = slideVM.Title,
                Photo=slideVM.Photo,
                Order=slideVM.Order
                
            };

            slide.CreateAt = DateTime.Now;
            await _context.Slides.AddAsync(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id <= 0)
            {
                return BadRequest();
            }
            Slide? slide = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);
            if (slide is null)
            {
                return NotFound();
            }
            slide.Image.DeleteFile(_env.WebRootPath, "assets", "img");
            _context.Remove(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult>Update(int? id)
        {
            if (id is null || id<=0)
            {
                return BadRequest();
            }
            Slide? slide = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);
            if (slide is null)
            {
                return NotFound();
            }
            UpdateSlideVM slideVM = new UpdateSlideVM
            {
                Image = slide.Image,
                Photo = slide.Photo,
                Title = slide.Title,
                Order = slide.Order


            };
            return View(slideVM);

        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateSlideVM slideVM)
        {
            Slide? slide = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);
            if (!ModelState.IsValid)
            {
                slideVM.Image = slide.Image;
                return View(slideVM);
            }
            Slide existed = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);
            if (existed is null) return NotFound();
            if (slideVM.Photo is not null)
            {
                if (!slideVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(UpdateSlideVM.Photo), "File type is incorrect");
                    return View(slideVM);
                }
                if (!slideVM.Photo.ValidateSize(FileSize.MB, 1))
                {
                    ModelState.AddModelError(nameof(UpdateSlideVM.Photo), "File size must be less than 1 mb ");
                    return View(slideVM);
                }
                string fileName = await slideVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "website-images");
                existed.Image.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");
                existed.Image = fileName;
            }
            existed.Title = slideVM.Title;
            existed.Image = slideVM.Image;
            existed.Photo = slideVM.Photo;
         
            existed.Order = slideVM.Order;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


            //return Content ("ok");
        }
      




    }
}

