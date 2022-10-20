namespace WCryptoApi.Core.Data.Category;

public interface ICategoryDeleteDb
{
    Task<bool> Delete(int categoryId);
}