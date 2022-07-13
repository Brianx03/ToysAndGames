using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using ToysAndGames.Controllers;
using ToysAndGames_DataAccess.Data;
using ToysAndGames_Model.Models;
using Xunit.Abstractions;

namespace Tests
{
    public class IntegrationTest
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly ProductController _controller;
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<ApplicationDbContext> _db;

        public IntegrationTest(ITestOutputHelper outputhelper)
        {
            _factory = new WebApplicationFactory<Program>();
            _outputHelper = outputhelper;
        }
        [Fact]
        public async Task TestGetProducts()
        {
            //Arrange
            var client = _factory.CreateDefaultClient();
            //Act
            var response = await client.GetAsync("/api/product/getproduct");
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
            product.Id = 9;
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
            product.Id = 9;

            var json = JsonConvert.SerializeObject(product);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            var response = await client.DeleteAsync($"/api/product/deleteproduct?id={product.Id}");
            //Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            var responseContent = response.Content.ReadAsStringAsync().Result;
            _outputHelper.WriteLine(JsonConvert.SerializeObject(responseContent));
        }
    }
}