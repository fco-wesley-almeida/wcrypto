using System.ComponentModel.DataAnnotations;
using WCryptoApi.Core.Data.Category;
using WCryptoApi.Core.Dtos;

namespace WCryptoApi.Core.Entities;

public class Product
{
    public int    ProductId   { get; }
    public string Description { get; }
    public int    CategoryId  { get;  }

    public Product(int productId, string description, int categoryId)
    {
        ValidateProductId(productId);
        ValidateCategoryId(categoryId);
        ValidateCategoryId(categoryId);
        ProductId   = productId;
        Description = description;
        CategoryId  = categoryId;
    }

    public Product(ProductRequest productRequest)
    {
        ValidateDescription(productRequest.Description);
        ValidateCategoryId(productRequest.CategoryId);
        Description = productRequest.Description;
        CategoryId  = productRequest.CategoryId;
        ProductId   = 0;
    }
    
    public Product(ProductDto productDto)
    {
        ValidateDescription(productDto.Description);
        ValidateCategoryId(productDto.CategoryId);
        ValidateProductId(productDto.ProductId);
        Description = productDto.Description;
        CategoryId  = productDto.CategoryId;
        ProductId   = productDto.ProductId;
    }

    private static void ValidateDescription(string? description)
    {
        if (string.IsNullOrEmpty(description) || description.Length > 100)
        {
            throw new ArgumentException("Description is not valid.");
        }   
    }

    private static void ValidateCategoryId(int categoryId)
    {
        if (categoryId <= 0)
        {
            throw new ArgumentException("CategoryId is not valid.");
        }
    }
    
    private static void ValidateProductId(int productId)
    {
        if (productId <= 0)
        {
            throw new ArgumentException("ProductId is not valid.");
        }
    }
}