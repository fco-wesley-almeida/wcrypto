using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using WCryptoApi.Business.Services.Category;
using WCryptoApi.Core.Data.Category;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Core.Exceptions;
using WCryptoApi.Core.Services.Category;
using WCryptoApi.Testing.Utils;
using Xunit;
using Xunit.Abstractions;

namespace WCryptoApi.Testing.Domains.Category.Services;

public class CategoryDeleteServiceTest: UnitTestBase<CategoryDeleteService>
{
    public CategoryDeleteServiceTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task Delete_ExistingRecord_ShouldPass()
    {
        int categoryId = NumberMockUtil.RandomIntGt0();
        CategoryDto categoryDto = new()
        {
            CategoryId = categoryId,
            Description = StringMockUtil.RandomString(100),
            UserId = NumberMockUtil.RandomIntGt0()
        };
        TestTarget = new CategoryDeleteService(
            MockCategoryDeleteDb(true),
            MockCategoryFinderService(categoryId, categoryDto)
        );
        Assert.Equal(
        JsonConvert.SerializeObject(categoryDto),
        JsonConvert.SerializeObject(await TestTarget.DeleteById(categoryId))
        );
    }
    
    [Fact]
    public async Task Delete_NonExistingRecord_ShouldThrowHttpNotFoundException()
    {
        int categoryId = NumberMockUtil.RandomIntGt0();
        TestTarget = new CategoryDeleteService(
            MockCategoryDeleteDb(false),
            MockCategoryFinderServiceThrowingException()
        );
        await Assert.ThrowsAsync<HttpNotFoundException>(() => TestTarget.DeleteById(categoryId));
    }
    
    [Fact]
    public async Task Delete_OnFailureOfDeleting_ShouldFail()
    {
        int categoryId = NumberMockUtil.RandomIntGt0();
        CategoryDto categoryDto = new()
        {
            CategoryId = categoryId,
            Description = StringMockUtil.RandomString(100),
            UserId = NumberMockUtil.RandomIntGt0()
        };
        TestTarget = new CategoryDeleteService(
            MockCategoryDeleteDb(false),
            MockCategoryFinderService(categoryId, categoryDto)
        );
        await Assert.ThrowsAsync<HttpBadRequestException>(() => TestTarget.DeleteById(categoryId));
    }

    private ICategoryDeleteDb MockCategoryDeleteDb(bool result)
    {
        Mock<ICategoryDeleteDb> categoryDeleteDbMock = new Mock<ICategoryDeleteDb>();
        categoryDeleteDbMock
           .Setup(c => c.Delete(It.IsAny<int>()))
           .ReturnsAsync(result);
        return categoryDeleteDbMock.Object;
    }
    
    private ICategoryFinderService MockCategoryFinderService(int categoryId, CategoryDto categoryDto)
    {
        Mock<ICategoryFinderService> categoryFinderServiceMock = new();
        categoryFinderServiceMock
           .Setup(c => c.FindById(categoryId))
           .ReturnsAsync(categoryDto);
        return categoryFinderServiceMock.Object;
    }
    
    private ICategoryFinderService MockCategoryFinderServiceThrowingException()
    {
        Mock<ICategoryFinderService> categoryFinderServiceMock = new();
        categoryFinderServiceMock
           .Setup(c => c.FindById(It.IsAny<int>()))
           .Throws(new HttpNotFoundException());
        return categoryFinderServiceMock.Object;
    }
}