using WCryptoApi.Core.Data.Category;
using WCryptoApi.Core.Exceptions;
using WCryptoApi.Core.Requests;
using WCryptoApi.Core.Services.Category;
using Entities = WCryptoApi.Core.Entities;
namespace WCryptoApi.Business.Services.Category;

public class CategoryRegisterService: ICategoryRegisterService
{
    private readonly ICategoryRegisterDb _categoryRegisterDb;
    private readonly ICategoryFinderService _categoryFinderService;

    public CategoryRegisterService(ICategoryRegisterDb categoryRegisterDb, ICategoryFinderService categoryFinderService)
    {
        _categoryRegisterDb = categoryRegisterDb;
        _categoryFinderService = categoryFinderService;
    }

    public async Task<Entities.Category> Register(CategoryRequest categoryRequest)
    {
        Entities.Category category = new(categoryRequest);
        try
        {
            int categoryId = await _categoryRegisterDb.Register(category);
            return new Entities.Category(await _categoryFinderService.FindById(categoryId));
        }
        catch (Exception e)
        {
            throw new HttpBadRequestException("The register of category has failed: ", e);
        }
    }
}