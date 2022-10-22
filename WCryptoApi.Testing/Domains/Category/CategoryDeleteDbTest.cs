using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Infrastructure.Dao.Category;
using Xunit;
using Xunit.Abstractions;

namespace WCryptoApi.Testing.Domains.Category;

public class CategoryDeleteDbTest: DatabaseTest<CategoryDeleteDb>
{
    public CategoryDeleteDbTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task Delete_ExistingId_ShouldPass()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new CategoryDeleteDb();
        const int categoryId = 1;
        bool success = await TestTarget.Delete(categoryId);
        Assert.True(success);
    }
    
    [Fact]
    public async Task Delete_NonExistingId_ShouldFail()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new CategoryDeleteDb();
        const int categoryId = 100;
        bool success = await TestTarget.Delete(categoryId);
        Assert.False(success);
    }
    
    [Fact]
    public async Task Delete_DeletedRecord_ShouldFail()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new CategoryDeleteDb();
        const int categoryId = 3;
        bool success = await TestTarget.Delete(categoryId);
        Assert.False(success);
    }
}