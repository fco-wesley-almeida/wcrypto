namespace WCryptoApi.Core.Services.Product;

using Entities;
public interface IProductRegisterService
{
    Task<Product> Register(ProductRequest product);
}