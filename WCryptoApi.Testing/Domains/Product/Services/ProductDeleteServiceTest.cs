using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using WCryptoApi.Business.Services.Product;
using WCryptoApi.Core.Data.Product;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Core.Exceptions;
using WCryptoApi.Core.Services.Product;
using WCryptoApi.Testing.Utils;
using Xunit;
using Xunit.Abstractions;

namespace WCryptoApi.Testing.Domains.Product.Services;

public class ProductDeleteServiceTest: UnitTestBase<ProductDeleteService>
{
    public ProductDeleteServiceTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task Delete_ExistingRecord_ShouldPass()
    {
        int productId = NumberMockUtil.RandomIntGt0();
        ProductDto productDto = new()
        {
            ProductId = productId,
            Description = StringMockUtil.RandomString(100),
            UserId = NumberMockUtil.RandomIntGt0()
        };
        TestTarget = new ProductDeleteService(
            MockProductDeleteDb(true),
            MockProductFinderService(productId, productDto)
        );
        Assert.Equal(
        JsonConvert.SerializeObject(productDto),
        JsonConvert.SerializeObject(await TestTarget.DeleteById(productId))
        );
    }
    
    [Fact]
    public async Task Delete_NonExistingRecord_ShouldThrowHttpNotFoundException()
    {
        int productId = NumberMockUtil.RandomIntGt0();
        TestTarget = new ProductDeleteService(
            MockProductDeleteDb(false),
            MockProductFinderServiceThrowingException()
        );
        await Assert.ThrowsAsync<HttpNotFoundException>(() => TestTarget.DeleteById(productId));
    }
    
    [Fact]
    public async Task Delete_OnFailureOfDeleting_ShouldFail()
    {
        int productId = NumberMockUtil.RandomIntGt0();
        ProductDto productDto = new()
        {
            ProductId = productId,
            Description = StringMockUtil.RandomString(100),
            UserId = NumberMockUtil.RandomIntGt0()
        };
        TestTarget = new ProductDeleteService(
            MockProductDeleteDb(false),
            MockProductFinderService(productId, productDto)
        );
        await Assert.ThrowsAsync<HttpBadRequestException>(() => TestTarget.DeleteById(productId));
    }

    private IProductDeleteDb MockProductDeleteDb(bool result)
    {
        Mock<IProductDeleteDb> productDeleteDbMock = new Mock<IProductDeleteDb>();
        productDeleteDbMock
           .Setup(c => c.Delete(It.IsAny<int>()))
           .ReturnsAsync(result);
        return productDeleteDbMock.Object;
    }
    
    private IProductFinderService MockProductFinderService(int productId, ProductDto productDto)
    {
        Mock<IProductFinderService> productFinderServiceMock = new();
        productFinderServiceMock
           .Setup(c => c.FindById(productId))
           .ReturnsAsync(productDto);
        return productFinderServiceMock.Object;
    }
    
    private IProductFinderService MockProductFinderServiceThrowingException()
    {
        Mock<IProductFinderService> productFinderServiceMock = new();
        productFinderServiceMock
           .Setup(c => c.FindById(It.IsAny<int>()))
           .Throws(new HttpNotFoundException());
        return productFinderServiceMock.Object;
    }
}