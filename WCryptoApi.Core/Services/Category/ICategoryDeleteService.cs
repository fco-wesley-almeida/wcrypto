using WCryptoApi.Core.Dtos;

namespace WCryptoApi.Core.Services.Category;

public interface ICategoryDeleteService
{
    Task<CategoryDto> DeleteById(int categoryId);
}