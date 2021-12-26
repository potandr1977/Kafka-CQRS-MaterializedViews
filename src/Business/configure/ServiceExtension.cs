using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Configuration
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IPaymentService, PaymentService>();
            services.AddSingleton<IPersonService, PersonService>();

            return services;
        }
    }
}
