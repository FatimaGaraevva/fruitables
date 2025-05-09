﻿using Fruitables.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Fruitables.DAL
{
   
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Slide> Slides { get; set; }
        public DbSet<Category> Categories { get; set; }
            public DbSet<Product> Products { get; set; }
            public DbSet<ProductImage> ProductImages { get; set; }
        }
    }

