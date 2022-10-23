using WCryptoApi.Core.Data.Category;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Core.Exceptions;
using WCryptoApi.Core.Services.Category;

namespace WCryptoApi.Business.Services.Category;

public class CategoryListingService: ICategoryListingService
{
    private readonly ICategoryListingDb  _categoryListingDb;

    public CategoryListingService(ICategoryListingDb categoryListingDb)
    {
        _categoryListingDb = categoryListingDb;
    }

    public async Task<List<CategoryDto>> FindAll()
    {
        List<CategoryDto> categories = (await _categoryListingDb.FindAll()).ToList();
        if (categories.Count == 0)
        {
            throw new HttpNotFoundException();
        }
        return categories;
    }
}