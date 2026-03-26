using Soenneker.OpenAI.OpenApiClientUtil.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.OpenAI.OpenApiClientUtil.Tests;

[Collection("Collection")]
public sealed class OpenAIOpenApiClientUtilTests : FixturedUnitTest
{
    private readonly IOpenAIOpenApiClientUtil _openapiclientutil;

    public OpenAIOpenApiClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _openapiclientutil = Resolve<IOpenAIOpenApiClientUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
