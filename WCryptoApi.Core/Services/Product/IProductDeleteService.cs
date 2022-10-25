using WCryptoApi.Core.Dtos;

namespace WCryptoApi.Core.Services.Product;

public interface IProductDeleteService
{
    Task<ProductDto> DeleteById(int productId);
}