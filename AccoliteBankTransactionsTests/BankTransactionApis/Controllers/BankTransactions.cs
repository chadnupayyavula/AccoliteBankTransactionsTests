using BankTransactionApis.Templates;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace BankTransactionApis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankTransactions : ControllerBase
    {
        dbQueries dbContext = new dbQueries();

     

        [HttpPut(Name = "Post Transaction")]
        public string PostTransaction(MoneyTranscation obj)
        {
      return  dbContext.MakeTransaction(obj);
        }


        [HttpGet(Name = "Get Transaction")]
        public List<Transactions> GetTransaction(int accountId)
        {
            return dbContext.GetStatement(accountId);
        }
    }
}
