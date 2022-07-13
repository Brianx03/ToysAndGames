using Microsoft.EntityFrameworkCore;
using ToysAndGames.Services;
using ToysAndGames_DataAccess.Data;
using ToysAndGames_Model.Models;

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
                    Company = "Kotobukiya",
                    Price = 805.00M
                });
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                ProductServices service = new ProductServices(context);
                var products = service.Get();

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
                ProductServices service = new ProductServices(context);
                var resultCreate = service.Insert(product);
                var products = service.Get();

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
                ProductServices service = new ProductServices(context);
                var resultUpdate = service.Update(product);
                var products = service.Get();

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
                ProductServices service = new ProductServices(context);
                service.Delete(1);
                var products = service.Get();

                Assert.True(products?.Count == 0);
            }
        }
    }
}