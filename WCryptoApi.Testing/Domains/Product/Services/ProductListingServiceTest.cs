using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using WCryptoApi.Business.Services.Product;
using WCryptoApi.Core.Data.Product;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Core.Exceptions;
using Xunit;
using Xunit.Abstractions;

namespace WCryptoApi.Testing.Domains.Product.Services;

public class ProductListingServiceTest: UnitTestBase<ProductListingService>
{
    public ProductListingServiceTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task FindAll_OnExistingRecords_ShouldReturnValues()
    {
        
        IEnumerable<ProductDto> categoriesMock = MockProductDtos();
        IProductListingDb productListingDbMock = ProductListingDbMock(categoriesMock);
        TestTarget = new ProductListingService(productListingDbMock);
        List<ProductDto> categories = await TestTarget.FindAll();
        Assert.Equal(
            expected: JsonConvert.SerializeObject(categoriesMock), 
            actual: JsonConvert.SerializeObject(categories)
        );
    }
    
    [Fact]
    public async Task FindAll_OnNotReturningRecords_ShouldThrowHttpNotFoundException()
    {;
        IProductListingDb productListingDbMock = ProductListingDbMock(new List<ProductDto>());
        TestTarget = new ProductListingService(productListingDbMock);
        await Assert.ThrowsAsync<HttpNotFoundException>(() => TestTarget.FindAll());
    }

    private IEnumerable<ProductDto> MockProductDtos()
    {
        return new List<ProductDto>
        {
            new ProductDto { ProductId = 1, UserId = 1, Description = "test 1" },
            new ProductDto { ProductId = 2, UserId = 1, Description = "test 2" }
        };
    }

    private IProductListingDb ProductListingDbMock(IEnumerable<ProductDto> categoriesMock)
    {
        Mock<IProductListingDb> productListingDbMock = new Mock<IProductListingDb>();
        productListingDbMock
           .Setup(c => c.FindAll())
           .ReturnsAsync(categoriesMock)
        ;
        return productListingDbMock.Object;
    }
}