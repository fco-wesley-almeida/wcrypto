using WCryptoApi.Core.Data.Category;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Core.Entities;
using WCryptoApi.Core.Exceptions;
using WCryptoApi.Core.Requests;
using WCryptoApi.Core.Services.Category;

namespace WCryptoApi.Business.Services;

public class CategoryService: ICategoryListingService, ICategoryFinderService, ICategoryRegisterService, ICategoryUpdateService, ICategoryDeleteService
{
    private readonly ICategoryListingDb  _categoryListingDb;
    private readonly ICategoryFinderDb   _categoryFinderDb;
    private readonly ICategoryRegisterDb _categoryRegisterDb;
    private readonly ICategoryUpdateDb   _categoryUpdateDb;
    private readonly ICategoryDeleteDb   _categoryDeleteDb;

    public CategoryService(ICategoryListingDb categoryListingDb, ICategoryFinderDb categoryFinderDb, ICategoryRegisterDb categoryRegisterDb, ICategoryUpdateDb categoryUpdateDb, ICategoryDeleteDb categoryDeleteDb)
    {
        _categoryListingDb     = categoryListingDb;
        _categoryFinderDb      = categoryFinderDb;
        _categoryRegisterDb    = categoryRegisterDb;
        _categoryUpdateDb      = categoryUpdateDb;
        _categoryDeleteDb = categoryDeleteDb;
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

    public async Task<CategoryDto> FindById(int categoryId)
    {
        return await _categoryFinderDb.FindById(categoryId) ?? throw new HttpNotFoundException();
    }

    public async Task<Category> Register(CategoryRequest categoryRequest)
    {
        Category category = new(categoryRequest);
        try
        {
            int categoryId = await _categoryRegisterDb.Register(category);
            return new Category(await FindById(categoryId));
        }
        catch (Exception)
        {
            throw new HttpBadRequestException("The register of category has failed.");
        }
    }

    public async Task<Category> Update(int categoryId, CategoryRequest categoryRequest)
    {
        CategoryDto categoryDto = await FindById(categoryId);
        categoryDto.Description = categoryRequest.Description;
        categoryDto.UserId      = categoryRequest.UserId;
        Category category = new(categoryDto);
        if (await _categoryUpdateDb.Update(category))
        {
            return new Category(await FindById(category.CategoryId));
        }
        throw new HttpBadRequestException("This category does not exists.");
    }

    public async Task         DeleteById(int categoryId)
    {
        if (!await _categoryDeleteDb.Delete(categoryId))
        {
            throw new HttpBadRequestException("This category does not exists.");    
        }
    }
}