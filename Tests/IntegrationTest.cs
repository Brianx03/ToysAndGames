using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using ToysAndGamesModel.Models;
using Xunit.Abstractions;

namespace Tests
{
    public class IntegrationTest
    {
        private readonly WebApplicationFactory<Program> _factory;



        public IntegrationTest(ITestOutputHelper outputhelper)
        {
            _factory = new WebApplicationFactory<Program>();
        }

        public static IEnumerable<object[]> Data()
        {
            var testcase = new Product()
            {
                Name = "Zoid",
                Description = "Shield Liger",
                AgeRestriction = 5,
                Company = "Hasbro",
                Price = 600.55M,
                ImagePath = "C:\\Users\\brian.armenta\\Pictures\\SamplePhotos\\dog.jpg",
                ImageBytes = new byte[] { 0x0 }
            };

            yield return new object[] { testcase };
        }

        public static IEnumerable<object[]> Data2()
        {
            var testcase = new Product()
            {
                Name = "Corvette ZR1",
                Description = "Diecast Car",
                AgeRestriction = 8,
                Company = "HotWheels",
                Price = 40.55M,
                ImagePath = "C:\\Users\\brian.armenta\\Pictures\\SamplePhotos\\dog.jpg",
                ImageBytes = new byte[] { 0x0 }
            };

            yield return new object[] { testcase };
        }

        public static IEnumerable<object[]> Data3()
        {
            var testcase = new Product()
            {
                Name = "Zoid",
                Description = "Liger Zero",
                AgeRestriction = 200,
                Company = "Kotobukiya",
                Price = 800.55M,
                ImagePath = "C:\\Users\\brian.armenta\\Pictures\\SamplePhotos\\dog.jpg",
                ImageBytes = new byte[] { 0x0 }
            };

            yield return new object[] { testcase };
        }

        [Fact]
        public async void GetAllProductsReturnsOk()
        {
            //Arrange
            var client = _factory.CreateDefaultClient();
            //Act
            var response = await client.GetAsync("/api/product");

            //Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(Data))]
        [MemberData(nameof(Data2))]
        public async void GivenTwoProductsInsertReturnsCreated(Product product)
        {
            //Arrange
            var client = _factory.CreateDefaultClient();

            var json = JsonConvert.SerializeObject(product);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            var response = await client.PostAsync("/api/product", data);
            //Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
        [Theory]
        [MemberData(nameof(Data3))]
        public async void GivenProductWithInvalidAgeRestrictionReturnsException(Product product)
        {
            //Arrange
            var client = _factory.CreateDefaultClient();

            var json = JsonConvert.SerializeObject(product);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            var response = await client.PostAsync("/api/product", data);
            //Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public async void getsProductsAndUpdateFirst(Product product)
        {
            //Arrange
            var client = _factory.CreateDefaultClient();

            //Act
            var response = await client.GetAsync("/api/product");
            var obj = response.Content.ReadAsStringAsync().Result;
            var firsProduct = (JsonConvert.DeserializeObject<List<Product>>(obj)).FirstOrDefault();

            product.Id = firsProduct.Id;

            var json = JsonConvert.SerializeObject(product);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var responseUpdate = await client.PutAsync("/api/product", data);
            //Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public async void InsertsProductThenDeletesIt(Product product)
        {
            //Arrange
            var client = _factory.CreateDefaultClient();

            var json = JsonConvert.SerializeObject(product);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            //Act
            var responseInsert = await client.PostAsync("/api/product", data);
            var obj = responseInsert.Content.ReadAsStringAsync().Result;
            var jsonResponse = JsonConvert.DeserializeObject<Product>(obj);

            var responseDelete = await client.DeleteAsync($"/api/product/{jsonResponse.Id}");
            
            //Assert
            Assert.Equal(204, (int)responseDelete.StatusCode);
        }
    }
}