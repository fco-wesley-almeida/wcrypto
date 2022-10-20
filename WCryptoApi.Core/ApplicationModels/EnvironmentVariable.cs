using WCryptoApi.Core.Exceptions;

namespace WCryptoApi.Core.ApplicationModels;

public class EnvironmentVariable
{
    private readonly string _value;

    public EnvironmentVariable(string variable)
    {
        if (variable == "")
        {
            throw new ArgumentException(message: "The variable reference must to be not empty.");
        }
        string? variableValue = Environment.GetEnvironmentVariable(variable);
        switch (variableValue)
        {
            case null:
                throw new EnvironmentVariableNotDefinedException($@"The variable {variable} is not defined.");
            case "":
                throw new EnvironmentVariableNotDefinedException($@"The variable {variable} is defined, but empty.");
        }
        _value = variableValue;
    }
    public string Value => _value;
}