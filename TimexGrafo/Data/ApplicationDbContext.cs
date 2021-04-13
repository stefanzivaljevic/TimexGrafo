using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TimexGrafo.Models;

namespace TimexGrafo.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<SubGroup> Subgroups { get; set; }
        public DbSet<Group> Groups { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<ShopGroup> ShopGroups { get; set; }

        public DbSet<ShopSubGroup> ShopSubGroups { get; set; }

        public DbSet<ShopItem> ShopItems { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
