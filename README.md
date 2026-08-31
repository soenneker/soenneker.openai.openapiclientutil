[![](https://img.shields.io/nuget/v/soenneker.openai.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.openai.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.openai.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.openai.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.openai.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.openai.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.openai.openapiclientutil/codeql.yml?style=for-the-badge&label=codeql)](https://github.com/soenneker/soenneker.openai.openapiclientutil/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.OpenAI.OpenApiClientUtil

Provides a configured OpenAI REST API client and reuses it for the lifetime of the registered service.

## Installation

```bash
dotnet add package Soenneker.OpenAI.OpenApiClientUtil
```

## Configuration

```json
{
  "OpenAI": {
    "ApiKey": "your-api-key"
  }
}
```

`OpenAI:ClientBaseUrl` and `OpenAI:AuthHeaderValueTemplate` can override the API base URL and authorization value format when needed.

## Usage

```csharp
using Soenneker.OpenAI.OpenApiClientUtil.Abstract;
using Soenneker.OpenAI.OpenApiClientUtil.Registrars;

services.AddOpenAIOpenApiClientUtilAsSingleton();

IOpenAIOpenApiClientUtil openAi = serviceProvider
    .GetRequiredService<IOpenAIOpenApiClientUtil>();

var client = await openAi.Get(cancellationToken);
var models = await client.Models.GetAsync(cancellationToken: cancellationToken);
```

Use `AddOpenAIOpenApiClientUtilAsScoped()` when each application scope should have its own generated client wrapper. The underlying authenticated HTTP provider remains shared and is disposed by the service container at shutdown.
