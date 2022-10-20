namespace WCryptoApi.Core.Services.Product;

public interface IProductDeleteService
{
    Task DeleteById(int productId);
}