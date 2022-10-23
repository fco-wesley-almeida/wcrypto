using WCryptoApi.Business.Services;
using WCryptoApi.Business.Services.Category;
using WCryptoApi.Core.Data.Category;
using WCryptoApi.Core.Data.Product;
using WCryptoApi.Core.Services.Category;
using WCryptoApi.Core.Services.Product;
using WCryptoApi.Infrastructure.Dao.Category;
using WCryptoApi.Infrastructure.Dao.Product;

namespace WCryptoApi.Configuration;

public class DependencyInjection
{
    public static void Configure(IServiceCollection services)
    {
        services.AddScoped<ICategoryListingService, CategoryListingService>();
        services.AddScoped<ICategoryFinderService, CategoryFinderService>();
        services.AddScoped<ICategoryRegisterService, CategoryRegisterService>();
        services.AddScoped<ICategoryUpdateService, CategoryUpdateService>();
        services.AddScoped<ICategoryDeleteService, CategoryDeleteService>();
        services.AddScoped<ICategoryListingDb, CategoryListingDb>();
        services.AddScoped<ICategoryFinderDb, CategoryFinderDb>();
        services.AddScoped<ICategoryRegisterDb, CategoryRegisterDb>();
        services.AddScoped<ICategoryUpdateDb, CategoryUpdateDb>();
        services.AddScoped<ICategoryDeleteDb, CategoryDeleteDb>();
        
        services.AddScoped<IProductListingService, ProductService>();
        services.AddScoped<IProductFinderService, ProductService>();
        services.AddScoped<IProductRegisterService, ProductService>();
        services.AddScoped<IProductUpdateService, ProductService>();
        services.AddScoped<IProductDeleteService, ProductService>();
        services.AddScoped<IProductListingDb, ProductListingDb>();
        services.AddScoped<IProductFinderDb, ProductFinderDb>();
        services.AddScoped<IProductRegisterDb, ProductRegisterDb>();
        services.AddScoped<IProductUpdateDb, ProductUpdateDb>();
        services.AddScoped<IProductDeleteDb, ProductDeleteDb>();
    }
}