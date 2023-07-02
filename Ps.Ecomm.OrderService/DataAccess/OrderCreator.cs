using Dapper;
using Microsoft.Data.SqlClient;
using Ps.Ecomm.Models;

namespace Ps.Ecomm.OrderService.DataAccess
{
    public class OrderCreator : IOrderCreator
    {
        private string _connectionString;
        private readonly ILogger<OrderCreator> logger;

        public OrderCreator(string connectionString, ILogger<OrderCreator> logger)
        {
            _connectionString = connectionString;
            this.logger = logger;
        }
        public async Task<int> CreateAsync(OrderDetail orderDetail)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var transaction = await connection.BeginTransactionAsync();

            try
            {
                int id = await connection.QuerySingleAsync<int>("dbo.uspCreateOrder",
                                            new
                                            {
                                                UserId = 1,
                                                UserName = orderDetail.User
                                            },
                                            commandType: System.Data.CommandType.StoredProcedure,
                                            transaction: transaction);

                await connection.ExecuteAsync("dbo.uspCreateOrderDetail",
                                    new
                                    {
                                        OrderId = id,
                                        ProductId = orderDetail.ProductId,
                                        Quantity = orderDetail.Quantity,
                                        ProductName = orderDetail.ProductName
                                    },
                                    commandType: System.Data.CommandType.StoredProcedure,
                                    transaction: transaction);

                transaction.Commit();
                return id;
            }
            catch(Exception ex)
            {

                transaction.Rollback();
                return -1;
            }
        }
    }
}
