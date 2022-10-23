using System.Collections.Generic;
using System.Linq;
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

namespace WCryptoApi.Testing.Domains.Category;

public class CategoryFinderServiceTest: UnitTestBase<CategoryFinderService>
{
    public CategoryFinderServiceTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task FindAll_WithExistingRecord_ShouldReturnRecord()
    {
        int categoryId = NumberMockUtil.RandomIntGt0();
        CategoryDto categoryMock = new CategoryDto()
        {
            CategoryId = categoryId,
            Description = StringMockUtil.RandomString(100),
            UserId = NumberMockUtil.RandomIntGt0()
        };
        Mock<ICategoryFinderDb> categoryFinderDbMock = new Mock<ICategoryFinderDb>();
        categoryFinderDbMock.Setup(c => c.FindById(categoryId)).ReturnsAsync(categoryMock);
        TestTarget = new CategoryFinderService(categoryFinderDbMock.Object);
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
        Mock<ICategoryFinderDb> categoryFinderDbMock = new Mock<ICategoryFinderDb>();
        categoryFinderDbMock
           .Setup(c => c.FindById(categoryId))
           .ReturnsAsync((CategoryDto?) null)
        ;
        TestTarget = new CategoryFinderService(categoryFinderDbMock.Object);
        await Assert.ThrowsAsync<HttpNotFoundException>(() => TestTarget.FindById(categoryId));
    }
}