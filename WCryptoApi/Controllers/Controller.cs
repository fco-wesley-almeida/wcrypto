using Microsoft.AspNetCore.Mvc;
using WCryptoApi.Core.Exceptions;

namespace WCryptoApi.Controllers;

public class Controller: ControllerBase
{
    protected async Task<ObjectResult> HandleException(Exception exception)
    {
        if (exception is HttpResponseException httpResponseException)
        {
            return StatusCode(
                httpResponseException.StatusCode(), 
                httpResponseException.Data()
            );
        }
        return StatusCode(
            statusCode: 500, 
            value: "Internal Server Error"
        );
    }
}