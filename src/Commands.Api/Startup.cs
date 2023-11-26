using Business.Configuration;
using Commands.Application;
using Commands.Application.Commands;
using DataAccess.Mongo.Configure;
using EventBus.Kafka.Abstraction;
using Infrastructure.Clients;
using MediatR;
using Messages.Account;
using Messages.Payments;
using Messages.Persons;
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
        private const string AllowedSpecificOrigins = "_AllowedSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(config => {
                config.RegisterServicesFromAssemblyContaining<Program>();
                config.RegisterServicesFromAssemblyContaining<CreatePersonHandler>();

                // Setting the publisher directly will make the instance a Singleton.
                config.NotificationPublisher = new TaskWhenAllPublisher();

                config.NotificationPublisherType = typeof(TaskWhenAllPublisher);

                config.Lifetime = ServiceLifetime.Transient;
            });
            
            services.AddClients();
            services.AddRetryPolicy();

            services.AddScoped<IMongoClient>(s =>
                new MongoClient(MongoSettings.ConnectionString)
            );

            services.AddKafkaProducer<UpdateAccountProjectionMessage>(KafkaSettings.CommandTopics.UpdateAccountTopicName);
            services.AddKafkaProducer<SaveAccountProjectionMessage>(KafkaSettings.CommandTopics.SaveAccountTopicName);
            services.AddKafkaProducer<DeleteAccountProjectionMessage>(KafkaSettings.CommandTopics.DeleteAccountTopicName);

            services.AddKafkaProducer<UpdatePaymentProjectionMessage>(KafkaSettings.CommandTopics.UpdatePaymentTopicName);
            services.AddKafkaProducer<SavePaymentProjectionMessage>(KafkaSettings.CommandTopics.SavePaymentTopicName);
            services.AddKafkaProducer<DeletePaymentProjectionMessage>(KafkaSettings.CommandTopics.DeletePaymentTopicName);

            services.AddKafkaProducer<UpdatePersonProjectionMessage>(KafkaSettings.CommandTopics.UpdatePersonTopicName);
            services.AddKafkaProducer<SavePersonProjectionMessage>(KafkaSettings.CommandTopics.SavePersonTopicName);
            services.AddKafkaProducer<DeletePersonProjectionMessage>(KafkaSettings.CommandTopics.DeletePersonTopicName);

            services.AddMongoDataAccessObjects();
            services.AddDomainServices();

            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowedSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:3009");//Accounting-ui
                                  });
            });

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

            app.UseCors(AllowedSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
