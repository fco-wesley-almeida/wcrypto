using WCryptoApi.Core.Data.Category;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Core.Exceptions;
using WCryptoApi.Core.Services.Category;

namespace WCryptoApi.Business.Services.Category;

public class CategoryDeleteService: ICategoryDeleteService
{
    private readonly ICategoryDeleteDb   _categoryDeleteDb;
    private readonly ICategoryFinderService _categoryFinderService;

    public CategoryDeleteService(ICategoryDeleteDb categoryDeleteDb, ICategoryFinderService categoryFinderService)
    {
        _categoryDeleteDb = categoryDeleteDb;
        _categoryFinderService = categoryFinderService;
    }

    public async Task<CategoryDto> DeleteById(int categoryId)
    {
        CategoryDto category = await _categoryFinderService.FindById(categoryId);
        if (!await _categoryDeleteDb.Delete(categoryId))
        {
            throw new HttpBadRequestException("This category could not be deleted.");    
        }
        return category;
    }
}