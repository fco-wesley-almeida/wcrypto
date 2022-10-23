using WCryptoApi.Core.Data.Category;
using WCryptoApi.Core.Exceptions;
using WCryptoApi.Core.Services.Category;

namespace WCryptoApi.Business.Services.Category;

public class CategoryDeleteService: ICategoryDeleteService
{
    private readonly ICategoryDeleteDb   _categoryDeleteDb;

    public CategoryDeleteService(ICategoryDeleteDb categoryDeleteDb)
    {
        _categoryDeleteDb = categoryDeleteDb;
    }

    public async Task DeleteById(int categoryId)
    {
        if (!await _categoryDeleteDb.Delete(categoryId))
        {
            throw new HttpBadRequestException("This category does not exists.");    
        }
    }
}