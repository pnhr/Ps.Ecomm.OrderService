using Microsoft.AspNetCore.Mvc;
using Ps.Ecomm.Models;
using Ps.Ecomm.OrderService.DataAccess;
using Ps.Ecomm.PlaneRabbitMQ;
using System.Text.Json;

namespace Ps.Ecomm.OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderDetailsProvider orderDetailsProvider;
        private readonly IPublisher publisher;

        public OrderController(IOrderDetailsProvider orderDetailsProvider, IPublisher publisher)
        {
            this.orderDetailsProvider = orderDetailsProvider;
            this.publisher = publisher;
        }
        // GET: api/<OrderController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orderdetails = await orderDetailsProvider.Get();
            return Ok(orderdetails);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderDetail orderDetail)
        {
            IDictionary<string, object> messageMetadata = new Dictionary<string, object>();
            messageMetadata.Add(MQConstants.OBJECT_TYPE, Convert.ToString(orderDetail));
            publisher.Publish(JsonSerializer.Serialize(orderDetail), MQConstants.ROUTE_KEY_REPORT_ORDER, messageMetadata);
            return await Task.FromResult(Ok());
        }
    }
}
