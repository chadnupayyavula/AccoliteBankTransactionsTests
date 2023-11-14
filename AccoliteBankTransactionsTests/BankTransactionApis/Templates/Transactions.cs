namespace BankTransactionApis.Templates
{
    public class Transactions
    {
        public int CustomerId { get; set; }

        //public string CustomerName { get; set; }

        public int Credit { get; set; }

        public int Debit { get; set; }

        public int Balance { get; set; }

        public DateTime lastUpdated { get; set; }
    }

    public class MoneyTranscation
    {
        public int CustomerId { get; set; }

        public TansactionType Type { get; set; }

        public double Amount { get; set; }

    }

    public enum TansactionType
    {
        Credit,
        Debit
    }
}
