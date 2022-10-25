using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Infrastructure.Dao.Product;
using Xunit;
using Xunit.Abstractions;

namespace WCryptoApi.Testing.Domains.Product.Db;

public class ProductListingDbTest: DatabaseTest<ProductListingDb>
{
    public ProductListingDbTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task FindAll_OnExistingRecords_ShouldReturnRecords()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new ProductListingDb();
        IEnumerable<ProductDto> categories = await TestTarget.FindAll();
        int categoriesCount = categories.Count();
        Assert.Equal(expected: 4, actual: categoriesCount);
    }
    
    [Fact]
    public async Task FindAllByCategoryId_WithExistingCategory_ShouldReturnCategoryAssociateRecords()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new ProductListingDb();
        IEnumerable<ProductDto> categories = await TestTarget.FindAllByCategoryId(4);
        int categoriesCount = categories.Count();
        Assert.Equal(expected: 2, actual: categoriesCount);
    }
    
    [Fact]
    public async Task FindAllByCategoryId_WithNonExistingCategory_ShouldReturnEmpty()
    {
        await CreatePreConditionsForTesting();
        TestTarget = new ProductListingDb();
        IEnumerable<ProductDto> categories = await TestTarget.FindAllByCategoryId(3);
        int categoriesCount = categories.Count();
        Assert.Equal(expected: 0, actual: categoriesCount);
    }
}