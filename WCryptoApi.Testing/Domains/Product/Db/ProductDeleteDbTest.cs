using System.Threading.Tasks;
using WCryptoApi.Infrastructure.Dao.Product;
using Xunit;
using Xunit.Abstractions;

namespace WCryptoApi.Testing.Domains.Product.Db;

public class ProductDeleteDbTest: DatabaseTest<ProductDeleteDb>
{
    public ProductDeleteDbTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task Delete_ExistingRecord_ShouldPass()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new ProductDeleteDb();
        const int productId = 1;
        bool success = await TestTarget.Delete(productId);
        Assert.True(success);
    }
    
    [Fact]
    public async Task Delete_NonExistingRecord_ShouldFail()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new ProductDeleteDb();
        const int productId = 100;
        bool success = await TestTarget.Delete(productId);
        Assert.False(success);
    }
    
    [Fact]
    public async Task Delete_DeletedRecord_ShouldFail()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new ProductDeleteDb();
        const int productId = 3;
        bool success = await TestTarget.Delete(productId);
        Assert.False(success);
    }
}