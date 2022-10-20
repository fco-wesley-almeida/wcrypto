namespace WCryptoApi.Core.Data.Category;

public interface ICategoryRegisterDb
{
    Task<int> Register(Entities.Category category);
}