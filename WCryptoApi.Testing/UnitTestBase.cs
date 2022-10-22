using Xunit.Abstractions;

namespace WCryptoApi.Testing;

public abstract class UnitTestBase<T>
{
    protected readonly ITestOutputHelper TestOutputHelper;
    protected T                 TestTarget;

    public UnitTestBase(ITestOutputHelper testOutputHelper)
    {
        TestOutputHelper = testOutputHelper;
    }
}