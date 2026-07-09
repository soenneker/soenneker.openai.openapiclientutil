using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.OpenAI.HttpClients.Abstract;
using Soenneker.OpenAI.OpenApiClient;
using Soenneker.OpenAI.OpenApiClient.Models;
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

    [Test]
    public async ValueTask Get_ShouldUseBearerAuthorizationByDefault()
    {
        using var handler = new CapturingHandler();
        using var httpClient = new HttpClient(handler);
        await using var httpClientUtil = new TestOpenAIOpenApiHttpClient(httpClient);

        IConfiguration configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["OpenAI:ApiKey"] = "test-key"
        }).Build();

        await using var util = new OpenAIOpenApiClientUtil(httpClientUtil, configuration);
        OpenAIOpenApiClient client = await util.Get(CancellationToken.None);

        await client.Moderations.PostAsync(new CreateModerationRequest
        {
            Input = new CreateModerationRequestInput
            {
                CreateModerationRequestInputString = "test"
            }
        }, cancellationToken: CancellationToken.None);

        await Assert.That(handler.AuthorizationHeader).IsEqualTo("Bearer test-key");
    }

    private sealed class TestOpenAIOpenApiHttpClient : IOpenAIOpenApiHttpClient
    {
        private readonly HttpClient _httpClient;

        public TestOpenAIOpenApiHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
        {
            return new ValueTask<HttpClient>(_httpClient);
        }

        public void Dispose()
        {
        }

        public ValueTask DisposeAsync()
        {
            return default;
        }
    }

    private sealed class CapturingHandler : HttpMessageHandler
    {
        public string? AuthorizationHeader { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request.Headers.TryGetValues("Authorization", out IEnumerable<string>? values))
                AuthorizationHeader = values.SingleOrDefault();

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("""{"id":"modr_test","model":"omni-moderation-latest","results":[]}""",
                    Encoding.UTF8, "application/json")
            });
        }
    }
}