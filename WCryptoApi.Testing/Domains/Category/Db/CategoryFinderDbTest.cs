using System.Threading.Tasks;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Infrastructure.Dao.Category;
using Xunit;
using Xunit.Abstractions;

namespace WCryptoApi.Testing.Domains.Category.Db;

public class CategoryFinderDbTest: DatabaseTest<CategoryFinderDb>
{
    public CategoryFinderDbTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task FindById_ExistingRecord_ShouldReturnRecord()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new CategoryFinderDb();
        const int categoryId = 1;
        CategoryDto? category = await TestTarget.FindById(categoryId);
        bool categoryExists = category is not null; 
        Assert.True(categoryExists);
    }
    
    [Fact]
    public async Task FindById_ExistingRecord_ShouldReturnRecordAndIdCorrespond()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new CategoryFinderDb();
        const int categoryId = 1;
        CategoryDto? category = await TestTarget.FindById(categoryId);
        bool categoryExists = category is not null && category.CategoryId == categoryId; 
        Assert.True(categoryExists);
    }
    
    [Fact]
    public async Task FindById_NonExistingRecord_ShouldReturnNull()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new CategoryFinderDb();
        const int categoryId = 100;
        CategoryDto? category = await TestTarget.FindById(categoryId);
        bool categoryExists = category is not null; 
        Assert.False(categoryExists);
    }
    
    [Fact]
    public async Task FindById_DeletedRecord_ShouldReturnNull()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new CategoryFinderDb();
        const int categoryId = 3;
        CategoryDto? category = await TestTarget.FindById(categoryId);
        bool categoryExists = category is not null; 
        Assert.False(categoryExists);
    }
}