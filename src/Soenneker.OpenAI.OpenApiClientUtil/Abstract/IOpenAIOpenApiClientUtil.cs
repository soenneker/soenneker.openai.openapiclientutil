using Soenneker.OpenAI.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.OpenAI.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface IOpenAIOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    ValueTask<OpenAIOpenApiClient> Get(CancellationToken cancellationToken = default);
}
