namespace WCryptoApi.Core.Data.Product;

public interface IProductUpdateDb
{
    Task<bool> Update(Entities.Product product);
}