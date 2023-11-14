using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AccoliteBankTransactionsTests.Templates.Request
{
    [DataContract]
    public class AccountCreation
    {
        [DataMember(Name = "customerName", EmitDefaultValue = false)]
        public string CustomerName { get; set; }

        [DataMember(Name = "multipleAccounts")]
        public bool MultipleAccounts { get; set; }

        [DataMember(Name = "numberofAccounts", EmitDefaultValue = false)]
        public int NumberofAccounts { get; set; }
    }


}
