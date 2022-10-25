using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using WCryptoApi.Business.Services.Category;
using WCryptoApi.Core.Data.Category;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Core.Exceptions;
using WCryptoApi.Testing.Utils;
using Xunit;
using Xunit.Abstractions;

namespace WCryptoApi.Testing.Domains.Category.Services;

public class CategoryFinderServiceTest: UnitTestBase<CategoryFinderService>
{
    public CategoryFinderServiceTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task FindAll_WithExistingRecord_ShouldReturnRecord()
    {
        int categoryId = NumberMockUtil.RandomIntGt0();
        CategoryDto categoryMock = CategoryDtoMock(categoryId);
        ICategoryFinderDb categoryFinderDbMock = CategoryFinderDbMock(categoryMock);
        TestTarget = new CategoryFinderService(categoryFinderDbMock);
        CategoryDto category = await TestTarget.FindById(categoryId);
        Assert.Equal(
            expected: JsonConvert.SerializeObject(categoryMock), 
            actual: JsonConvert.SerializeObject(category)
        );
    }
    
    [Fact]
    public async Task FindAll_WithNonExistingRecord_ShouldThrowHttpNotFoundException()
    {
        int categoryId = NumberMockUtil.RandomIntGt0();
        ICategoryFinderDb categoryFinderDbMock = CategoryFinderDbMockReturningNull();
        TestTarget = new CategoryFinderService(categoryFinderDbMock);
        await Assert.ThrowsAsync<HttpNotFoundException>(() => TestTarget.FindById(categoryId));
    }

    private CategoryDto CategoryDtoMock(int categoryId)
    {
        return new CategoryDto
        {
            CategoryId = categoryId,
            Description = StringMockUtil.RandomString(100),
            UserId = NumberMockUtil.RandomIntGt0()
        };
    }

    private ICategoryFinderDb CategoryFinderDbMock(CategoryDto categoryMock)
    {
        Mock<ICategoryFinderDb> categoryFinderDbMock = new Mock<ICategoryFinderDb>();
        categoryFinderDbMock
           .Setup(c => c.FindById(It.IsAny<int>()))
           .ReturnsAsync(categoryMock)
        ;
        return categoryFinderDbMock.Object;
    }
    
    private ICategoryFinderDb CategoryFinderDbMockReturningNull()
    {
        Mock<ICategoryFinderDb> categoryFinderDbMock = new Mock<ICategoryFinderDb>();
        categoryFinderDbMock
           .Setup(c => c.FindById(It.IsAny<int>()))
           .ReturnsAsync((CategoryDto?) null)
        ;
        return categoryFinderDbMock.Object;
    }
}