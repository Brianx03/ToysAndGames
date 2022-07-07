using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using ToysAndGames.Controllers;
using ToysAndGames_DataAccess.Data;
using ToysAndGames_Model.Models;
using Xunit.Abstractions;

namespace TestCode
{
    public class ToysAndGamesTest
    {
           
        [Fact]
        public async void TestGetProducts()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: "ProductsDatabase")
           .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Products.Add(new Product
                {
                    Name = "Car",
                    Description = "Diecast Car",
                    AgeRestriction = 3,
                    Company = "HotWheels",
                    Price = 25.00M
                });

                context.Products.Add(new Product
                {
                    Name = "Zoid",
                    Description = "Action figure",
                    AgeRestriction = 8,
                    Company = "Mattel",
                    Price = 805.00M
                });
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                ProductController controller = new ProductController(context);
                var result = await controller.GetProduct() as ObjectResult;
                List<Product>? products = result?.Value as List<Product>;

                Assert.NotNull(products);
                Assert.True(products.Count > 0);
            }
        }

        [Fact]
        public async void TestCreateProducts()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: "ProductsCreationDatabase")
           .Options;

            Product product = new Product {
                Name = "Car",
                Description = "Diecast Car",
                AgeRestriction = 3,
                Company = "HotWheels",
                Price = 25.00M
            };
         

            using (var context = new ApplicationDbContext(options))
            {
                ProductController controller = new ProductController(context);
                var resultCreate = await controller.CreateProduct(product);

                var result = await controller.GetProduct() as ObjectResult;
                List<Product>? products = result?.Value as List<Product>;

                //Assert.Equal(HttpStatusCode.OK, resultCreate.ToString());
                Assert.NotNull(products);
                Assert.True(products.Count > 0);
            }
        }

        [Fact]
        public async void TestUpdateProducts()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: "ProductsDatabase")
           .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Products.Add(new Product
                {
                    Name = "Car",
                    Description = "Diecast Car",
                    AgeRestriction = 3,
                    Company = "HotWheels",
                    Price = 25.00M
                });
                context.SaveChanges();
            }

            Product product = new Product
            {
                Id = 1,
                Name = "Car",
                Description = "Diecast Car",
                AgeRestriction = 3,
                Company = "Matchbox",
                Price = 29.00M
            };

            using (var context = new ApplicationDbContext(options))
            {
                ProductController controller = new ProductController(context);
                var resultUpdate = await controller.UpdateProduct(product);
                var result = await controller.GetProduct() as ObjectResult;
                List<Product>? products = result?.Value as List<Product>;

                Assert.NotNull(products);
                Assert.Equal("Matchbox",products[0].Company);
            }
        }

        [Fact]
        public async void TestDeleteProducts()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: "ProductsDatabase")
           .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Products.Add(new Product
                {
                    Name = "Car",
                    Description = "Diecast Car",
                    AgeRestriction = 3,
                    Company = "HotWheels",
                    Price = 25.00M
                });
                context.SaveChanges();
            }

            Product product = new Product
            {
                Id = 1,
            };

            using (var context = new ApplicationDbContext(options))
            {
                ProductController controller = new ProductController(context);
                var resultUpdate = await controller.DeleteProduct(1);
                var result = await controller.GetProduct() as ObjectResult;
                List<Product>? products = result?.Value as List<Product>;

                Assert.True(products?.Count == 0);
            }
        }
    }
}