using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Configuration
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPersonService, PersonService>();

            return services;
        }
    }
}
