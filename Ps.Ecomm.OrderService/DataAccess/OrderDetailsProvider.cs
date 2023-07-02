using Dapper;
using Microsoft.Data.SqlClient;
using Ps.Ecomm.Models;
using System.Net.Http;
using System.Text.Json;

namespace Ps.Ecomm.OrderService.DataAccess
{
    public class OrderDetailsProvider : IOrderDetailsProvider
    {
        private string _connectionString;
        public OrderDetailsProvider(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<OrderDetail[]> GetAsync()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var query = @"SELECT o.UserName As 'User', od.ProductName, od.Quantity
                                FROM OrderDetails od
                                INNER JOIN Orders o ON od.OrderId = o.Id";
                var orderDetials = await connection.QueryAsync<OrderDetail>(query);
                return orderDetials.ToArray();
            }
            catch (System.Exception exc)
            {
                return Array.Empty<OrderDetail>();
            }
        }
    }
}
