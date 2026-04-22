using Soenneker.OpenAI.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.OpenAI.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class OpenAIOpenApiClientUtilTests : HostedUnitTest
{
    private readonly IOpenAIOpenApiClientUtil _openapiclientutil;

    public OpenAIOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<IOpenAIOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
