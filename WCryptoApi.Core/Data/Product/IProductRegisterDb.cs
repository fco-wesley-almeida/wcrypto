namespace WCryptoApi.Core.Data.Product;

public interface IProductRegisterDb
{
    Task<int> Register(Entities.Product product);
}