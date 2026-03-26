using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.OpenAI.HttpClients.Abstract;
using Soenneker.OpenAI.OpenApiClientUtil.Abstract;
using Soenneker.OpenAI.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.OpenAI.OpenApiClientUtil;

///<inheritdoc cref="IOpenAIOpenApiClientUtil"/>
public sealed class OpenAIOpenApiClientUtil : IOpenAIOpenApiClientUtil
{
    private readonly AsyncSingleton<OpenAIOpenApiClient> _client;

    public OpenAIOpenApiClientUtil(IOpenAIOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<OpenAIOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("OpenAI:ApiKey");
            string authHeaderValueTemplate = configuration["OpenAI:AuthHeaderValueTemplate"] ?? "{token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerValue: authHeaderValue), httpClient: httpClient);

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
