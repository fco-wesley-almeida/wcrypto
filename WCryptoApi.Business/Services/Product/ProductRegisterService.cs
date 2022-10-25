using WCryptoApi.Core;
using WCryptoApi.Core.Data.Category;
using WCryptoApi.Core.Data.Product;
using WCryptoApi.Core.Exceptions;
using WCryptoApi.Core.Requests;
using WCryptoApi.Core.Services.Product;
using Entities = WCryptoApi.Core.Entities;
namespace WCryptoApi.Business.Services.Product;

public class ProductRegisterService: IProductRegisterService
{
    private readonly IProductRegisterDb _productRegisterDb;
    private readonly IProductFinderService _productFinderService;
    private readonly ICategoryFinderDb _categoryFinderDb;

    private Entities.Product _product = null!;
    
    public ProductRegisterService(IProductRegisterDb productRegisterDb, IProductFinderService productFinderService, ICategoryFinderDb categoryFinderDb)
    {
        _productRegisterDb = productRegisterDb;
        _productFinderService = productFinderService;
        _categoryFinderDb = categoryFinderDb;
    }

    public async Task<Entities.Product> Register(ProductRequest productRequest)
    {
        _product = new(productRequest);
        try
        {
            await ValidateCategory();
            return await PerformInsert();
        }
        catch (Exception e)
        {
            throw new HttpBadRequestException("The register of product has failed: ", e);
        }
    }
    private async Task ValidateCategory()
    {
        if (await _categoryFinderDb.FindById(_product.CategoryId) is null)
        {
            throw new HttpBadRequestException("This category does not exists.");
        }
    }
    private async Task<Entities.Product> PerformInsert()
    {
        int productId = await _productRegisterDb.Register(_product);
        return new Entities.Product(await _productFinderService.FindById(productId));
    }
}