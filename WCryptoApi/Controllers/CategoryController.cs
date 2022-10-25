using Microsoft.AspNetCore.Mvc;
using WCryptoApi.Core.Dtos;
using WCryptoApi.Core.Entities;
using WCryptoApi.Core.Exceptions;
using WCryptoApi.Core.Requests;
using WCryptoApi.Core.Services.Category;

namespace WCryptoApi.Controllers;

[ApiController]
[Route(template: "[controller]")]

public class CategoryController : Controller
{
    private readonly ILogger<CategoryController> _logger;
    private readonly ICategoryListingService     _categoryListingService;
    private readonly ICategoryRegisterService    _categoryRegisterService;
    private readonly ICategoryUpdateService      _categoryUpdateService;
    private readonly ICategoryFinderService      _categoryFinderService;
    private readonly ICategoryDeleteService      _categoryDeleteService;

    public CategoryController(ILogger<CategoryController> logger, ICategoryListingService categoryListingService, ICategoryRegisterService categoryRegisterService, ICategoryUpdateService categoryUpdateService, ICategoryFinderService categoryFinderService, ICategoryDeleteService categoryDeleteService)
    {
        _logger                     = logger;
        _categoryListingService     = categoryListingService;
        _categoryRegisterService    = categoryRegisterService;
        _categoryUpdateService      = categoryUpdateService;
        _categoryFinderService      = categoryFinderService;
        _categoryDeleteService = categoryDeleteService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> Get()
    {
        try
        {
            return Ok(await _categoryListingService.FindAll());
        }
        catch (HttpResponseException e)
        {
            return await HandleException(e);
        }
    }
    
    [HttpGet(template: "{categoryId:int}")]
    public async Task<ActionResult<CategoryDto>> GetById(int categoryId)
    {
        try
        {
            return Ok(await _categoryFinderService.FindById(categoryId));
        }
        catch (HttpResponseException e)
        {
            return await HandleException(e);
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<Category>> Register(CategoryRequest category)
    {
        try
        {
            return Ok(await _categoryRegisterService.Register(category));
        }
        catch (HttpResponseException e)
        {
            return await HandleException(e);
        }
    }
    
    [HttpPut(template: "{categoryId:int}")]
    public async Task<ActionResult<Category>> Update(int categoryId, CategoryRequest category)
    {
        try
        {
            return Ok(await _categoryUpdateService.Update(categoryId, category));
        }
        catch (HttpResponseException e)
        {
            return await HandleException(e);
        }
    }
    
    [HttpDelete(template: "{categoryId:int}")]
    public async Task<ActionResult<CategoryDto>> Delete(int categoryId)
    {
        try
        {
            return Ok(await _categoryDeleteService.DeleteById(categoryId));
        }
        catch (HttpResponseException e)
        {
            return await HandleException(e);
        }
    }
}