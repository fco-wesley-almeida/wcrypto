namespace WCryptoApi.Core.Data.Product;

public interface IProductDeleteDb
{
    Task<bool> Delete(int categoryId);
}