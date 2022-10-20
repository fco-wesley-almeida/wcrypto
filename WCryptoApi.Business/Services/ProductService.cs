using WCryptoApi.Core;
using WCryptoApi.Core.Data.Category;
using WCryptoApi.Core.Data.Product;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Core.Entities;
using WCryptoApi.Core.Exceptions;
using WCryptoApi.Core.Services.Product;

namespace WCryptoApi.Business.Services;

public class ProductService: IProductListingService, IProductFinderService, IProductRegisterService, IProductUpdateService, IProductDeleteService
{
    private readonly IProductListingDb  _productListingDb;
    private readonly IProductFinderDb   _productFinderDb;
    private readonly IProductRegisterDb _productRegisterDb;
    private readonly IProductUpdateDb   _productUpdateDb;
    private readonly IProductDeleteDb   _productDeleteDb;
    private readonly ICategoryFinderDb  _categoryFinder;

    public ProductService(IProductListingDb productListingDb, IProductFinderDb productFinderDb, IProductRegisterDb productRegisterDb, IProductUpdateDb productUpdateDb, IProductDeleteDb productDeleteDb, ICategoryFinderDb categoryFinder)
    {
        _productListingDb    = productListingDb;
        _productFinderDb     = productFinderDb;
        _productRegisterDb   = productRegisterDb;
        _productUpdateDb     = productUpdateDb;
        _productDeleteDb     = productDeleteDb;
        _categoryFinder = categoryFinder;
    }

    public async Task<List<ProductDto>> FindAll()
    {
        List<ProductDto> categories = (await _productListingDb.FindAll()).ToList();
        if (categories.Count == 0)
        {
            throw new HttpNotFoundException();
        }
        return categories;
    }

    public async Task<ProductDto> FindById(int productId)
    {
        return await _productFinderDb.FindById(productId) ?? throw new HttpNotFoundException();
    }

    public async Task<Product> Register(ProductRequest productRequest)
    {
        Product product = new (productRequest);
        if (await _categoryFinder.FindById(product.CategoryId) is null)
        {
            throw new HttpBadRequestException("This category does not exists.");
        }
        try
        {
            int productId = await _productRegisterDb.Register(product);
            return new Product(await FindById(productId));
        }
        catch (Exception)
        {
            throw new HttpBadRequestException("The register of product has failed.");
        }
    }

    public async Task<Product> Update(int productId, ProductRequest productRequest)
    {
        ProductDto productDto = await FindById(productId);
        productDto.Description = productRequest.Description;
        productDto.CategoryId  = productRequest.CategoryId;
        Product product = new (productDto);
        if (await _categoryFinder.FindById(product.CategoryId) is null)
        {
            throw new HttpBadRequestException("This category does not exists.");
        }
        if (await _productUpdateDb.Update(product))
        {
            return new Product(await FindById(productId));
        }
        throw new HttpBadRequestException("This product does not exists.");
    }

    public async Task DeleteById(int productId)
    {
        if (!await _productDeleteDb.Delete(productId))
        {
            throw new HttpBadRequestException("This product does not exists.");    
        }
    }
}