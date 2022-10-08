using DataAccess.DataAccess;
using DataAccess.Mongo;
using Domain.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Mongo.Configure
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddMongoDataAccessObjects(this IServiceCollection services)
        {
            services.AddScoped<IAccountDao, AccountDao>();
            services.AddScoped<IPaymentDao, PaymentDao>();
            services.AddScoped<IPersonDao, PersonDao>();

            return services;
        }
    }
}
