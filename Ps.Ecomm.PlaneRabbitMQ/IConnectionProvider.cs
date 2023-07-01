using RabbitMQ.Client;
using System;

namespace Ps.Ecomm.PlaneRabbitMQ
{
    public interface IConnectionProvider : IDisposable
    {
        IConnection GetConnection();
    }
}