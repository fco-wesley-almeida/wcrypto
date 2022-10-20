using WCryptoApi.Core.Dtos;

namespace WCryptoApi.Core.Services.Category;

using Entities;

public interface ICategoryFinderService
{
    Task<CategoryDto> FindById(int categoryId);
}