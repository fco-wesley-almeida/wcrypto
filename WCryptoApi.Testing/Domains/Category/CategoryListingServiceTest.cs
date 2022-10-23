using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using WCryptoApi.Business.Services.Category;
using WCryptoApi.Core.Data.Category;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Core.Exceptions;
using Xunit;
using Xunit.Abstractions;

namespace WCryptoApi.Testing.Domains.Category;

public class CategoryListingServiceTest: UnitTestBase<CategoryListingService>
{
    public CategoryListingServiceTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task FindAll_OnExistingRecords_ShouldReturnValues()
    {
        Mock<ICategoryListingDb> categoryListingDbMock = new Mock<ICategoryListingDb>();
        IEnumerable<CategoryDto> categoriesMock = new List<CategoryDto>
        {
            new CategoryDto { CategoryId = 1, UserId = 1, Description = "test 1" },
            new CategoryDto { CategoryId = 2, UserId = 1, Description = "test 2" }
        };
        categoryListingDbMock.Setup(c => c.FindAll()).ReturnsAsync(categoriesMock);
        TestTarget = new CategoryListingService(categoryListingDbMock.Object);
        List<CategoryDto> categories = await TestTarget.FindAll();
        Assert.Equal(
            expected: JsonConvert.SerializeObject(categoriesMock), 
            actual: JsonConvert.SerializeObject(categories)
        );
    }
    
    [Fact]
    public async Task FindAll_OnNotReturningRecords_ShouldThrowHttpNotFoundException()
    {;
        Mock<ICategoryListingDb> categoryListingDbMock = new Mock<ICategoryListingDb>();
        IEnumerable<CategoryDto> categoriesMock = new List<CategoryDto>();
        categoryListingDbMock.Setup(c => c.FindAll()).ReturnsAsync(categoriesMock);
        TestTarget = new CategoryListingService(categoryListingDbMock.Object);
        await Assert.ThrowsAsync<HttpNotFoundException>(() => TestTarget.FindAll());
    }
}