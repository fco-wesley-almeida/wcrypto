using System.Threading.Tasks;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Infrastructure.Dao.Category;
using Xunit;
using Xunit.Abstractions;

namespace WCryptoApi.Testing.Domains.Category.Db;

public class CategoryUpdateDbTest: DatabaseTest<CategoryUpdateDb>
{
    public CategoryUpdateDbTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task Update_NonDeletedRecord_ShouldPass()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new CategoryUpdateDb();
        Core.Entities.Category category = new (new CategoryDto {
            CategoryId = 1,
            Description = "Test",
            UserId = 1
        });
        bool success = await TestTarget.Update(category);
        Assert.True(success);
    }
    
    [Fact]
    public async Task Update_DeletedRecord_ShouldFail()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new CategoryUpdateDb();
        Core.Entities.Category category = new(new CategoryDto {
            CategoryId = 3,
            Description = "Test",
            UserId = 1
        });
        bool success = await TestTarget.Update(category);
        Assert.False(success);
    }
    
    [Fact]
    public async Task Update_NonExistingRecord_ShouldFail()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new CategoryUpdateDb();
        Core.Entities.Category category = new(new CategoryDto {
            CategoryId = 100,
            Description = "Test",
            UserId = 1
        });
        bool success = await TestTarget.Update(category);
        Assert.False(success);
    }
}