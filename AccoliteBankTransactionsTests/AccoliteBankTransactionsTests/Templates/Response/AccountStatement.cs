using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AccoliteBankTransactionsTests.Templates.Response
{
    public class AccountStatement
    {
        [JsonProperty(PropertyName = "customerId")]
        public int CustomerId { get; set; }

        [JsonProperty(PropertyName = "credit")]
        public int Credit { get; set; }

        [JsonProperty(PropertyName = "debit")]
        public int Debit { get; set; }

        [JsonProperty(PropertyName = "balance")]
        public int Balance { get; set; }

        [JsonProperty(PropertyName = "lastUpdated")]
        public DateTime LastUpdated { get; set; }
    }


}
