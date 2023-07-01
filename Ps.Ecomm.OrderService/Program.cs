using Ps.Ecomm.OrderService.AppStart;

namespace Ps.Ecomm.OrderService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAppServices(builder.Configuration);

            WebApplication app = builder.Build();
            app.AddMiddlewares();
        }
    }
}