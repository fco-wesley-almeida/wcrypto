using System.Threading.Tasks;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Infrastructure.Dao.Product;
using Xunit;
using Xunit.Abstractions;

namespace WCryptoApi.Testing.Domains.Product.Db;

public class ProductUpdateDbTest: DatabaseTest<ProductUpdateDb>
{
    public ProductUpdateDbTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task Update_NonDeletedRecord_ShouldPass()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new ProductUpdateDb();
        Core.Entities.Product product = new (new ProductDto {
            ProductId = 1,
            Description = "Test",
            CategoryId = 1
        });
        bool success = await TestTarget.Update(product);
        Assert.True(success);
    }
    
    [Fact]
    public async Task Update_DeletedRecord_ShouldFail()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new ProductUpdateDb();
        Core.Entities.Product product = new(new ProductDto {
            ProductId = 3,
            Description = "Test",
            CategoryId = 1
        });
        bool success = await TestTarget.Update(product);
        Assert.False(success);
    }
    
    [Fact]
    public async Task Update_NonExistingRecord_ShouldFail()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new ProductUpdateDb();
        Core.Entities.Product product = new(new ProductDto {
            ProductId = 100,
            Description = "Test",
            CategoryId = 1
        });
        bool success = await TestTarget.Update(product);
        Assert.False(success);
    }
}