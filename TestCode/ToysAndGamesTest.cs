using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using ToysAndGames_Model.Models;
using Xunit.Abstractions;

namespace TestCode
{
    public class ToysAndGamesTest
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly WebApplicationFactory<Program> _factory;

        public ToysAndGamesTest(ITestOutputHelper outputhelper)
        {
            _factory = new WebApplicationFactory<Program>();
            _outputHelper = outputhelper;
        }
        [Fact]
        public async void TestGetProducts()
        {
            //Arrange
            var client = _factory.CreateDefaultClient();
            //Act
            var response = await client.GetAsync("/api/product/product");
            //Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = response.Content.ReadAsStringAsync().Result;
            _outputHelper.WriteLine(JsonConvert.SerializeObject(responseContent));
        }

        [Fact]
        public async void TestCreateProducts()
        {
            //Arrange
            var client = _factory.CreateDefaultClient();

            var product = new Product();
            product.Name = "Zoid";
            product.Description = "Liget Zero";
            product.AgeRestriction = 8;
            product.Company = "Kotobukiya";
            product.Price = 800.55M;

            var json = JsonConvert.SerializeObject(product);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            var response = await client.PostAsync("/api/product/createproduct", data);
            //Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = response.Content.ReadAsStringAsync().Result;
            _outputHelper.WriteLine(JsonConvert.SerializeObject(responseContent));
        }

        [Fact]
        public async void TestUpdateProducts()
        {
            //Arrange
            var client = _factory.CreateDefaultClient();

            var product = new Product();
            product.Id = 4;
            product.Name = "Carrito";
            product.Description = "Corvette ZR1";
            product.AgeRestriction = 3;
            product.Company = "HotWheels";
            product.Price = 40.00M;

            var json = JsonConvert.SerializeObject(product);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            var response = await client.PutAsync("/api/product/updateproduct", data);
            //Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = response.Content.ReadAsStringAsync().Result;
            _outputHelper.WriteLine(JsonConvert.SerializeObject(responseContent));
        }

        [Fact]
        public async void TestDelete()
        {
            //Arrange
            var client = _factory.CreateDefaultClient();

            var product = new Product();
            product.Id = 4;

            var json = JsonConvert.SerializeObject(product);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            var response = await client.DeleteAsync("/api/product/deleteproduct?id=6");
            //Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = response.Content.ReadAsStringAsync().Result;
            _outputHelper.WriteLine(JsonConvert.SerializeObject(responseContent));
        }
    }
}