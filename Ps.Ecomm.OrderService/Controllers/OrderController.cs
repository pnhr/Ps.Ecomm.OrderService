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
        private readonly IOrderCreator orderCreator;
        private readonly IPublishEndpoint publisher;

        public OrderController(IOrderDetailsProvider orderDetailsProvider, IOrderCreator orderCreator, IPublishEndpoint publisher)
        {
            this.orderDetailsProvider = orderDetailsProvider;
            this.orderCreator = orderCreator;
            this.publisher = publisher;
        }
        // GET: api/<OrderController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orderdetails = await orderDetailsProvider.GetAsync();
            return Ok(orderdetails);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderDetail orderDetail)
        {
            OrderRequest req = new OrderRequest();
            req.OrderId = await orderCreator.CreateAsync(orderDetail);
            req.ProductId = orderDetail.ProductId;
            req.Quantity = orderDetail.Quantity;
            await publisher.Publish<OrderRequest>(req);
            return Ok();
        }
    }
}
