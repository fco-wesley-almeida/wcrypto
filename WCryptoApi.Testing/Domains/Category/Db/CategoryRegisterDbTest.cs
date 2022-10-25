using System.Threading.Tasks;
using WCryptoApi.Core.Requests;
using WCryptoApi.Infrastructure.Dao.Category;
using Xunit;
using Xunit.Abstractions;

namespace WCryptoApi.Testing.Domains.Category.Db;

public class CategoryRegisterDbTest: DatabaseTest<CategoryRegisterDb>
{
    public CategoryRegisterDbTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task Register_ValidRecord_ShouldPass()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new CategoryRegisterDb();
        var category = new Core.Entities.Category(new CategoryRequest
        {
            Description = "Test",
            UserId = 1
        });
        int categoryId = await TestTarget.Register(category);
        Assert.True(categoryId > 0);
    }
}