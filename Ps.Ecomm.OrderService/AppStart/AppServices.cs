using MassTransit;
using Ps.Ecomm.Models;
using Ps.Ecomm.OrderService.DataAccess;
//using RabbitMQ.Client;

namespace Ps.Ecomm.OrderService.AppStart
{
    public static class AppServices
    {
        public static void AddAppServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Ps Ecomm Order Service",
                    Version = "v1"
                });
            });

            var dbConnStr = config.GetConnectionString("AppDb");
            var rabbitMqConnStr = config.GetConnectionString("RabbitMqConnStr");
            services.AddSingleton<IOrderDetailsProvider>(new OrderDetailsProvider(dbConnStr));
            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(rabbitMqConnStr);
                });
            });

            //services.AddSingleton<IConnectionProvider>(new ConnectionProvider(rabbitMqConnStr));
            //services.AddScoped<IPublisher>(x => new Publisher(x.GetService<IConnectionProvider>(), 
            //                                                    MQConstants.EXCHANGE_REPORT,
            //                                                    ExchangeType.Topic));


        }
    }
}
