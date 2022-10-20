namespace WCryptoApi.Core.Exceptions;

public class HttpNotFoundException: HttpResponseException
{
    public HttpNotFoundException()
    {
        
    }
    public override int    StatusCode() => 404;
    public override object? Data() => new
    {
        Message = "Not found."
    };
}