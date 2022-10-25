using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using WCryptoApi.Business.Services.Product;
using WCryptoApi.Core.Data.Product;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Core.Exceptions;
using WCryptoApi.Testing.Utils;
using Xunit;
using Xunit.Abstractions;

namespace WCryptoApi.Testing.Domains.Product.Services;

public class ProductFinderServiceTest: UnitTestBase<ProductFinderService>
{
    public ProductFinderServiceTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task FindAll_WithExistingRecord_ShouldReturnRecord()
    {
        int productId = NumberMockUtil.RandomIntGt0();
        ProductDto productMock = ProductDtoMock(productId);
        IProductFinderDb productFinderDbMock = ProductFinderDbMock(productMock);
        TestTarget = new ProductFinderService(productFinderDbMock);
        ProductDto product = await TestTarget.FindById(productId);
        Assert.Equal(
            expected: JsonConvert.SerializeObject(productMock), 
            actual: JsonConvert.SerializeObject(product)
        );
    }
    
    [Fact]
    public async Task FindAll_WithNonExistingRecord_ShouldThrowHttpNotFoundException()
    {
        int productId = NumberMockUtil.RandomIntGt0();
        IProductFinderDb productFinderDbMock = ProductFinderDbMockReturningNull();
        TestTarget = new ProductFinderService(productFinderDbMock);
        await Assert.ThrowsAsync<HttpNotFoundException>(() => TestTarget.FindById(productId));
    }

    private ProductDto ProductDtoMock(int productId)
    {
        return new ProductDto
        {
            ProductId = productId,
            Description = StringMockUtil.RandomString(100),
            UserId = NumberMockUtil.RandomIntGt0()
        };
    }

    private IProductFinderDb ProductFinderDbMock(ProductDto productMock)
    {
        Mock<IProductFinderDb> productFinderDbMock = new Mock<IProductFinderDb>();
        productFinderDbMock
           .Setup(c => c.FindById(It.IsAny<int>()))
           .ReturnsAsync(productMock)
        ;
        return productFinderDbMock.Object;
    }
    
    private IProductFinderDb ProductFinderDbMockReturningNull()
    {
        Mock<IProductFinderDb> productFinderDbMock = new Mock<IProductFinderDb>();
        productFinderDbMock
           .Setup(c => c.FindById(It.IsAny<int>()))
           .ReturnsAsync((ProductDto?) null)
        ;
        return productFinderDbMock.Object;
    }
}