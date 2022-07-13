
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using ToysAndGames.Controllers;
using ToysAndGames.Services;
using ToysAndGames_Model.Models;

namespace Tests
{
    public class UnitTest
    {
        [Fact]
        public void GetProductNotNull_CountGreaterThanZero_And_ResponseIsOk()
        {
            //Arrange
            var mockService = new Mock<IProductServices>();
            mockService.Setup(s => s.Get())
                        .Returns(new List<Product>
                        {
                            new Product()
                            {
                                Id = 1,
                                Name = "Car",
                                Description = "Diecast Car",
                                AgeRestriction = 3,
                                Company="HotWheels",
                                Price = 39.00M
                            },
                            new Product()
                            {
                                Id = 1,
                                Name = "Car",
                                Description = "Diecast Car",
                                AgeRestriction = 3,
                                Company="MatchBox",
                                Price = 29.0M
                            },
                            new Product()
                            {
                                Id = 1,
                                Name = "Zoid",
                                Description = "Action figure",
                                AgeRestriction = 10,
                                Company="Kotobukiya",
                                Price = 999.00M
                            }
                        });

            var controller = new ProductController(mockService.Object);

            //Act
            var result = (OkObjectResult)controller.GetProduct();
            var resultList = result.Value as List<Product>;
            //Assert
            Assert.NotNull(result.Value);
            Assert.True(resultList.Count > 0);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void InsertProduct_And_Validate_Id_Greater_Than_Cero()
        {
            //Arrange
            Product product = new Product()
            {
                Id = 1,
                Name = "Car",
                Description = "Diecast Car",
                AgeRestriction = 3,
                Company = "HotWheels",
                Price = 39.00M
            };

            var mockService = new Mock<IProductServices>();
            mockService.Setup(s => s.Insert(It.IsAny<Product>())).Returns(product);
            var controller = new ProductController(mockService.Object);

            //Act
            var result = ((OkObjectResult)controller.CreateProduct(product)).Value as Product;

            //Asert
            Assert.NotNull(result);
            Assert.True(result.Id > 0);
        }

        [Fact]
        public void UpdateProduct_And_Validate_Company()
        {
            //Arrange
            Product product = new Product()
            {
                Id = 1,
                Name = "Car",
                Description = "Diecast Car",
                AgeRestriction = 3,
                Company = "HotWheels",
                Price = 39.00M
            };
            var mockService = new Mock<IProductServices>();
            mockService.Setup(s => s.Update(product)).Returns(product);
            var controller = new ProductController(mockService.Object);

            //Act
            var result = ((OkObjectResult)controller.UpdateProduct(product)).Value as Product;

            //Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("HotWheels", result.Company);
        }

        [Fact]
        public void Delete_Product_Returns_NoContent()
        {
            //Arrange
            var mockService = new Mock<IProductServices>();
            mockService.Setup(s => s.Delete(It.IsAny<int>()));
            var controller = new ProductController(mockService.Object);

            //Act
            var actionResult = (NoContentResult)controller.DeleteProduct(1);

            //Assert
            Assert.Equal(204, actionResult.StatusCode);
        }

        [Fact]
        public void Insert_null_Product_Returns_Bad_Request()
        {
            //Arrange
            Product? product = null;
            var mockService = new Mock<IProductServices>();
            mockService.Setup(s => s.Insert(product));
            var controller = new ProductController(mockService.Object);

            //Act
            var actionResult = (BadRequestResult)controller.CreateProduct(product);

            //Assert
            Assert.Equal(400, actionResult.StatusCode);
        }
    }
}