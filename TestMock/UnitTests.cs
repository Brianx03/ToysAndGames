using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToysAndGames.Controllers;
using ToysAndGames_Model.Models;

namespace TestMock
{
    public class UnitTests
    {
        IDbContextGenerator contextGenerator;
        List<Product> products;
        [SetUp]
        public void Setup()
        {
            products = new List<Product>();
            products.Add(new Product
            {
                Id = 1,
                Name = "Car",
                Description = "Diecast Car",
                AgeRestriction = 3,
                Company = "HotWheels",
                Price = 29.00M
            });
            var myDbMoq = new Mock<IMyDbContext>();
            myDbMoq.Setup(p => p.Products).Returns(DbContextMock.GetQueryableMockDbSet<Product>(products));
            myDbMoq.Setup(p => p.SaveChanges()).Returns(1);
            var moq = new Mock<IDbContextGenerator>();
            moq.Setup(p => p.GenerateMyDbContext()).Returns(myDbMoq.Object);
            contextGenerator = moq.Object;
        }
    }
}
