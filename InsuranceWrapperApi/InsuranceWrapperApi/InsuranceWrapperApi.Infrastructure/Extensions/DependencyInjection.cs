using InsuranceWrapperApi.Application.Interfaces;
using InsuranceWrapperApi.Application.Services;
using InsuranceWrapperApi.Domain.Factories;
using InsuranceWrapperApi.Domain.Mappers.LOB;
using InsuranceWrapperApi.Domain.Mappers.Providers;
using InsuranceWrapperApi.Infrastructure.ApiClients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System.Net.Http.Headers;

namespace InsuranceWrapperApi.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Register Application Services
        services.AddScoped<ILobDetectorService, LobDetectorService>();
        services.AddScoped<IProviderSelectionService, ProviderSelectionService>();
        services.AddScoped<IQuoteService, QuoteService>();
        services.AddScoped<IBindService, BindService>();

        // Register LOB Mappers
        services.AddScoped<IGLMapper, GLMapper>();
        services.AddScoped<IPropertyMapper, PropertyMapper>();
        services.AddScoped<IFloodMapper, FloodMapper>();
        services.AddScoped<IWorkerCompMapper, WorkerCompMapper>();

        // Register Provider Mappers
        services.AddScoped<IDyadQuoteMapper, DyadQuoteMapper>();
        services.AddScoped<IDyadBindMapper, DyadBindMapper>();
        services.AddScoped<IHeraldQuoteMapper, HeraldQuoteMapper>();
        services.AddScoped<IHeraldBindMapper, HeraldBindMapper>();
        services.AddScoped<IZywaveQuoteMapper, ZywaveQuoteMapper>();
        services.AddScoped<IZywaveBindMapper, ZywaveBindMapper>();

        // Register Factories
        services.AddScoped<LobMapperFactory>();
        services.AddScoped<ProviderMapperFactory>();

        // Configure Refit API Clients with Polly for resilience
        services.AddRefitClients(configuration);

        return services;
    }

    private static void AddRefitClients(this IServiceCollection services, IConfiguration configuration)
    {
        // Dyad API Client
        services.AddRefitClient<IDyadApiClient>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(configuration["Providers:Dyad:BaseUrl"] ?? "https://api.dyad.com");
                c.DefaultRequestHeaders.Add("X-API-Key", configuration["Providers:Dyad:ApiKey"]);
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                c.Timeout = TimeSpan.FromSeconds(30);
            })
            .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, retryAttempt => 
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))))
            .AddTransientHttpErrorPolicy(policy => policy.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        // Herald API Client
        services.AddRefitClient<IHeraldApiClient>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(configuration["Providers:Herald:BaseUrl"] ?? "https://api.herald.com");
                c.DefaultRequestHeaders.Add("Authorization", $"Bearer {configuration["Providers:Herald:ApiToken"]}");
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                c.Timeout = TimeSpan.FromSeconds(30);
            })
            .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, retryAttempt => 
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))))
            .AddTransientHttpErrorPolicy(policy => policy.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        // Zywave API Client
        services.AddRefitClient<IZywaveApiClient>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(configuration["Providers:Zywave:BaseUrl"] ?? "https://api.zywave.com");
                c.DefaultRequestHeaders.Add("X-Api-Key", configuration["Providers:Zywave:ApiKey"]);
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                c.Timeout = TimeSpan.FromSeconds(30);
            })
            .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, retryAttempt => 
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))))
            .AddTransientHttpErrorPolicy(policy => policy.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
    }
}
