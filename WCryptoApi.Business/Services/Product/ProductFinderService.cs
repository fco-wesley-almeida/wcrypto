using WCryptoApi.Core.Data.Product;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Core.Exceptions;
using WCryptoApi.Core.Services.Product;

namespace WCryptoApi.Business.Services.Product;

public class ProductFinderService: IProductFinderService
{
    private readonly IProductFinderDb   _productFinderDb;

    public ProductFinderService(IProductFinderDb productFinderDb)
    {
        _productFinderDb = productFinderDb;
    }

    public async Task<ProductDto> FindById(int productId)
    {
        return await _productFinderDb.FindById(productId) ?? throw new HttpNotFoundException();
    }
}