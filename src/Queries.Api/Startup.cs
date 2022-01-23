using DataAccess.Elastic.Configure;
using EventBus.Kafka.Abstraction;
using MediatR;
using Messages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Nest;
using Queries.Api.KafkaHandlers;
using Queries.Application.Persons;
using Queries.Core.models;
using Settings;
using System;
using System.Reflection;
using System.Threading;

namespace Queries.Api
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
            var handlersAssembly = typeof(GetAllPersonHandler).GetTypeInfo().Assembly;
            services.AddMediatR(Assembly.GetExecutingAssembly(), handlersAssembly);

            services.AddSingleton<IElasticClient>(s =>
            {
                var settings =
                    new ConnectionSettings(new Uri(ElasticSettings.Url))
                    .DefaultIndex(ElasticSettings.PersonsIndexName)
                    .DefaultMappingFor<Account>(m => m.IndexName(ElasticSettings.AccountsIndexName))
                    .DefaultMappingFor<Payment>(m => m.IndexName(ElasticSettings.PaymentsIndexName))
                    .DefaultMappingFor<Person>(m => m.IndexName(ElasticSettings.PersonsIndexName));

                return new ElasticClient(settings);
            });

            services.AddKafkaConsumer<UpdateAccountProjectionMessage, UpdateAccountProjectionHandler>(
                KafkaSettings.ProjectionTopics.AccountTopicName,
                KafkaSettings.Groups.BusinessGroupId);

            services.AddKafkaConsumer<UpdatePaymentProjectionMessage, UpdatePaymentProjectionHandler>(
                KafkaSettings.ProjectionTopics.PaymentTopicName,
                KafkaSettings.Groups.BusinessGroupId);

            services.AddKafkaConsumer<UpdatePersonProjectionMessage, UpdatePersonProjectionHandler>(
                KafkaSettings.ProjectionTopics.PersonTopicName,
                KafkaSettings.Groups.BusinessGroupId);

            services.AddElasticDataAccessObjects();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Queries.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IKafkaConsumer<UpdateAccountProjectionMessage> kafkaAccountConsumer,
            IKafkaConsumer<UpdatePaymentProjectionMessage> kafkaPaymentConsumer,
            IKafkaConsumer<UpdatePersonProjectionMessage> kafkaPersonConsumer,
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Queries.Api v1"));
            }

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken stoppingToken = cts.Token;

            kafkaAccountConsumer.Consume(stoppingToken);
            kafkaPaymentConsumer.Consume(stoppingToken);
            kafkaPersonConsumer.Consume(stoppingToken);

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
