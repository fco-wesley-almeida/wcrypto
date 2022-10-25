using WCryptoApi.Core.Dtos;

namespace WCryptoApi.Core.Data.Product;

public interface IProductListingDb
{
    Task<IEnumerable<ProductDto>> FindAllByCategoryId(int categoryId);
    Task<IEnumerable<ProductDto>> FindAll();
}