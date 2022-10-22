using System;
using System.Collections.Generic;
using WCryptoApi.Core;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Core.Requests;
using WCryptoApi.Testing.Utils;
using Xunit;
using Xunit.Abstractions;

namespace WCryptoApi.Testing.Domains.Product;

public class ProductTest: UnitTestBase<Core.Entities.Product>
{
    
    public ProductTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Theory]
    [MemberData(nameof(InvalidArgsForNormalConstructor))]
    public void Build_Using_Normal_Constructor_With_Invalid_Args_Should_Fail(int productId, string description, int categoryId)
    {
        TestOutputHelper.WriteLine(@$"
            The product should not be created with params (
                productId: {productId},
                description: {description},
                categoryId: {categoryId}
            )
        ");
        Assert.Throws<ArgumentException>(() => new Core.Entities.Product(productId, description, categoryId));
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
    public void Build_Using_Normal_Constructor_With_Valid_Args_Should_Pass(int productId, string description, int categoryId)
    {
        TestTarget = new Core.Entities.Product(productId, description, categoryId);
        TestOutputHelper.WriteLine($"The product object was created successfully: ({TestTarget})");
        Assert.Equal(productId, TestTarget.ProductId);
        Assert.Equal(description, TestTarget.Description);
        Assert.Equal(categoryId, TestTarget.CategoryId);
    }

    private static IEnumerable<object[]> ValidArgsForNormalConstructor()
    {
        yield return new object[] { NumberMockUtil.RandomIntGt0(), StringMockUtil.RandomString(100), NumberMockUtil.RandomIntGt0() };
        yield return new object[] { int.MaxValue, StringMockUtil.RandomString(1), int.MaxValue };
    }
    
    [Theory]
    [MemberData(nameof(InvalidArgsForProductRequestConstructor))]
    public void Build_Using_ProductRequest_Constructor_With_Invalid_Args_Should_Fail(ProductRequest productRequest)
    {
        TestOutputHelper.WriteLine(@$"
            The product should not be created with params (
                description: {productRequest.Description},
                categoryId: {productRequest.CategoryId}
            )
        ");
        Assert.Throws<ArgumentException>(() => new Core.Entities.Product(productRequest));
    }

    private static IEnumerable<object[]> InvalidArgsForProductRequestConstructor()
    {
        yield return new object[] {new ProductRequest { Description  = "", CategoryId                               = 0}};
        yield return new object[] {new ProductRequest { Description  = "", CategoryId                               = NumberMockUtil.RandomIntLt0()}};
        yield return new  object[] {new ProductRequest { Description = StringMockUtil.RandomString(100), CategoryId = NumberMockUtil.RandomIntLt0()}};
        yield return new object[] {new ProductRequest { Description  = StringMockUtil.RandomString(101), CategoryId = NumberMockUtil.RandomIntGt0()}};
        yield return new object[] {new ProductRequest { Description  = "       ", CategoryId = NumberMockUtil.RandomIntGt0()}};
    }
    
    [Theory]
    [MemberData(nameof(ValidArgsForProductRequestConstructor))]
    public void Build_Using_ProductRequest_Constructor_With_Valid_Args_Should_Pass(ProductRequest productRequest)
    {
        TestOutputHelper.WriteLine($"The product object was created successfully: ({TestTarget})");
        TestTarget = new Core.Entities.Product(productRequest);
        Assert.Equal(expected: 0, TestTarget.ProductId);
        Assert.Equal(productRequest.Description, TestTarget.Description);
        Assert.Equal(productRequest.CategoryId, TestTarget.CategoryId);
    }

    private static IEnumerable<object[]> ValidArgsForProductRequestConstructor()
    {
        yield return new object[] 
        {
            new ProductRequest 
            { 
                Description  = StringMockUtil.RandomString(100),
                CategoryId = NumberMockUtil.RandomIntGt0()
            }
        };
        yield return new object[] 
        {
            new ProductRequest 
            { 
                Description  = StringMockUtil.RandomString(1),
                CategoryId = int.MaxValue
            }
        };
        yield return new object[]
        {
            new ProductRequest
            {
                Description = "   " + StringMockUtil.RandomString(1) + "   ",
                CategoryId      = int.MaxValue
            }
        };
    }
    
    [Theory]
    [MemberData(nameof(InvalidArgsForProductDtoConstructor))]
    public void Build_Using_ProductDto_Constructor_With_Invalid_Args_Should_Fail(ProductDto productDto)
    {
        TestOutputHelper.WriteLine(@$"
            The product should not be created with params (
                productId: {productDto.ProductId}
                description: {productDto.Description},
                categoryId: {productDto.CategoryId}
            )
        ");
        Assert.Throws<ArgumentException>(() => new Core.Entities.Product(productDto));
    }

    private static IEnumerable<object[]> InvalidArgsForProductDtoConstructor()
    {
        yield return new object[] {
            new ProductDto 
            {
                Description  = "", 
                CategoryId = 0
            }
        };
        yield return new object[] {
            new ProductDto 
            {
                Description  = "", 
                CategoryId  = NumberMockUtil.RandomIntLt0()
            }
        };
        yield return new  object[] {
            new ProductDto 
            {
                Description = StringMockUtil.RandomString(100), 
                CategoryId = NumberMockUtil.RandomIntLt0()
            }
        };
        yield return new object[] {
            new ProductDto 
            {
                Description  = StringMockUtil.RandomString(101), 
                CategoryId = NumberMockUtil.RandomIntGt0()
            }
        };
        yield return new object[] {
            new ProductDto 
            {
                Description  = "       ", 
                CategoryId = NumberMockUtil.RandomIntGt0()
            }
        };
        yield return new object[] {
            new ProductDto 
            {
                ProductId = 0, Description  = StringMockUtil.RandomString(100), 
                CategoryId = int.MaxValue
            }
        };
        yield return new object[] {
            new ProductDto 
            {
                ProductId = NumberMockUtil.RandomIntLt0(), Description  = StringMockUtil.RandomString(100), 
                CategoryId = int.MaxValue
            }
        };
    }
    
    [Theory]
    [MemberData(nameof(ValidArgsForProductDtoConstructor))]
    public void Build_Using_ProductDto_Constructor_With_Valid_Args_Should_Pass(ProductDto productDto)
    {
        TestTarget = new Core.Entities.Product(productDto);
        Assert.Equal(productDto.ProductId, TestTarget.ProductId);
        Assert.Equal(productDto.Description, TestTarget.Description);
        Assert.Equal(productDto.CategoryId, TestTarget.CategoryId);
        TestOutputHelper.WriteLine($"The product object was created successfully: ({TestTarget})");
    }

    private static IEnumerable<object[]> ValidArgsForProductDtoConstructor()
    {
        yield return new object[]
        {
            new ProductDto
            {
                ProductId = NumberMockUtil.RandomIntGt0(), 
                Description  = StringMockUtil.RandomString(100), 
                CategoryId = NumberMockUtil.RandomIntGt0()
            }
        };
        yield return new object[]
        {
            new ProductDto
            {
                ProductId = NumberMockUtil.RandomIntGt0(), 
                Description = StringMockUtil.RandomString(1), 
                CategoryId = int.MaxValue
            }
        };
        yield return new object[]
        {
            new ProductDto
            {
                ProductId = int.MaxValue, 
                Description  = "   " + StringMockUtil.RandomString(100) + "   ", 
                CategoryId = int.MaxValue
            }
        };
    }


}