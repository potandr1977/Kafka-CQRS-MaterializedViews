using DataAccess.DataAccess;
using DataAccess.Mongo;
using Domain.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Configure
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDataAccessObjects(this IServiceCollection services)
        {
            services.AddSingleton<IAccountDao, AccountDao>();
            services.AddSingleton<IPaymentDao, PaymentDao>();
            services.AddSingleton<IPersonDao, PersonDao>();

            return services;
        }
    }
}
