﻿using Fruitables.DAL;
using Fruitables.Models;
using Fruitables.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fruitables.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int? id)
        {
            if (id is null || id <= 0)
            {
                return BadRequest();
            }
            Product? product = _context.Products
                .Include(p => p.ProductImages.OrderByDescending(pi => pi.IsPrimary))
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);


            if (product is null) return NotFound();
            DetailVM detailVM = new DetailVM
            {
                Product = product,
                RelatedProducts = _context.Products
           .Where(p => p.CategoryId == product.CategoryId && p.Id != product.Id)
           .Take(8)
           .Include(p => p.ProductImages.Where(pi => pi.IsPrimary != null))
           .ToList()
            };
            return View(detailVM);
        }
    }
}
