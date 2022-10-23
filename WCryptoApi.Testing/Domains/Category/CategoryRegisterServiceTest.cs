using System;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using WCryptoApi.Business.Services.Category;
using WCryptoApi.Core.Data.Category;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Core.Exceptions;
using WCryptoApi.Core.Requests;
using WCryptoApi.Core.Services.Category;
using WCryptoApi.Testing.Utils;
using Xunit;
using Xunit.Abstractions;

namespace WCryptoApi.Testing.Domains.Category;

public class CategoryRegisterServiceTest: UnitTestBase<CategoryRegisterService>
{
    public CategoryRegisterServiceTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task Register_WithValidParams_ShouldPass()
    {
        int categoryId = NumberMockUtil.RandomIntGt0();
        CategoryRequest categoryRequest = new()
        {
            Description = StringMockUtil.RandomString(100),
            UserId = NumberMockUtil.RandomIntGt0()
        };
        TestTarget = new CategoryRegisterService(
            categoryRegisterDb: MockCategoryRegisterDb(categoryId), 
            categoryFinderService: MockCategoryFinderService(categoryId, categoryRequest)
        );
        Assert.Equal(
            expected: JsonConvert.SerializeObject(new Core.Entities.Category(
                categoryId: categoryId,
                description: categoryRequest.Description,
                userId: categoryRequest.UserId
            )),
            actual: JsonConvert.SerializeObject(await TestTarget.Register(categoryRequest))
        );
    }
    
    [Fact]
    public async Task Register_WithInvalidParams_ShouldThrowArgumentException()
    {
        int categoryId = NumberMockUtil.RandomIntGt0();
        CategoryRequest categoryRequest = new()
        {
            Description = StringMockUtil.RandomString(101),
            UserId = NumberMockUtil.RandomIntGt0()
        };
        TestTarget = new CategoryRegisterService(
            categoryRegisterDb: MockCategoryRegisterDb(categoryId), 
            categoryFinderService: MockCategoryFinderService(categoryId, categoryRequest)
        );
        await Assert.ThrowsAsync<ArgumentException>(() => TestTarget.Register(categoryRequest));
    }
    
    [Fact]
    public async Task Register_WithExceptionOnDatabaseLayer_ShouldThrowHttpBadRequestException()
    {
        int categoryId = NumberMockUtil.RandomIntGt0();
        CategoryRequest categoryRequest = new()
        {
            Description = StringMockUtil.RandomString(100),
            UserId = NumberMockUtil.RandomIntGt0()
        };
        TestTarget = new CategoryRegisterService(
            categoryRegisterDb: MockCategoryRegisterDb(), 
            categoryFinderService: MockCategoryFinderService(categoryId, categoryRequest)
        );
        await Assert.ThrowsAsync<HttpBadRequestException>(() => TestTarget.Register(categoryRequest));
    }

    private ICategoryRegisterDb MockCategoryRegisterDb(int categoryId)
    {
        Mock<ICategoryRegisterDb> categoryRegisterDbMock = new();
        categoryRegisterDbMock
           .Setup(c => 
                c.Register(It.IsAny<Core.Entities.Category>())
            )
           .ReturnsAsync(categoryId)
        ;
        return categoryRegisterDbMock.Object;
    }
    
    private ICategoryRegisterDb MockCategoryRegisterDb()
    {
        Mock<ICategoryRegisterDb> categoryRegisterDbMock = new();
        categoryRegisterDbMock
           .Setup(c => 
                c.Register(It.IsAny<Core.Entities.Category>())
            )
           .Throws(new Exception())
        ;
        return categoryRegisterDbMock.Object;
    }

    private ICategoryFinderService MockCategoryFinderService(int categoryId, CategoryRequest category)
    {
        Mock<ICategoryFinderService> categoryFinderServiceMock = new();
        categoryFinderServiceMock
           .Setup(c => c.FindById(categoryId))
           .ReturnsAsync(new CategoryDto {
                CategoryId = categoryId,
                Description = category.Description,
                UserId = category.UserId
            })
        ;
        return categoryFinderServiceMock.Object;
    }
}