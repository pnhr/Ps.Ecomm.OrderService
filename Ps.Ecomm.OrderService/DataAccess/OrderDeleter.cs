using Dapper;
using Microsoft.Data.SqlClient;
using Ps.Ecomm.Models;

namespace Ps.Ecomm.OrderService.DataAccess
{
    public class OrderDeleter : IOrderDeleter
    {
        private readonly string _connectionString;

        public OrderDeleter(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public async Task DeleteAsync(int orderId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var transaction = await connection.BeginTransactionAsync();
            try
            {
                await connection.ExecuteAsync("DELETE FROM OrderDetails WHERE  OrderId = @orderId", new { orderId = orderId }, transaction: transaction);
                await connection.ExecuteAsync("DELETE FROM ORDERS WHERE Id = @orderId", new { orderId = orderId }, transaction: transaction);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
        }
    }
}
