using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Ps.Ecomm.Models;
using Ps.Ecomm.OrderService.DataAccess;
using System.Text.Json;

namespace Ps.Ecomm.OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderDetailsProvider orderDetailsProvider;
        private readonly IPublishEndpoint publisher;

        public OrderController(IOrderDetailsProvider orderDetailsProvider, IPublishEndpoint publisher)
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
            await publisher.Publish<OrderDetail>(orderDetail);
            return Ok();
        }
    }
}
