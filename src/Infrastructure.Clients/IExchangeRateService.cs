using System.Threading.Tasks;

namespace Infrastructure.Clients
{
    public interface IExchangeRateService
    {
        public Task<int> GetAccessRateAsync();
    }
}
