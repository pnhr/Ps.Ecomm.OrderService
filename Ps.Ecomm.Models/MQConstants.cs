using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ps.Ecomm.Models
{
    public static class MQConstants
    {
        public const string EXCHANGE_REPORT = "report_exchange";

        #region RoutingKeys
        public const string ROUTE_KEY_REPORT_ALL = "report.*";
        public const string ROUTE_KEY_REPORT_ORDER = "report.order";
        public const string ROUTE_KEY_REPORT_PRODUCT = "report.product";
        #endregion


        public const string QUEUE_REPORT_ORDER = "report_queue";

        public const string OBJECT_TYPE = "ObjectType";
    }
}
