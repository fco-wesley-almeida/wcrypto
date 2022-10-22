using System;
using WCryptoApi.Core.ApplicationModels;
using WCryptoApi.Core.Exceptions;
using Xunit;

namespace WCryptoApi.Testing.Objects.ApplicationModels;

public class EnvironmentVariableTest
{
    [Fact]
    public void Build_With_Invalid_Params_Should_Fail_Test()
    {
        Assert.Throws<ArgumentException>(() => new EnvironmentVariable(""));
    }

    [Fact]
    public void Build_With_Existing_Variable_Should_Pass_Test()
    {
        const string variable = "TEST_1_REF";
        const string value    = "TEST_1_VALUE";
        Environment.SetEnvironmentVariable(variable, value);
        EnvironmentVariable envVar = new(variable);
        Assert.Equal(value, envVar.Value);
    }
    
    [Fact]
    public void Build_With_NonExisting_Variable_Should_Fail_Test()
    {
        Assert.Throws<EnvironmentVariableNotDefinedException>(() => new EnvironmentVariable("NON_EXISTING_VARIABLE"));
    }
    
    [Fact]
    public void Build_With_Empty_Variable_Should_Fail_Test()
    {
        const string variable = "TEST_1_REF";
        const string value    = "";
        Environment.SetEnvironmentVariable(variable, value);
        Assert.Throws<EnvironmentVariableNotDefinedException>(() => new EnvironmentVariable(variable));
    }
}