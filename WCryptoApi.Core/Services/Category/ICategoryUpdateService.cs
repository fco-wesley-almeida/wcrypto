using WCryptoApi.Core.Requests;

namespace WCryptoApi.Core.Services.Category;

using Entities;
public interface ICategoryUpdateService
{
    Task<Category> Update(int categoryId,CategoryRequest category);
}