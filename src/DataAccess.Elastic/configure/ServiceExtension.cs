using Microsoft.Extensions.DependencyInjection;
using Queries.Core.dataaccess;

namespace DataAccess.Elastic.Configure
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddElasticDataAccessObjects(this IServiceCollection services)
        {
            services.AddSingleton<IAccountSimpleViewDao, AccountSimpleViewDao>();
            services.AddSingleton<IPaymentSimpleViewDao, PaymentSimpleViewDao>();
            services.AddSingleton<IPersonSimpleViewDao, PersonSimpleViewDao>();

            return services;
        }
    }
}
