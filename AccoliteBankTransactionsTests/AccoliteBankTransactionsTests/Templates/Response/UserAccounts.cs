using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccoliteBankTransactionsTests.Templates.Response
{
    public class UserAccounts
    {
        [JsonProperty(PropertyName = "customerId")]
        public int CustomerId { get; set; }

        [JsonProperty(PropertyName = "customerName")]
        public string CustomerName { get; set; }
    }


}
