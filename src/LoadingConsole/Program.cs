using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LoadingConsole
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static string url = "http://localhost:54320";

        static async Task Main(string[] args)
        {
            var personId = await AddPerson();
            var accountId = await AddAccount(personId);
            var paymentId = await AddPayment(accountId);
            Console.WriteLine("Hello World!");
        }

        private static async Task<string> AddPerson()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var content = new StringContent("{\"name\":\"Person1\", \"inn\":\"3123114578\"}", Encoding.UTF8, "application/json");

            var msg = await client.PostAsync($"{url}/api/Person/", content);

            return await msg.Content.ReadAsStringAsync();
        }

        private static async Task<string> AddAccount(string personId)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var content = new StringContent($"{{\"name\":\"Account1\", \"personid\":{personId}}}", Encoding.UTF8, "application/json");

            var msg = await client.PostAsync($"{url}/api/Account/", content);

            return await msg.Content.ReadAsStringAsync();
        }
        private static async Task<string> AddPayment(string accountId)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
                       
            var content = new StringContent($"{{\"accountid\":{accountId}, \"sum\":\"10\",\"paymenttype\":1}}", Encoding.UTF8, "application/json");

            var msg = await client.PostAsync($"{url}/api/Payment/", content);

            return await msg.Content.ReadAsStringAsync();
        }
    }
}
