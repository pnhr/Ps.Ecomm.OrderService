using MassTransit;
using Ps.Ecomm.Models;
using Ps.Ecomm.OrderService.DataAccess;
using Ps.Ecomm.OrderService.Listeners;
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
            services.AddSingleton<IOrderCreator>(x => new OrderCreator(dbConnStr, x.GetService<ILogger<OrderCreator>>()));
            services.AddSingleton<IOrderDeleter>(new OrderDeleter(dbConnStr));

            services.AddMassTransit(config =>
            {
                config.AddConsumer<InventoryResponseConsumer>();

                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(rabbitMqConnStr);
                    cfg.ReceiveEndpoint("inventory-queue", c =>
                    {
                        c.ConfigureConsumer<InventoryResponseConsumer>(ctx);
                    });
                });
            });
        }
    }
}
