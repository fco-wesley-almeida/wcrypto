using WCryptoApi.Core.Data.Product;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Core.Exceptions;
using WCryptoApi.Core.Services.Product;

namespace WCryptoApi.Business.Services.Product;

public class ProductListingService: IProductListingService
{
    private readonly IProductListingDb  _productListingDb;

    public ProductListingService(IProductListingDb productListingDb)
    {
        _productListingDb = productListingDb;
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
}