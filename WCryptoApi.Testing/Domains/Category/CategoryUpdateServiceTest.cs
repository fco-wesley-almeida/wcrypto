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

public class CategoryUpdateServiceTest: UnitTestBase<CategoryUpdateService>
{
    public CategoryUpdateServiceTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task Update_WithValidParams_ShouldPass()
    {
        int categoryId = NumberMockUtil.RandomIntGt0();
        CategoryRequest categoryRequest = new()
        {
            Description = StringMockUtil.RandomString(100),
            UserId = NumberMockUtil.RandomIntGt0()
        };
        TestTarget = new CategoryUpdateService(
            categoryUpdateDb: MockCategoryUpdateDb(true), 
            categoryFinderService: MockCategoryFinderService(categoryId, categoryRequest)
        );
        Assert.Equal(
            expected: JsonConvert.SerializeObject(new Core.Entities.Category(
                categoryId: categoryId,
                description: categoryRequest.Description,
                userId: categoryRequest.UserId
            )),
            actual: JsonConvert.SerializeObject(await TestTarget.Update(categoryId, categoryRequest))
        );
    }
    
    [Fact]
    public async Task Update_WithInvalidParams_ShouldThrowArgumentException()
    {
        int categoryId = NumberMockUtil.RandomIntGt0();
        CategoryRequest categoryRequest = new()
        {
            Description = StringMockUtil.RandomString(101),
            UserId = NumberMockUtil.RandomIntGt0()
        };
        TestTarget = new CategoryUpdateService(
            categoryUpdateDb: MockCategoryUpdateDb(true), 
            categoryFinderService: MockCategoryFinderService(categoryId, categoryRequest)
        );
        await Assert.ThrowsAsync<ArgumentException>(() => TestTarget.Update(categoryId, categoryRequest));
    }
    
    [Fact]
    public async Task Update_WithExceptionOnDatabaseLayer_ShouldThrowHttpBadRequestException()
    {
        int categoryId = NumberMockUtil.RandomIntGt0();
        CategoryRequest categoryRequest = new()
        {
            Description = StringMockUtil.RandomString(100),
            UserId = NumberMockUtil.RandomIntGt0()
        };
        TestTarget = new CategoryUpdateService(
            categoryUpdateDb: MockCategoryUpdateDbThrowingException(), 
            categoryFinderService: MockCategoryFinderService(categoryId, categoryRequest)
        );
        await Assert.ThrowsAsync<HttpBadRequestException>(() => TestTarget.Update(categoryId, categoryRequest));
    }
    
    [Fact]
    public async Task Update_NonExistingRecord_ShouldThrowHttpBadRequestException()
    {
        int categoryId = NumberMockUtil.RandomIntGt0();
        CategoryRequest categoryRequest = new()
        {
            Description = StringMockUtil.RandomString(100),
            UserId = NumberMockUtil.RandomIntGt0()
        };
        TestTarget = new CategoryUpdateService(
            categoryUpdateDb: MockCategoryUpdateDb(false), 
            categoryFinderService: MockCategoryFinderService(categoryId, categoryRequest)
        );
        await Assert.ThrowsAsync<HttpBadRequestException>(() => TestTarget.Update(categoryId, categoryRequest));
    }

    private ICategoryUpdateDb MockCategoryUpdateDb(bool result)
    {
        Mock<ICategoryUpdateDb> categoryUpdateDbMock = new();
        categoryUpdateDbMock
           .Setup(c => 
                c.Update(It.IsAny<Core.Entities.Category>())
            )
           .ReturnsAsync(result)
        ;
        return categoryUpdateDbMock.Object;
    }
    
    private ICategoryUpdateDb MockCategoryUpdateDbThrowingException()
    {
        Mock<ICategoryUpdateDb> categoryUpdateDbMock = new();
        categoryUpdateDbMock
           .Setup(c => 
                c.Update(It.IsAny<Core.Entities.Category>())
            )
           .Throws(new Exception())
        ;
        return categoryUpdateDbMock.Object;
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