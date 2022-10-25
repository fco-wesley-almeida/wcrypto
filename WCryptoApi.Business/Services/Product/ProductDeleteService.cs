using WCryptoApi.Core.Data.Product;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Core.Exceptions;
using WCryptoApi.Core.Services.Product;

namespace WCryptoApi.Business.Services.Product;

public class ProductDeleteService: IProductDeleteService
{
    private readonly IProductDeleteDb   _productDeleteDb;
    private readonly IProductFinderService _productFinderService;

    public ProductDeleteService(IProductDeleteDb productDeleteDb, IProductFinderService productFinderService)
    {
        _productDeleteDb = productDeleteDb;
        _productFinderService = productFinderService;
    }

    public async Task<ProductDto> DeleteById(int productId)
    {
        ProductDto product = await _productFinderService.FindById(productId);
        if (!await _productDeleteDb.Delete(productId))
        {
            throw new HttpBadRequestException("This product could not be deleted.");    
        }
        return product;
    }
}