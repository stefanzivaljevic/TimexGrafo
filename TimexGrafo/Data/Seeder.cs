using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TimexGrafo.Models;

namespace TimexGrafo.Data
{
    public class Seeder
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hosting;

        public Seeder(ApplicationDbContext context, IWebHostEnvironment hosting)
        {
            _context = context;
            _hosting = hosting;
        }

        public void Seed()
        {
            _context.Database.EnsureCreated();

            if (!_context.Items.Any())
            {

                var filepath = Path.Combine(_hosting.ContentRootPath, "Data/data.json");
                var json = File.ReadAllText(filepath);
                var items = JsonConvert.DeserializeObject<IEnumerable<Item>>(json);
                _context.Items.AddRange(items);


                _context.SaveChanges();


            }

            if (!_context.ShopItems.Any())
            {

                var filepath = Path.Combine(_hosting.ContentRootPath, "Data/shopData.json");
                var json = File.ReadAllText(filepath);
                var items = JsonConvert.DeserializeObject<IEnumerable<ShopItem>>(json);
                _context.ShopItems.AddRange(items);


                _context.SaveChanges();


            }
        }
    }
}
