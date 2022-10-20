using Microsoft.AspNetCore.Mvc;
using WCryptoApi.Core;
using WCryptoApi.Core.Entities;
using WCryptoApi.Core.Exceptions;
using WCryptoApi.Core.Services.Product;

namespace WCryptoApi.Controllers;

[ApiController]
[Route(template: "[controller]")]

public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductListingService     _productListingService;
    private readonly IProductRegisterService    _productRegisterService;
    private readonly IProductUpdateService      _productUpdateService;
    private readonly IProductFinderService      _productFinderService;
    private readonly IProductDeleteService      _productDeleteService;

    public ProductController(ILogger<ProductController> logger, IProductListingService productListingService, IProductRegisterService productRegisterService, IProductUpdateService productUpdateService, IProductFinderService productFinderService, IProductDeleteService productDeleteService)
    {
        _logger                     = logger;
        _productListingService     = productListingService;
        _productRegisterService    = productRegisterService;
        _productUpdateService      = productUpdateService;
        _productFinderService      = productFinderService;
        _productDeleteService = productDeleteService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> Get()
    {
        try
        {
            return Ok(await _productListingService.FindAll());
        }
        catch (HttpResponseException e)
        {
            return await HandleException(e);
        }
    }
    
    [HttpGet(template: "{productId:int}")]
    public async Task<ActionResult<Product>> GetById(int productId)
    {
        try
        {
            return Ok(await _productFinderService.FindById(productId));
        }
        catch (HttpResponseException e)
        {
            return await HandleException(e);
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<Product>> Register(ProductRequest productRequest)
    {
        try
        {
            return Ok(await _productRegisterService.Register(productRequest));
        }
        catch (HttpResponseException e)
        {
            return await HandleException(e);
        }
    }
    
    [HttpPut("{productId:int}")]
    public async Task<ActionResult<Product>> Update(int productId, ProductRequest productRequest)
    {
        try
        {
            return Ok(await _productUpdateService.Update(productId, productRequest));
        }
        catch (HttpResponseException e)
        {
            return await HandleException(e);
        }
    }
    
    [HttpDelete(template: "{productId:int}")]
    public async Task<ActionResult> Delete(int productId)
    {
        try
        {
            await _productDeleteService.DeleteById(productId);
            return Ok();
        }
        catch (HttpResponseException e)
        {
            return await HandleException(e);
        }
    }
}