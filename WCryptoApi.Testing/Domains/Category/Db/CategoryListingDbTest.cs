using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Infrastructure.Dao.Category;
using Xunit;
using Xunit.Abstractions;

namespace WCryptoApi.Testing.Domains.Category.Db;

public class CategoryListingDbTest: DatabaseTest<CategoryListingDb>
{
    public CategoryListingDbTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task FindAll_OnExistingRecords_ShouldReturnRecords()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new CategoryListingDb();
        IEnumerable<CategoryDto> categories = await TestTarget.FindAll();
        int categoriesCount = categories.Count();
        Assert.Equal(expected: 4, actual: categoriesCount);
    }
    
    [Fact]
    public async Task FindAllByUserId_WithExistingUser_ShouldReturnUserAssociateRecords()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new CategoryListingDb();
        IEnumerable<CategoryDto> categories = await TestTarget.FindAllByUserId(1);
        int categoriesCount = categories.Count();
        Assert.Equal(expected: 4, actual: categoriesCount);
    }
}