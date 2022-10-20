using WCryptoApi.Core.Dtos;

namespace WCryptoApi.Core.Data.Category;

public interface ICategoryListingDb
{
    Task<IEnumerable<CategoryDto>> FindAllByUserId(int userId);
    Task<IEnumerable<CategoryDto>> FindAll();
}