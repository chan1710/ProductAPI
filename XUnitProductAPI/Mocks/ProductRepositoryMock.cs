using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Product_API.Data;
using Product_API.Models;
using Product_API.Services;

namespace XUnitProductAPI.Mocks
{
    internal class ProductRepositoryMock
    {
        public static Mock<IProductRepository> GetMock()
        {
            var mock = new Mock<IProductRepository>();

            // Setup the mock
            mock.Setup(
                m => m.GetAll(
                    It.IsAny<string>(),
                    It.IsAny<double>(),
                    It.IsAny<double>(),
                    It.IsAny<string>(),
                    It.IsAny<int>()
                    )
                )
                .Returns(() => new List<ProductModel>
                {
                    new ProductModel
                    {
                        ProductId = Guid.NewGuid(),
                        Price = 10,
                        ProductName = "Test Name Product",
                        TypeName = "Test Name Type",
                    }
                });

            // return
            return mock;
        }
    }
}
