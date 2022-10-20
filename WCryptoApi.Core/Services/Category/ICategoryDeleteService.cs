namespace WCryptoApi.Core.Services.Category;

public interface ICategoryDeleteService
{
    Task DeleteById(int categoryId);
}