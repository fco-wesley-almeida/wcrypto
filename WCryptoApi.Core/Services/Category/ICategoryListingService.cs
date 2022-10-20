using WCryptoApi.Core.Dtos;

namespace WCryptoApi.Core.Services.Category;

using Entities;
public interface ICategoryListingService
{
    Task<List<CategoryDto>> FindAll();
}