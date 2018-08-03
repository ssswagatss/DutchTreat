using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext _context;
        private readonly IHostingEnvironment _env;
        private readonly UserManager<StoreUser> _userManager;

        public DutchSeeder(DutchContext context,IHostingEnvironment env, UserManager<StoreUser> userManager)
        {
            _context = context;
            _env = env;
            _userManager = userManager;
        }
        public async Task Seed()
        {
            _context.Database.EnsureCreated();

            var user =await _userManager.FindByEmailAsync("ssswagatss@gmail.com");
            if (user == null)
            {
                user = new StoreUser {
                    FirstName="Swagat",
                    LastName="Swain",
                    Email="ssswagatss@gmail.com",
                    UserName= "ssswagatss@gmail.com"
                };
                var res = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (res != IdentityResult.Success)
                {
                    throw new Exception("Failed to create user");
                }
            }


            if (!_context.Products.Any())
            {
                var filePath = Path.Combine(_env.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filePath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _context.Products.AddRange(products);

                var order = new Order()
                {
                    OrderDate = DateTime.Now,
                    OrderNumber = "1234",
                    Items = new List<OrderItem>()
                    {
                        new OrderItem {
                            Product=products.First(),
                            Quantity=5,
                            UnitPrice=products.First().Price
                        }
                    },
                    User= user
                };
                _context.Orders.Add(order);

                _context.SaveChanges();
            }
        }
    }
}
