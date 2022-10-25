using System;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using WCryptoApi.Business.Services.Product;
using WCryptoApi.Core;
using WCryptoApi.Core.Data.Category;
using WCryptoApi.Core.Data.Product;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Core.Exceptions;
using WCryptoApi.Core.Services.Product;
using WCryptoApi.Testing.Utils;
using Xunit;
using Xunit.Abstractions;

namespace WCryptoApi.Testing.Domains.Product.Services;

public class ProductRegisterServiceTest: UnitTestBase<ProductRegisterService>
{
    public ProductRegisterServiceTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task Register_WithValidParams_ShouldPass()
    {
        int productId = NumberMockUtil.RandomIntGt0();
        ProductRequest productRequest = new()
        {
            Description = StringMockUtil.RandomString(100),
            CategoryId = NumberMockUtil.RandomIntGt0()
        };
        TestTarget = new ProductRegisterService(
            productRegisterDb: MockProductRegisterDb(productId), 
            productFinderService: MockProductFinderService(productId, productRequest),
            MockCategoryFinderDbReturningCategory()
        );
        Assert.Equal(
            expected: JsonConvert.SerializeObject(new Core.Entities.Product(
                productId: productId,
                description: productRequest.Description,
                categoryId: productRequest.CategoryId
            )),
            actual: JsonConvert.SerializeObject(await TestTarget.Register(productRequest))
        );
    }
    
    [Fact]
    public async Task Register_WithInvalidParams_ShouldThrowArgumentException()
    {
        int productId = NumberMockUtil.RandomIntGt0();
        ProductRequest productRequest = new()
        {
            Description = StringMockUtil.RandomString(101),
            CategoryId = NumberMockUtil.RandomIntGt0()
        };
        TestTarget = new ProductRegisterService(
            productRegisterDb: MockProductRegisterDb(productId), 
            productFinderService: MockProductFinderService(productId, productRequest),
            MockCategoryFinderDbReturningCategory()
        );
        await Assert.ThrowsAsync<ArgumentException>(() => TestTarget.Register(productRequest));
    }
    
    [Fact]
    public async Task Register_WithNoExistingCategory_ShouldThrowHttpBadRequestException()
    {
        int productId = NumberMockUtil.RandomIntGt0();
        ProductRequest productRequest = new()
        {
            Description = StringMockUtil.RandomString(100),
            CategoryId = NumberMockUtil.RandomIntGt0()
        };
        TestTarget = new ProductRegisterService(
            productRegisterDb: MockProductRegisterDb(productId), 
            productFinderService: MockProductFinderService(productId, productRequest),
            MockCategoryFinderDbReturningNull()
        );
        await Assert.ThrowsAsync<HttpBadRequestException>(() => TestTarget.Register(productRequest));
    }
    
    [Fact]
    public async Task Register_WithExceptionOnDatabaseLayer_ShouldThrowHttpBadRequestException()
    {
        int productId = NumberMockUtil.RandomIntGt0();
        ProductRequest productRequest = new()
        {
            Description = StringMockUtil.RandomString(100),
            CategoryId = NumberMockUtil.RandomIntGt0()
        };
        TestTarget = new ProductRegisterService(
            productRegisterDb: MockProductRegisterDb(), 
            productFinderService: MockProductFinderService(productId, productRequest),
            MockCategoryFinderDbReturningCategory()
        );
        await Assert.ThrowsAsync<HttpBadRequestException>(() => TestTarget.Register(productRequest));
    }

    private IProductRegisterDb MockProductRegisterDb(int productId)
    {
        Mock<IProductRegisterDb> productRegisterDbMock = new();
        productRegisterDbMock
           .Setup(c => 
                c.Register(It.IsAny<Core.Entities.Product>())
            )
           .ReturnsAsync(productId)
        ;
        return productRegisterDbMock.Object;
    }
    
    private IProductRegisterDb MockProductRegisterDb()
    {
        Mock<IProductRegisterDb> productRegisterDbMock = new();
        productRegisterDbMock
           .Setup(c => 
                c.Register(It.IsAny<Core.Entities.Product>())
            )
           .Throws(new Exception())
        ;
        return productRegisterDbMock.Object;
    }

    private IProductFinderService MockProductFinderService(int productId, ProductRequest product)
    {
        Mock<IProductFinderService> productFinderServiceMock = new();
        productFinderServiceMock
           .Setup(c => c.FindById(productId))
           .ReturnsAsync(new ProductDto {
                ProductId = productId,
                Description = product.Description,
                CategoryId = product.CategoryId
            })
        ;
        return productFinderServiceMock.Object;
    }
    
    private ICategoryFinderDb MockCategoryFinderDbReturningCategory()
    {
        Mock<ICategoryFinderDb> productFinderServiceMock = new();
        productFinderServiceMock
           .Setup(c => c.FindById(It.IsAny<int>()))
           .ReturnsAsync(new CategoryDto() {
                CategoryId = NumberMockUtil.RandomIntGt0(),
                Description = StringMockUtil.RandomString(100),
                UserId = NumberMockUtil.RandomIntGt0()
            })
        ;
        return productFinderServiceMock.Object;
    }
    
    private ICategoryFinderDb MockCategoryFinderDbReturningNull()
    {
        Mock<ICategoryFinderDb> productFinderServiceMock = new();
        productFinderServiceMock
           .Setup(c => c.FindById(It.IsAny<int>()))
           .ReturnsAsync((CategoryDto?) null)
        ;
        return productFinderServiceMock.Object;
    }
}