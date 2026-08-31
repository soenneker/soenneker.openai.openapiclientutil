using Soenneker.OpenAI.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.OpenAI.OpenApiClientUtil.Abstract;

/// <summary>
/// Provides a cached OpenAI REST API client backed by the configured HTTP provider.
/// </summary>
public interface IOpenAIOpenApiClientUtil : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the cached OpenAI client, creating it on the first call.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The configured OpenAI client.</returns>
    ValueTask<OpenAIOpenApiClient> Get(CancellationToken cancellationToken = default);
}
