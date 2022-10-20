namespace WCryptoApi.Core.Exceptions;

public class EnvironmentVariableNotDefinedException: Exception
{
    public EnvironmentVariableNotDefinedException(string? message) : base(message)
    {
    }
}