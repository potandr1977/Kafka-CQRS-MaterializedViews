using DataAccess.Elastic.Configure;
using EventBus.Kafka;
using EventBus.Kafka.Abstraction.Enums;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Nest;
using Queries.Application.Persons;
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
                    new ConnectionSettings(new Uri(ElasticSettings.Url)).DefaultIndex(ElasticSettings.DefaultIndexName);

                return new ElasticClient(settings);
            });

            services.AddSingleton<IKafkaAccountConsumer, KafkaAccountConsumer>();
            services.AddSingleton<IKafkaPaymentConsumer, KafkaPaymentConsumer>();
            services.AddSingleton<IKafkaPersonConsumer, KafkaPersonConsumer>();

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
            IWebHostEnvironment env,
            IKafkaAccountConsumer kafkaAccountConsumer,
            IKafkaPaymentConsumer kafkaPaymentConsumer,
            IKafkaPersonConsumer kafkaPersonConsumer
            )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Queries.Api v1"));
            }

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken stoppingToken = cts.Token;
            
            //accounts consumers
            var accountTask = kafkaAccountConsumer.Consume(
                (key, value) => {
                    var res = key;
                },
                (int) PartitionEnum.Account, null, stoppingToken);
            //payments consumers
            var paymentTask = kafkaPaymentConsumer.Consume(
                (key, value) => {
                    var res = value;
                },
                (int) PartitionEnum.Payment, null, stoppingToken);

            /*
            //persons consumers
            var personTask = kafkaPersonConsumer.Consume(
                (key, value) => {
                    var res = value;
                },
                (int) PartitionEnum.Person, null, stoppingToken);
            */
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
