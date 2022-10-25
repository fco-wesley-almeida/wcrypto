using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using WCryptoApi.Core;
using WCryptoApi.Core.Requests;
using WCryptoApi.Infrastructure.Dao.Product;
using WCryptoApi.Testing.Utils;
using Xunit;
using Xunit.Abstractions;

namespace WCryptoApi.Testing.Domains.Product.Db;

public class ProductRegisterDbTest: DatabaseTest<ProductRegisterDb>
{
    public ProductRegisterDbTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task Register_ValidRecord_ShouldPass()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new ProductRegisterDb();
        var product = new Core.Entities.Product(new ProductRequest
        {
            Description = "Test",
            CategoryId = 1
        });
        int productId = await TestTarget.Register(product);
        Assert.True(productId > 0);
    }
    
    [Fact]
    public async Task Register_WithNonExistingCategory_ShouldThrowMySqlException()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new ProductRegisterDb();
        var product = new Core.Entities.Product(new ProductRequest
        {
            Description = "Test",
            CategoryId = 100
        });
        await Assert.ThrowsAsync<MySqlException>(() => TestTarget.Register(product));
    }
    
    [Fact]
    public async Task Register_WithDeletedCategory_ShouldPass()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new ProductRegisterDb();
        var product = new Core.Entities.Product(new ProductRequest
        {
            Description = StringMockUtil.RandomString(100),
            CategoryId = 3
        });
        int productId = await TestTarget.Register(product);
        Assert.True(productId > 0);
    }
}