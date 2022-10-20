using WCryptoApi.Core.Requests;

namespace WCryptoApi.Core.Services.Category;

using Entities;
public interface ICategoryRegisterService
{
    Task<Category> Register(CategoryRequest category);
}