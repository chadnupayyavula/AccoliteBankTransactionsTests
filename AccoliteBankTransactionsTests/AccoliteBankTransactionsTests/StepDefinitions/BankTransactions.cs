using AccoliteBankTransactionsTests.Actions;
using AccoliteBankTransactionsTests.Templates.Response;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace AccoliteBankTransactionsTests.StepDefinitions
{
    [Binding]
    public sealed class BankTransactions : CommonMethods
    {
        [Given(@"Customer (.*) opens (.*)")]
        public void GivenCustomerChanduOpens(string name, int number)
        {
            CustomerName = name;
            NumberOfAccounts = number;
            IsMutipleAccounts = number > 1 ? true : false;
        }

        [Given(@"Customer Account number (.*)")]
        public void GivenCustomerAccountNumber(int number)
        {
            AccountNumber = number;
        }


        [When(@"Create User Account")]
        public void WhenCreateUserWithName()
        {

            CreateUser(CustomerName, IsMutipleAccounts, NumberOfAccounts);
        }

        [Then(@"Verify user (.*) with name (.*)")]
        public void ThenVerifyUserCreatedwithname(string status, string name)
        {

            List<UserAccounts> account = GetUserAccounts(name: name);
            Console.WriteLine(JsonConvert.SerializeObject(account));
            if (status == "created")
            {
                Assert.AreEqual(name, account[0].CustomerName);
            }
            else if (status == "removed")
            {
                Assert.IsTrue(account.Count == 0);
            }

            else if (status == "not exist")
            {
                Assert.IsTrue(account.Count == 0);
            }



        }

        [Then(@"Verify user (.*) with returned account number")]
        public void ThenVerifyUserCreatedWithReturnedAccountNumber(string status)
        {
            foreach (int number in AccountNumbers)
            {
                List<UserAccounts> account = GetUserAccounts(number: number);
                Console.WriteLine(JsonConvert.SerializeObject(account));
                if (status == "created")
                {
                    Assert.IsFalse(account.Count == 0);
                }
                else if (status == "removed")
                {
                    Assert.IsTrue(account.Count == 0);
                }
                else if (status == "not exist")
                {
                    Assert.IsTrue(account.Count == 0);
                }

            }

        }

        [Then(@"Verify user (.*) with account number (.*)")]
        public void ThenVerifyUser(string status, int number)
        {
            List<UserAccounts> account = GetUserAccounts(number: number);
            Console.WriteLine(JsonConvert.SerializeObject(account));
            if (status == "created")
            {
                Assert.IsFalse(account.Count == 0);
            }
            else if (status == "removed")
            {
                Assert.IsTrue(account.Count == 0);
            }
            else if (status == "not exist")
            {
                Assert.IsTrue(account.Count == 0);
            }

        }


        [Then(@"Verify account balance must be (.*)")]
        public void ThenVerifyAccountBalanceMustBe(int balance)
        {
            foreach (int number in AccountNumbers)
            {
                List<AccountStatement> statement = GetAccountBalance(number);
                AccountStatement res = statement.OrderByDescending(s => s.LastUpdated).First();

                Assert.AreEqual(balance, res.Balance);
            }
        }



        [Then(@"Verify account balance must be (.*) and error message (.*)")]
        public void ThenVerifyAccountBalanceMustBeAndErrorMessage(int balance, string errormessage)
        {
            foreach (int number in AccountNumbers)
            {
                List<AccountStatement> statement = GetAccountBalance(number);
                AccountStatement res = statement.OrderByDescending(s => s.LastUpdated).First();

                Assert.AreEqual(balance, res.Balance);
                Assert.AreEqual(message, errormessage);
            }
        }

        [When(@"Try credit (.*) on account number (.*)")]
        public void WhenTryCreditOnAccountNumber(int value, int accountNumber)
        {
            MakeTransaction(accountNumber, 0, value);
        }


        [Then(@"verify error message (.*)")]
        public void ThenVerifyErrorMessageCustomerDoesNotExists(string error)
        {
            Assert.AreEqual(error, message);
        }


        [When(@"Try withdrawal (.*) on account number (.*)")]
        public void WhenTryWithdrawalOnAccountNumber(int accountNumber, int value)
        {
            MakeTransaction(accountNumber, 1, value);
        }


        [When(@"Get account statement for account number (.*)")]
        public void WhenGetAccountStatementForAccountNumber(int accountNumber)
        {
            statement = GetAccountBalance(accountNumber);
        }

        [Then(@"Statement should be empty")]
        public void ThenStatementShouldBeEmpty()
        {
            Assert.IsTrue(statement.Count == 0 );
        }

        [When(@"Remove user account with account number (.*)")]
        public void WhenRemoveUserAccountWithAccountNumber(int number)
        {
            RemoveAccount(number);
        }

        [When(@"Remove user account with account number")]
        public void WhenRemoveUserAccountWithAccountNumber()
        {
            foreach (int number in AccountNumbers)
            {
                RemoveAccount(number);
            }
        }

        [When(@"Remove User Account (.*)")]
        public void WhenRemoveUserAccount(int number)
        {
            RemoveAccount(number);
        }

        [When(@"Remove user account with name (.*)")]
        public void WhenRemoveUserAccountWithName(string name)
        {
            RemoveAccount(name: name);
        }

        [When(@"Make Transaction")]
        public void WhenMakeTransaction(Table table)
        {
            foreach (var row in table.Rows)
            {
                MakeTransaction(AccountNumbers[0], row[0] == "credit" ? 0 : 1, Convert.ToInt32(row[1]));
            }
        }


        [When(@"Make Transaction On the given account")]
        public void WhenMakeTransactionOnGivenAccount(Table table)
        {
            foreach (var row in table.Rows)
            {
                MakeTransaction(AccountNumber, row[0] == "credit" ? 0 : 1, Convert.ToInt32(row[1]));
            }
        }




        [When(@"Get statement for account")]
        public void WhenGetStatementForAccount()
        {
            foreach (int number in AccountNumbers)
            {
                statement = GetAccountBalance(number);
            }
        }

        [Then(@"Verify Response Status Code is (.*)")]
        public void ThenVerifyResponseStatusCodeIs(int statuscode)
        {
           Assert.AreEqual(StatusCode, statuscode);
        }

    }



}
