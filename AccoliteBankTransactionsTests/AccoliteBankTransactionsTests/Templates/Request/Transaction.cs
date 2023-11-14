using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AccoliteBankTransactionsTests.Templates.Request
{
    [DataContract]
    public class Transaction
    {
        [DataMember(Name = "customerId", EmitDefaultValue = false)]
        public int CustomerId { get; set; }

        [DataMember(Name = "type", EmitDefaultValue = false)]
        public int Type { get; set; }  // 0 = credit and 1 = debit

        [DataMember(Name = "amount", EmitDefaultValue = false)]
        public int Amount { get; set; }
    }


}
