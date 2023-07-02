using Ps.Ecomm.Models;

namespace Ps.Ecomm.OrderService.DataAccess
{
    public interface IOrderCreator
    {
        Task<int> CreateAsync(OrderDetail orderDetail);
    }
}