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

public class CategoryDeleteServiceTest: UnitTestBase<CategoryDeleteService>
{
    public CategoryDeleteServiceTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task Delete_ExistingRecord_ShouldPass()
    {
        int categoryId = NumberMockUtil.RandomIntGt0();;
        Mock<ICategoryDeleteDb> categoryDeleteDbMock = new Mock<ICategoryDeleteDb>();
        categoryDeleteDbMock
           .Setup(c => c.Delete(categoryId))
           .ReturnsAsync(true);
        TestTarget = new CategoryDeleteService(categoryDeleteDbMock.Object);
        await TestTarget.DeleteById(categoryId);
        Assert.True(true);
    }
    
    [Fact]
    public async Task Delete_ExistingRecord_ShouldThrowHttpBadRequestException()
    {
        int categoryId = NumberMockUtil.RandomIntGt0();
        Mock<ICategoryDeleteDb> categoryDeleteDbMock = new Mock<ICategoryDeleteDb>();
        categoryDeleteDbMock
           .Setup(c => c.Delete(categoryId))
           .ReturnsAsync(false)
        ;
        TestTarget = new CategoryDeleteService(categoryDeleteDbMock.Object);
        await Assert.ThrowsAsync<HttpBadRequestException>(() => TestTarget.DeleteById(categoryId));
    }
}