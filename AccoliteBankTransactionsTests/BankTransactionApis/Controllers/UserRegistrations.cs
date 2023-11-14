using BankTransactionApis.Templates;
using Microsoft.AspNetCore.Mvc;

namespace BankTransactionApis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserRegistrations : ControllerBase
    {
       

        private readonly ILogger<UserRegistrations> _logger;
        dbQueries dbContext = new dbQueries();

        public UserRegistrations(ILogger<UserRegistrations> logger)
        {
            _logger = logger;
        }



        [HttpPost(Name = "Create Customer")]
        public string Post(CustomerCreation obj)
        {
            return dbContext.RegisterCustomer(obj);
        }


        [HttpGet(Name = "Get Customer")]
        public List<CustomerResponseApi> GetCustomer(int accountNumber = 0, string customerName = "null")
        {
            return dbContext.GetUser(accountNumber, customerName);
        }


        [HttpDelete(Name = "Delete Customer")]
        public string RemoveCustomer(int accountNumber = 0, string customerName = "null")
        {
            return dbContext.RemoveUser(accountNumber, customerName);
        }


    }
}