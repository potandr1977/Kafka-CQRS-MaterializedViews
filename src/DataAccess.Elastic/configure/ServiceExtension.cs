using Microsoft.Extensions.DependencyInjection;
using Queries.Core.dataaccess;

namespace DataAccess.Elastic.Configure
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddElasticDataAccessObjects(this IServiceCollection services)
        {
            services.AddScoped<IAccountSimpleViewDao, AccountSimpleViewDao>();
            services.AddScoped<IPaymentSimpleViewDao, PaymentSimpleViewDao>();
            services.AddScoped<IPersonSimpleViewDao, PersonSimpleViewDao>();

            return services;
        }
    }
}
