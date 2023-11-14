using AccoliteBankTransactionsTests.Templates.Request;
using AccoliteBankTransactionsTests.Templates.Response;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;


namespace AccoliteBankTransactionsTests.Actions
{
    public class CommonMethods
    {
        public List<int> AccountNumbers = new List<int>();
        public List<UserAccounts> UserAccounts = new List<UserAccounts>();
        public string message;
        public List<AccountStatement> statement = new List<AccountStatement>();

        public string CustomerName;
        public int NumberOfAccounts;
        public bool IsMutipleAccounts;
        public int AccountNumber;
        public int StatusCode;

        private string ApiUrl = "https://accolitebanktransactions.azurewebsites.net/";
        public void CreateUser(string name, bool multipleAccounts, int number)
        {

            AccountCreation account = new AccountCreation();

            account.CustomerName = name;
            account.MultipleAccounts = multipleAccounts;
            account.NumberofAccounts = number;

            var request = JsonConvert.SerializeObject(account);

            RestResponse response = RestClientService.Post("UserRegistrations", account);
            StatusCode = (int)response.StatusCode;
            message = response.Content;

            Console.WriteLine(message);

        
                string[] lines = message.Split(new[] { "\\n" }, StringSplitOptions.None);
                foreach (string line in lines)
                {
                    if (int.TryParse(line.Trim('\"'), out int num))
                    {
                        AccountNumbers.Add(num);
                    }
                }
           
        }

        public List<UserAccounts> GetUserAccounts(int number = 0, string name = "null")
        {
            string url = $"{"UserRegistrations?accountNumber="}{number}&customerName={name}";


            RestResponse response = RestClientService.GetResponse(url);
            int code = (int)response.StatusCode;
            message = response.Content;

            return JsonConvert.DeserializeObject<List<UserAccounts>>(message);

        }

        public void RemoveAccount(int number = 0, string name = "null")
        {

            string url = $"{"UserRegistrations?accountNumber="}{number}&customerName={name}";

            RestResponse response = RestClientService.Delete(url);

            StatusCode = (int)response.StatusCode;
            message = response.Content;
        }

        public List<AccountStatement> GetAccountBalance(int number)
        {
            string url = $"{"BankTransactions?accountId="}{number}";

            RestResponse response = RestClientService.GetResponse(url);
            StatusCode = (int)response.StatusCode;
            message = response.Content;

            return JsonConvert.DeserializeObject<List<AccountStatement>>(message);

        }


        public string MakeTransaction(int accountnumber, int type, int amount)
        {
            Transaction trans = new Transaction();

            trans.CustomerId = accountnumber;
            trans.Type = type;
            trans.Amount = amount;

            var request = JsonConvert.SerializeObject(trans);

            RestResponse response = RestClientService.Update("BankTransactions", trans);
            StatusCode = (int)response.StatusCode;
            message = response.Content;

            Console.WriteLine(message + " -- " + StatusCode);
          return message;

        }
    }
}
