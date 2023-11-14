namespace BankTransactionApis.Templates
{
    public class CustomerCreation
    {
        public string CustomerName { get; set; }

        public bool multipleAccounts { get; set; }

        public int numberofAccounts { get; set; }
    }

    public class CustomerResponseApi
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
    }

  
}
