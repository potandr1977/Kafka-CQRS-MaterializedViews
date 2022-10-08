using Business.Configuration;
using Commands.Application.Commands;
using DataAccess.Mongo.Configure;
using EventBus.Kafka.Abstraction;
using Infrastructure.Clients;
using MediatR;
using Messages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Settings;
using System.Reflection;

namespace Commands.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var handlersAssembly = typeof(CreatePersonHandler).GetTypeInfo().Assembly;
            services.AddMediatR(Assembly.GetExecutingAssembly(), handlersAssembly);
            /*
            services.AddMediatR(typeof(Startup));
            services.Scan(scan =>
            {
                scan
                    .FromAssemblies(Assembly.Load($"{nameof(Application)}"))
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IPipelineBehavior<,>)))
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime();

            });
            */

            services.AddClients();
            services.AddRetryPolicy();

            services.AddScoped<IMongoClient>(s =>
                new MongoClient(MongoSettings.ConnectionString)
            );

            services.AddKafkaProducer<UpdateAccountProjectionMessage>(KafkaSettings.CommandTopics.AccountTopicName);
            services.AddKafkaProducer<UpdatePaymentProjectionMessage>(KafkaSettings.CommandTopics.PaymentTopicName);
            services.AddKafkaProducer<UpdatePersonProjectionMessage>(KafkaSettings.CommandTopics.PersonTopicName);

            services.AddMongoDataAccessObjects();
            services.AddDomainServices();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Commands.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Commands.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
