using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ps.Ecomm.PlaneRabbitMQ
{
    public interface ISubscriber : IDisposable
    {
        void Subscribe(Func<string, IDictionary<string, object>, bool> callback);
        Task SubscribeAsync(Func<string, IDictionary<string, object>, Task<bool>> callback);
    }
}