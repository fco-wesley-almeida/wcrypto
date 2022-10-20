using WCryptoApi.Core.Dtos;

namespace WCryptoApi.Core.Data.Product;

public interface IProductFinderDb
{
    Task<ProductDto?> FindById(int productId);
}