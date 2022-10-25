using System.Threading.Tasks;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Infrastructure.Dao.Product;
using Xunit;
using Xunit.Abstractions;

namespace WCryptoApi.Testing.Domains.Product.Db;

public class ProductFinderDbTest: DatabaseTest<ProductFinderDb>
{
    public ProductFinderDbTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task FindById_ExistingRecord_ShouldReturnRecord()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new ProductFinderDb();
        const int productId = 1;
        ProductDto? product = await TestTarget.FindById(productId);
        bool productExists = product is not null; 
        Assert.True(productExists);
    }
    
    [Fact]
    public async Task FindById_ExistingRecord_ShouldReturnRecordAndIdCorrespond()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new ProductFinderDb();
        const int productId = 1;
        ProductDto? product = await TestTarget.FindById(productId);
        bool productExists = product is not null && product.ProductId == productId; 
        Assert.True(productExists);
    }
    
    [Fact]
    public async Task FindById_NonExistingRecord_ShouldReturnNull()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new ProductFinderDb();
        const int productId = 100;
        ProductDto? product = await TestTarget.FindById(productId);
        bool productExists = product is not null; 
        Assert.False(productExists);
    }
    
    [Fact]
    public async Task FindById_DeletedRecord_ShouldReturnNull()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new ProductFinderDb();
        const int productId = 3;
        ProductDto? product = await TestTarget.FindById(productId);
        bool productExists = product is not null; 
        Assert.False(productExists);
    }
    
    [Fact]
    public async Task FindById_ActiveProductAndDeletedCategory_ShouldReturnNull()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new ProductFinderDb();
        const int productId = 4;
        ProductDto? product = await TestTarget.FindById(productId);
        bool productExists = product is not null; 
        Assert.False(productExists);
    }
}