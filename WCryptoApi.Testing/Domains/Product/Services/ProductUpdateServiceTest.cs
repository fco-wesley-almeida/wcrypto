using System;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using WCryptoApi.Business.Services.Product;
using WCryptoApi.Core;
using WCryptoApi.Core.Data.Product;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Core.Exceptions;
using WCryptoApi.Core.Requests;
using WCryptoApi.Core.Services.Product;
using WCryptoApi.Testing.Utils;
using Xunit;
using Xunit.Abstractions;

namespace WCryptoApi.Testing.Domains.Product.Services;

public class ProductUpdateServiceTest: UnitTestBase<ProductUpdateService>
{
    public ProductUpdateServiceTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task Update_WithValidParams_ShouldPass()
    {
        int productId = NumberMockUtil.RandomIntGt0();
        ProductRequest productRequest = new()
        {
            Description = StringMockUtil.RandomString(100),
            CategoryId = NumberMockUtil.RandomIntGt0()
        };
        TestTarget = new ProductUpdateService(
            productUpdateDb: MockProductUpdateDb(true), 
            productFinderService: MockProductFinderService(productId, productRequest)
        );
        Assert.Equal(
            expected: JsonConvert.SerializeObject(new Core.Entities.Product(
                productId: productId,
                description: productRequest.Description,
                categoryId: productRequest.CategoryId
            )),
            actual: JsonConvert.SerializeObject(await TestTarget.Update(productId, productRequest))
        );
    }
    
    [Fact]
    public async Task Update_WithInvalidParams_ShouldThrowArgumentException()
    {
        int productId = NumberMockUtil.RandomIntGt0();
        ProductRequest productRequest = new()
        {
            Description = StringMockUtil.RandomString(101),
            CategoryId = NumberMockUtil.RandomIntGt0()
        };
        TestTarget = new ProductUpdateService(
            productUpdateDb: MockProductUpdateDb(true), 
            productFinderService: MockProductFinderService(productId, productRequest)
        );
        await Assert.ThrowsAsync<ArgumentException>(() => TestTarget.Update(productId, productRequest));
    }
    
    [Fact]
    public async Task Update_WithExceptionOnDatabaseLayer_ShouldThrowHttpBadRequestException()
    {
        int productId = NumberMockUtil.RandomIntGt0();
        ProductRequest productRequest = new()
        {
            Description = StringMockUtil.RandomString(100),
            CategoryId = NumberMockUtil.RandomIntGt0()
        };
        TestTarget = new ProductUpdateService(
            productUpdateDb: MockProductUpdateDbThrowingException(), 
            productFinderService: MockProductFinderService(productId, productRequest)
        );
        await Assert.ThrowsAsync<HttpBadRequestException>(() => TestTarget.Update(productId, productRequest));
    }
    
    [Fact]
    public async Task Update_NonExistingRecord_ShouldThrowHttpBadRequestException()
    {
        int productId = NumberMockUtil.RandomIntGt0();
        ProductRequest productRequest = new()
        {
            Description = StringMockUtil.RandomString(100),
            CategoryId = NumberMockUtil.RandomIntGt0()
        };
        TestTarget = new ProductUpdateService(
            productUpdateDb: MockProductUpdateDb(false), 
            productFinderService: MockProductFinderService(productId, productRequest)
        );
        await Assert.ThrowsAsync<HttpBadRequestException>(() => TestTarget.Update(productId, productRequest));
    }

    private IProductUpdateDb MockProductUpdateDb(bool result)
    {
        Mock<IProductUpdateDb> productUpdateDbMock = new();
        productUpdateDbMock
           .Setup(c => 
                c.Update(It.IsAny<Core.Entities.Product>())
            )
           .ReturnsAsync(result)
        ;
        return productUpdateDbMock.Object;
    }
    
    private IProductUpdateDb MockProductUpdateDbThrowingException()
    {
        Mock<IProductUpdateDb> productUpdateDbMock = new();
        productUpdateDbMock
           .Setup(c => 
                c.Update(It.IsAny<Core.Entities.Product>())
            )
           .Throws(new Exception())
        ;
        return productUpdateDbMock.Object;
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
}