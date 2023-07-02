namespace Ps.Ecomm.OrderService.DataAccess
{
    public interface IOrderDeleter
    {
        Task DeleteAsync(int orderId);
    }
}