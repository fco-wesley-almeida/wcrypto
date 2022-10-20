using WCryptoApi.Core.Dtos;

namespace WCryptoApi.Core.Data.Product;

public interface IProductListingDb
{
    Task<IEnumerable<ProductDto>> FindAllByUserId(int userId);
    Task<IEnumerable<ProductDto>> FindAll();
}