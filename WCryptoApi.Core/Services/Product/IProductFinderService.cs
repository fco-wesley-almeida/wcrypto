using WCryptoApi.Core.Dtos;

namespace WCryptoApi.Core.Services.Product;

using Entities;

public interface IProductFinderService
{
    Task<ProductDto> FindById(int productId);
}