using WCryptoApi.Core.Data;
using WCryptoApi.Core.Data.Category;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Core.Exceptions;
using WCryptoApi.Core.Requests;
using WCryptoApi.Core.Entities;
using WCryptoApi.Core.Services.Category;

namespace WCryptoApi.Business.Services.Category;

public class CategoryUpdateService: ICategoryUpdateService
{
    private readonly ICategoryUpdateDb   _categoryUpdateDb;
    private readonly ICategoryFinderService _categoryFinderService;

    public CategoryUpdateService(ICategoryUpdateDb categoryUpdateDb, ICategoryFinderService categoryFinderService)
    {
        _categoryUpdateDb = categoryUpdateDb;
        _categoryFinderService = categoryFinderService;
    }

    public async Task<Core.Entities.Category> Update(int categoryId, CategoryRequest categoryRequest)
    {
        CategoryDto categoryDto = await _categoryFinderService.FindById(categoryId);
        categoryDto.Description = categoryRequest.Description;
        categoryDto.UserId      = categoryRequest.UserId;
        Core.Entities.Category category = new(categoryDto);
        try
        {
            if (await _categoryUpdateDb.Update(category))
            {
                return new Core.Entities.Category(await _categoryFinderService.FindById(category.CategoryId));
            }
        }
        catch (Exception e)
        {
            throw new HttpBadRequestException("Failure on updating category: ", e);
        }
        throw new HttpBadRequestException("This category does not exists.");
    }
}