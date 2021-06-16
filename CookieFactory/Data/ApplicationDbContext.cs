using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using CookieFactory.Models;

namespace CookieFactory.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CookieFactory.Models.Category> Category { get; set; }
        public DbSet<CookieFactory.Models.Product> Product { get; set; }
        public DbSet<CookieFactory.Models.Cookie> Cookie { get; set; }
    }
}
