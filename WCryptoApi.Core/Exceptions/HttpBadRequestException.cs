namespace WCryptoApi.Core.Exceptions;

public class HttpBadRequestException: HttpResponseException
{
    private object? _data;
    public HttpBadRequestException(string message, object? data = null) : base(message)
    {
        if (string.IsNullOrEmpty(message))
        {
            throw new ArgumentException("The message must be not null and not empty.");
        }
        _data = data;
    }

    public override int    StatusCode() => 400;
    public override object? Data() => new
    {
        Message,
        Error = _data
    };
}