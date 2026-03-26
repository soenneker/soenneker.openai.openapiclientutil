using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.OpenAI.HttpClients.Registrars;
using Soenneker.OpenAI.OpenApiClientUtil.Abstract;

namespace Soenneker.OpenAI.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class OpenAIOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="OpenAIOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddOpenAIOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddOpenAIOpenApiHttpClientAsSingleton()
                .TryAddSingleton<IOpenAIOpenApiClientUtil, OpenAIOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="OpenAIOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddOpenAIOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddOpenAIOpenApiHttpClientAsSingleton()
                .TryAddScoped<IOpenAIOpenApiClientUtil, OpenAIOpenApiClientUtil>();

        return services;
    }
}
