using WCryptoApi.Core;
using WCryptoApi.Core.Data;
using WCryptoApi.Core.Data.Product;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Core.Exceptions;
using WCryptoApi.Core.Requests;
using WCryptoApi.Core.Entities;
using WCryptoApi.Core.Services.Product;

namespace WCryptoApi.Business.Services.Product;

public class ProductUpdateService: IProductUpdateService
{
    private readonly IProductUpdateDb   _productUpdateDb;
    private readonly IProductFinderService _productFinderService;

    private int _productId;
    private ProductRequest _productRequest  = null!;
    private Core.Entities.Product _product = null!;
    
    public ProductUpdateService(IProductUpdateDb productUpdateDb, IProductFinderService productFinderService)
    {
        _productUpdateDb = productUpdateDb;
        _productFinderService = productFinderService;
    }

    public async Task<Core.Entities.Product> Update(int productId, ProductRequest productRequest)
    {
        _productId = productId;
        _productRequest = productRequest;
        try
        {
            await RetrieveProduct();
            return await PerformUpdate();
        }
        catch (Exception e)
        {
            HandleException(e);
            throw;
        }
    }

    private async Task RetrieveProduct()
    {
        ProductDto productDto = await _productFinderService.FindById(_productId);
        productDto.Description = _productRequest.Description;
        productDto.UserId      = _productRequest.CategoryId;
        _product = new(productDto);
    }

    private async Task<Core.Entities.Product> PerformUpdate()
    {
        if (await _productUpdateDb.Update(_product))
        {
            return new(await _productFinderService.FindById(_product.ProductId));
        }
        throw new HttpBadRequestException("This product does not exists.");
    }

    private void HandleException(Exception e)
    {
        if (e is ArgumentException or HttpBadRequestException)
        {
            throw e;
        }
        throw new HttpBadRequestException("Failure on updating product: ", e);
    }
}