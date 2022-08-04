using Microsoft.AspNetCore.Mvc;
using Moq;
using ToysAndGames.Controllers;
using ToysAndGames.Services;
using ToysAndGamesModel.Models;

namespace Tests
{
    public class UnitTest
    {
        public static IEnumerable<object[]> Data()
        {
            var testcase = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Car",
                    Description = "Diecast Car",
                    AgeRestriction = 3,
                    Company = "HotWheels",
                    Price = 39.00M
                },
                new Product
                {
                    Id = 2,
                    Name = "Car",
                    Description = "Diecast Car",
                    AgeRestriction = 3,
                    Company = "MatchBox",
                    Price = 29.0M
                },
                new Product
                {
                    Id = 3,
                    Name = "Zoid",
                    Description = "Action figure",
                    AgeRestriction = 10,
                    Company = "Kotobukiya",
                    Price = 999.00M
                }
            };

            yield return new object[] { testcase };
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void GetProductNotNullCountSameAsMemberDataAndResponseIsOk(List<Product> products)
        {
            //Arrange
            var mockService = new Mock<IProductServices>();
            mockService.Setup(s => s.Get())
                        .Returns(Task.FromResult(products));

            var controller = new ProductController(mockService.Object);

            //Act
            var result = (OkObjectResult)controller.GetProduct().Result;
            var resultList = result.Value as List<Product>;
            //Assert
            Assert.NotNull(result.Value);
            Assert.Equal(200, result.StatusCode);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void InsertProductAndValidateId(List<Product> products)
        {
            //Arrange
            var mockService = new Mock<IProductServices>();
            mockService.Setup(s => s.Insert(It.IsAny<Product>())).Returns(Task.FromResult(products[0]));
            var controller = new ProductController(mockService.Object);

            //Act
            Product? result = (controller.CreateProduct(products[0]).Result as ObjectResult).Value as Product;


            //Asert
            Assert.NotNull(result);
            Assert.True(result?.Id == 1);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public async void UpdateProductAndValidateCompany(List<Product> products)
        {
            //Arrange
            var mockService = new Mock<IProductServices>();
            mockService.Setup(s => s.Update(products[0])).Returns(Task.FromResult(products[0]));
            var controller = new ProductController(mockService.Object);

            //Act
            var result = (controller.UpdateProduct(products[0]).Result as ObjectResult).Value as Product;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, result?.Id);
            Assert.Equal("HotWheels", result?.Company);
        }

        [Theory]
        [InlineData(1)]
        public void DeleteProductReturnsNoContent(int id)
        {
            //Arrange
            var mockService = new Mock<IProductServices>();
            mockService.Setup(s => s.Delete(It.IsAny<int>())).Returns(Task.FromResult(1));
            var controller = new ProductController(mockService.Object);

            //Act
            var actionResult = controller.DeleteProduct(1).Result as NoContentResult;

            //Assert
            Assert.Equal(204, actionResult?.StatusCode);
        }

        [Theory]
        [InlineData(null)]
        public void InsertNullProductReturnsBadRequest(Product product)
        {
            //Arrange
            var mockService = new Mock<IProductServices>();
            mockService.Setup(s => s.Insert(product));
            var controller = new ProductController(mockService.Object);

            //Act
            var actionResult = controller.CreateProduct(product).Result as BadRequestResult;

            //Assert
            Assert.Equal(400, actionResult?.StatusCode);
        }
    }
}
