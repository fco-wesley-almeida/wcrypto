using WCryptoApi.Core.Dtos;

namespace WCryptoApi.Core.Services.Product;

using Entities;
public interface IProductListingService
{
    Task<List<ProductDto>> FindAll();
}