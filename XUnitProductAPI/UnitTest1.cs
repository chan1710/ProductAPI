using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using Moq.EntityFrameworkCore;
using Product_API.Controllers;
using Product_API.Data;
using Product_API.Models;
using Testing.Mocks;

namespace Testing
{
    public class UnitTest
    {
        [Theory]
        [InlineData("TestName", 1.0, 10.0, "name", 1)]
        [InlineData("TestName", 1.0, 10.0, "sdfgasd", 1)]
        public void GetAllProducts(string search, double? from, double? to, string sortBy, int page = 1)
        {
            var repositoryWrapperMock = ProductRepositoryWrapperMock.GetMock();
            var ownerController = new ProductsController(repositoryWrapperMock.Object);

            var result = ownerController.GetAllProducts(search, from, to, sortBy, page) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            var productList = result.Value as IEnumerable<ProductModel>;
            Assert.NotEmpty(productList);
            Assert.Equal(1, productList.Count());
        }

        private static List<User> GetFakeEmployeeList()
        {
            return new List<User>()
            {
                new User
                {
                    Id = 1,
                    UserName = "test",
                    Password = "test",
                    FullName = "123-456-7890",
                    Email = "tesst@gmail.com"
                },
                new User
                {
                    Id = 2,
                    UserName = "John Doe",
                    Password = "J.D@gmail.com",
                    FullName = "123-456-7890",
                    Email = "tesst@gmail.com"
                },
            };
        }

        // IDbContextGenerator contextGenerator;
        // List<Domain.Entity> entities;

        [Fact]
        public async void Login()
        {

            var userDbSetMock = new Mock<DbSet<User>>();

            var myDbMoq = new Mock<MyDbContext>();
            //myDbMoq.Setup(o => o.Users).Returns(() => userDbSetMock.Object);
            //myDbMoq.Setup(p => p.Set<User>()).Returns(userDbSetMock.Object);
            myDbMoq.Setup(p => p.Users).Returns(MyDbContextMock.GetQueryableMockDbSet<User>(GetFakeEmployeeList()));
            myDbMoq.Setup(p => p.SaveChanges()).Returns(1);
            //myDbMoq.SetupGet(x => x.Users).ReturnsDbSet(GetFakeEmployeeList());
            //myDbMoq.SetupSequence(x => x.Set<User>())
            //  .ReturnsDbSet(new List<User>())
            //  .ReturnsDbSet(GetFakeEmployeeList());

            //myDbMoq.Setup(p => p.SaveChanges()).Returns(1);

            // var employeeContextMock = new Mock<MyDbContext>();
            //myDbMoq.Setup<DbSet<User>>(x => x.Users)
            // .ReturnsDbSet(GetFakeEmployeeList());
            //myDbMoq.Setup(x => x.Users.FindAsync(1).Result)
            //  .Returns(GetFakeEmployeeList().Find(e => e.Id == 1) ?? new User());
            var ops = new AppSetting
            {
                SecretKey = "test",
            };
            var userContrl = new UserController(myDbMoq.Object, new TestOptionsMonitor<AppSetting>(ops));

            var loginData = new LoginModel
            {
                UserName = "test",
                Password = "test",
            };
            var result = await userContrl.Validate(loginData) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            var respsonse = result.Value as ApiResponse;
            Assert.True(respsonse.Success);

        }
    }
}