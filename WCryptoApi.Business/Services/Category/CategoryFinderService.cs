using WCryptoApi.Core.Data.Category;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Core.Exceptions;
using WCryptoApi.Core.Services.Category;

namespace WCryptoApi.Business.Services.Category;

public class CategoryFinderService: ICategoryFinderService
{
    private readonly ICategoryFinderDb   _categoryFinderDb;

    public CategoryFinderService(ICategoryFinderDb categoryFinderDb)
    {
        _categoryFinderDb = categoryFinderDb;
    }

    public async Task<CategoryDto> FindById(int categoryId)
    {
        return await _categoryFinderDb.FindById(categoryId) ?? throw new HttpNotFoundException();
    }
}