using Application.Abstractions;
using Infrastructure.Caching;
using Infrastructure.Llm;
using Infrastructure.Persistance;
using Infrastructure.Search;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration cfg)
    {
        services.AddSingleton(new CosmosClient(cfg["Cosmos:ConnectionString"]));
        services.AddSingleton<IChatRepository, ChatRepository>();
        services.AddSingleton<IAiSearchClient, AzureAiSearchClient>();
        services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(cfg["Redis:Connection"]!));
        services.AddSingleton<ICacheProvider, RedisCacheProvider>();
        services.AddSingleton<ILlmClient, AzureOpenAiClient>();
        return services;
    }
}