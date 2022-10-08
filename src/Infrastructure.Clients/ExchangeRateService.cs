using Polly;
using Polly.Registry;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Clients
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly HttpClient _client;
        private readonly IAsyncPolicy<HttpResponseMessage> _retryPolicy;

        public ExchangeRateService(
            HttpClient client,
            IReadOnlyPolicyRegistry<string> policyRegistry)
        {
            _client = client;

            _retryPolicy = policyRegistry.Get<IAsyncPolicy<HttpResponseMessage>>(Constants.InfiniteRetryPolicy);
        }

        public async Task<int> GetAccessRateAsync()
        {
            var response = await _retryPolicy.ExecuteAsync(() => _client.GetAsync("api/ExchangeRate"));

            var converted = await response.Content.ReadAsStringAsync();
            var rate = JsonSerializer.Deserialize<int>(converted);

            return rate;
        }
    }
}
