using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Registry;
using System;
using System.Net;
using System.Net.Http;

namespace Infrastructure.Clients
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddClients(this IServiceCollection services)
        {
            services.AddHttpClient<IExchangeRateService, ExchangeRateService>(client =>
            {
                client.BaseAddress = new Uri("http://exchangerateapi");
            });

            return services;
        }

        public static IServiceCollection AddRetryPolicy(this IServiceCollection services)
        {
            const int retryIntervalInSeconds = 3;
            const int retryMax = 3;

            var retryPolicy = Policy
                .HandleResult<HttpResponseMessage>(r => r.StatusCode != HttpStatusCode.OK)
                .WaitAndRetryAsync(retryMax, retries =>
                {
                    return TimeSpan.FromSeconds(retryIntervalInSeconds);
                });

            var policyRegistry = new PolicyRegistry();
            policyRegistry.Add(Constants.InfiniteRetryPolicy, retryPolicy);

            return services.AddScoped<IReadOnlyPolicyRegistry<string>>(s => policyRegistry);
        }
    }
}
