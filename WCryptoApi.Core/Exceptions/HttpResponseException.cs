namespace WCryptoApi.Core.Exceptions;

public abstract class HttpResponseException: Exception
{
    protected HttpResponseException()
    {
    }

    protected HttpResponseException(string? message) : base(message)
    {
    }

    public abstract int StatusCode();
    public new abstract object? Data();
}