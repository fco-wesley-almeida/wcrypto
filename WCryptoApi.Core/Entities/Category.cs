using System.ComponentModel.DataAnnotations;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Core.Requests;

namespace WCryptoApi.Core.Entities;

public class Category
{
    public Category(int categoryId, string description, int userId)
    {
        ValidateCategoryId(categoryId);
        ValidateDescription(description);
        ValidateUserId(userId);
        CategoryId  = categoryId;
        Description = description;
        UserId      = userId;
    }

    public Category(CategoryRequest categoryRequest)
    {
        ValidateDescription(categoryRequest.Description);
        ValidateUserId(categoryRequest.UserId);
        CategoryId  = 0;
        Description = categoryRequest.Description;
        UserId      = categoryRequest.UserId;
    }

    public Category(CategoryDto categoryDto)
    {
        ValidateCategoryId(categoryDto.CategoryId);
        ValidateDescription(categoryDto.Description);
        ValidateUserId(categoryDto.UserId);
        CategoryId  = categoryDto.CategoryId;
        Description = categoryDto.Description;
        UserId      = categoryDto.UserId;
    }
    public int    CategoryId          { get; }
    public string Description { get;  }
    public int    UserId      { get;  }

    private static void ValidateCategoryId(int categoryId)
    {
        if (categoryId <= 0)
        {
            throw new ArgumentException("CategoryId is invalid.");
        }
    }
    private static void ValidateDescription(string? description)
    {
        if (string.IsNullOrEmpty(description) || description.Length > 100)
        {
            throw new ArgumentException("Description is invalid.");
        }
    }
    private static void ValidateUserId(int userId)
    {
        if (userId <= 0)
        {
            throw new ArgumentException("UserId is invalid.");
        }
    }
}