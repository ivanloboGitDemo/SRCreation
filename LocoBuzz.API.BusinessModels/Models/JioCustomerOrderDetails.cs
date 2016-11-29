using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocoBuzz.API.BusinessModels.Models
{
    public class CustomerOrder
    {
        public string status { get; set; }
        public string customerOrderType { get; set; }
        public string purchaseOrderNumber { get; set; }
        public string orderRefNumber { get; set; }
    }

    public class JioCustomerOrderDetails
    {
        public JioCustomerOrderDetails()
        { }
        public string order { get; set; }
        public List<CustomerOrder> custOrderDetails { get; set; }
    }
}
