using System;
using System.Collections.Generic;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Core.Requests;
using WCryptoApi.Testing.Utils;
using Xunit;
using Xunit.Abstractions;

namespace WCryptoApi.Testing.Domains.Category;

public class CategoryTest: UnitTestBase<Core.Entities.Category>
{
    
    public CategoryTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Theory]
    [MemberData(nameof(InvalidArgsForNormalConstructor))]
    public void Constructor_WithInvalidArgs_ShouldThrowArgumentException(int categoryId, string description, int userId)
    {
        TestOutputHelper.WriteLine(@$"
            The category should not be created with params (
                categoryId: {categoryId},
                description: {description},
                userId: {userId}
            )
        ");
        Assert.Throws<ArgumentException>(() => new Core.Entities.Category(categoryId, description, userId));
    }

    private static IEnumerable<object[]> InvalidArgsForNormalConstructor()
    {
        yield return new object[] { 0, "", 0 };
        yield return new object[] { NumberMockUtil.RandomIntLt0(), "", NumberMockUtil.RandomIntGt0() };
        yield return new object[] { 0, StringMockUtil.RandomString(4), NumberMockUtil.RandomIntLt0() };
        yield return new object[] { NumberMockUtil.RandomIntLt0(), StringMockUtil.RandomString(4),NumberMockUtil.RandomIntGt0()};
        yield return new object[] { NumberMockUtil.RandomIntGt0(), "", NumberMockUtil.RandomIntLt0() };
        yield return new object[] { NumberMockUtil.RandomIntGt0(), "",NumberMockUtil.RandomIntGt0()};
        yield return new object[] { NumberMockUtil.RandomIntGt0(), StringMockUtil.RandomString(101),NumberMockUtil.RandomIntGt0()};
        yield return new object[] { NumberMockUtil.RandomIntGt0(), "    ", int.MaxValue };
    }
    
    [Theory]
    [MemberData(nameof(ValidArgsForNormalConstructor))]
    public void Constructor_WithValidArgs_ShouldPass(int categoryId, string description, int userId)
    {
        TestTarget = new Core.Entities.Category(categoryId, description, userId);
        TestOutputHelper.WriteLine($"The category object was created successfully: ({TestTarget})");
        Assert.Equal(categoryId, TestTarget.CategoryId);
        Assert.Equal(description, TestTarget.Description);
        Assert.Equal(userId, TestTarget.UserId);
    }

    private static IEnumerable<object[]> ValidArgsForNormalConstructor()
    {
        yield return new object[] { NumberMockUtil.RandomIntGt0(), StringMockUtil.RandomString(100), NumberMockUtil.RandomIntGt0() };
        yield return new object[] { int.MaxValue, StringMockUtil.RandomString(1), int.MaxValue };
    }
    
    [Theory]
    [MemberData(nameof(InvalidArgsForCategoryRequestConstructor))]
    public void ConstructorUsingCategoryRequest_WithInvalidArgs_ShouldThrowArgumentException(CategoryRequest categoryRequest)
    {
        TestOutputHelper.WriteLine(@$"
            The category should not be created with params (
                description: {categoryRequest.Description},
                userId: {categoryRequest.UserId}
            )
        ");
        Assert.Throws<ArgumentException>(() => new Core.Entities.Category(categoryRequest));
    }

    private static IEnumerable<object[]> InvalidArgsForCategoryRequestConstructor()
    {
        yield return new object[] {new CategoryRequest { Description  = "", UserId                               = 0}};
        yield return new object[] {new CategoryRequest { Description  = "", UserId                               = NumberMockUtil.RandomIntLt0()}};
        yield return new  object[] {new CategoryRequest { Description = StringMockUtil.RandomString(100), UserId = NumberMockUtil.RandomIntLt0()}};
        yield return new object[] {new CategoryRequest { Description  = StringMockUtil.RandomString(101), UserId = NumberMockUtil.RandomIntGt0()}};
        yield return new object[] {new CategoryRequest { Description  = "       ", UserId = NumberMockUtil.RandomIntGt0()}};
    }
    
    [Theory]
    [MemberData(nameof(ValidArgsForCategoryRequestConstructor))]
    public void ConstructorUsingCategoryRequest_WithValidArgs_ShouldPass(CategoryRequest categoryRequest)
    {
        TestOutputHelper.WriteLine($"The category object was created successfully: ({TestTarget})");
        TestTarget = new Core.Entities.Category(categoryRequest);
        Assert.Equal(expected: 0, TestTarget.CategoryId);
        Assert.Equal(categoryRequest.Description, TestTarget.Description);
        Assert.Equal(categoryRequest.UserId, TestTarget.UserId);
    }

    private static IEnumerable<object[]> ValidArgsForCategoryRequestConstructor()
    {
        yield return new object[] 
        {
            new CategoryRequest 
            { 
                Description  = StringMockUtil.RandomString(100),
                UserId = NumberMockUtil.RandomIntGt0()
            }
        };
        yield return new object[] 
        {
            new CategoryRequest 
            { 
                Description  = StringMockUtil.RandomString(1),
                UserId = int.MaxValue
            }
        };
        yield return new object[]
        {
            new CategoryRequest
            {
                Description = "   " + StringMockUtil.RandomString(1) + "   ",
                UserId      = int.MaxValue
            }
        };
    }
    
    [Theory]
    [MemberData(nameof(InvalidArgsForCategoryDtoConstructor))]
    public void ConstructorUsingCategoryDto_WithInvalidArgs_ShouldFail(CategoryDto categoryDto)
    {
        TestOutputHelper.WriteLine(@$"
            The category should not be created with params (
                categoryId: {categoryDto.CategoryId}
                description: {categoryDto.Description},
                userId: {categoryDto.UserId}
            )
        ");
        Assert.Throws<ArgumentException>(() => new Core.Entities.Category(categoryDto));
    }

    private static IEnumerable<object[]> InvalidArgsForCategoryDtoConstructor()
    {
        yield return new object[] {
            new CategoryDto 
            {
                Description  = "", 
                UserId = 0
            }
        };
        yield return new object[] {
            new CategoryDto 
            {
                Description  = "", 
                UserId  = NumberMockUtil.RandomIntLt0()
            }
        };
        yield return new  object[] {
            new CategoryDto 
            {
                Description = StringMockUtil.RandomString(100), 
                UserId = NumberMockUtil.RandomIntLt0()
            }
        };
        yield return new object[] {
            new CategoryDto 
            {
                Description  = StringMockUtil.RandomString(101), 
                UserId = NumberMockUtil.RandomIntGt0()
            }
        };
        yield return new object[] {
            new CategoryDto 
            {
                Description  = "       ", 
                UserId = NumberMockUtil.RandomIntGt0()
            }
        };
        yield return new object[] {
            new CategoryDto 
            {
                CategoryId = 0, Description  = StringMockUtil.RandomString(100), 
                UserId = int.MaxValue
            }
        };
        yield return new object[] {
            new CategoryDto 
            {
                CategoryId = NumberMockUtil.RandomIntLt0(), Description  = StringMockUtil.RandomString(100), 
                UserId = int.MaxValue
            }
        };
    }
    
    [Theory]
    [MemberData(nameof(ValidArgsForCategoryDtoConstructor))]
    public void ConstructorUsingCategoryDto_WithValidArgs_ShouldPass(CategoryDto categoryDto)
    {
        TestTarget = new Core.Entities.Category(categoryDto);
        Assert.Equal(categoryDto.CategoryId, TestTarget.CategoryId);
        Assert.Equal(categoryDto.Description, TestTarget.Description);
        Assert.Equal(categoryDto.UserId, TestTarget.UserId);
        TestOutputHelper.WriteLine($"The category object was created successfully: ({TestTarget})");
    }

    private static IEnumerable<object[]> ValidArgsForCategoryDtoConstructor()
    {
        yield return new object[]
        {
            new CategoryDto
            {
                CategoryId = NumberMockUtil.RandomIntGt0(), 
                Description  = StringMockUtil.RandomString(100), 
                UserId = NumberMockUtil.RandomIntGt0()
            }
        };
        yield return new object[]
        {
            new CategoryDto
            {
                CategoryId = NumberMockUtil.RandomIntGt0(), 
                Description = StringMockUtil.RandomString(1), 
                UserId = int.MaxValue
            }
        };
        yield return new object[]
        {
            new CategoryDto
            {
                CategoryId = int.MaxValue, 
                Description  = "   " + StringMockUtil.RandomString(100) + "   ", 
                UserId = int.MaxValue
            }
        };
    }


}