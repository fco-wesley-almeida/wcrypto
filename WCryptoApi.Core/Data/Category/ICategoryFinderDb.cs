using WCryptoApi.Core.Dtos;

namespace WCryptoApi.Core.Data.Category;

public interface ICategoryFinderDb
{
    Task<CategoryDto?> FindById(int categoryId);
}