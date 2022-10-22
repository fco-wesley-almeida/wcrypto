using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Infrastructure.Dao.Category;
using Xunit;
using Xunit.Abstractions;

namespace WCryptoApi.Testing.Domains.Category;

public class CategoryListingDbTest: DatabaseTest<CategoryListingDb>
{
    public CategoryListingDbTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task List_All_Non_Deleted_Records_Should_Pass()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new CategoryListingDb();
        IEnumerable<CategoryDto> categories = await TestTarget.FindAll();
        int categoriesCount = categories.Count();
        Assert.Equal(expected: 4, actual: categoriesCount);
    }
    
    [Fact]
    public async Task List_All_Non_Deleted_Records_Of_A_User_Should_Pass()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new CategoryListingDb();
        IEnumerable<CategoryDto> categories = await TestTarget.FindAllByUserId(1);
        int categoriesCount = categories.Count();
        Assert.Equal(expected: 4, actual: categoriesCount);
    }
}