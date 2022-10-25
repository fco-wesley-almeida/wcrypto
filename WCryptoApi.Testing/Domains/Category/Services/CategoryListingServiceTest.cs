using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using WCryptoApi.Business.Services.Category;
using WCryptoApi.Core.Data.Category;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Core.Exceptions;
using Xunit;
using Xunit.Abstractions;

namespace WCryptoApi.Testing.Domains.Category.Services;

public class CategoryListingServiceTest: UnitTestBase<CategoryListingService>
{
    public CategoryListingServiceTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task FindAll_OnExistingRecords_ShouldReturnValues()
    {
        
        IEnumerable<CategoryDto> categoriesMock = MockCategoryDtos();
        ICategoryListingDb categoryListingDbMock = CategoryListingDbMock(categoriesMock);
        TestTarget = new CategoryListingService(categoryListingDbMock);
        List<CategoryDto> categories = await TestTarget.FindAll();
        Assert.Equal(
            expected: JsonConvert.SerializeObject(categoriesMock), 
            actual: JsonConvert.SerializeObject(categories)
        );
    }
    
    [Fact]
    public async Task FindAll_OnNotReturningRecords_ShouldThrowHttpNotFoundException()
    {;
        ICategoryListingDb categoryListingDbMock = CategoryListingDbMock(new List<CategoryDto>());
        TestTarget = new CategoryListingService(categoryListingDbMock);
        await Assert.ThrowsAsync<HttpNotFoundException>(() => TestTarget.FindAll());
    }

    private IEnumerable<CategoryDto> MockCategoryDtos()
    {
        return new List<CategoryDto>
        {
            new CategoryDto { CategoryId = 1, UserId = 1, Description = "test 1" },
            new CategoryDto { CategoryId = 2, UserId = 1, Description = "test 2" }
        };
    }

    private ICategoryListingDb CategoryListingDbMock(IEnumerable<CategoryDto> categoriesMock)
    {
        Mock<ICategoryListingDb> categoryListingDbMock = new Mock<ICategoryListingDb>();
        categoryListingDbMock
           .Setup(c => c.FindAll())
           .ReturnsAsync(categoriesMock)
        ;
        return categoryListingDbMock.Object;
    }
}