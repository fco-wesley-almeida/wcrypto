namespace WCryptoApi.Core.Services.Product;

using Entities;
public interface IProductUpdateService
{
    Task<Product> Update(int productId, ProductRequest productRequest);
}