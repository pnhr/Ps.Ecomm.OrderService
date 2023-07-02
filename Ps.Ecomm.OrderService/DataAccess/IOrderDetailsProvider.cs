using Ps.Ecomm.Models;

namespace Ps.Ecomm.OrderService.DataAccess
{
    public interface IOrderDetailsProvider
    {
        Task<OrderDetail[]> GetAsync();
    }
}