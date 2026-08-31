using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.ValueTask;
using Soenneker.OpenAI.HttpClients.Abstract;
using Soenneker.OpenAI.OpenApiClient;
using Soenneker.OpenAI.OpenApiClientUtil.Abstract;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.OpenAI.OpenApiClientUtil;

public sealed class OpenAIOpenApiClientUtil : IOpenAIOpenApiClientUtil
{
    private readonly AsyncSingleton<OpenAIOpenApiClient> _client;

    public OpenAIOpenApiClientUtil(IOpenAIOpenApiHttpClient httpClientUtil)
    {
        _client = new AsyncSingleton<OpenAIOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider(), httpClient: httpClient);

            return new OpenAIOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<OpenAIOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
