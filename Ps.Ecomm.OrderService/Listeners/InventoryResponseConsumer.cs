using MassTransit;
using Ps.Ecomm.Models;
using Ps.Ecomm.OrderService.DataAccess;

namespace Ps.Ecomm.OrderService.Listeners
{
    public class InventoryResponseConsumer : IConsumer<InventoryResponse>
    {
        private readonly IOrderDeleter orderDeleter;

        public InventoryResponseConsumer(IOrderDeleter orderDeleter)
        {
            this.orderDeleter = orderDeleter;
        }
        public async Task Consume(ConsumeContext<InventoryResponse> context)
        {
            if (!context.Message.IsSuccess)
            {
                await orderDeleter.DeleteAsync(context.Message.OrderId);
            }
        }
    }
}
