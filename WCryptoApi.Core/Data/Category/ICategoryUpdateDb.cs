namespace WCryptoApi.Core.Data.Category;

public interface ICategoryUpdateDb
{
    Task<bool> Update(Entities.Category category);
}