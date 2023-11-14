using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Net;
using System.Numerics;
using System.Transactions;
using BankTransactionApis.Templates;
using Microsoft.AspNetCore.Mvc;

namespace BankTransactionApis
{
    public class dbQueries
    {
        string connectionString =
            "Server=accoliteaqlserver.database.windows.net;Initial Catalog=AccoliteDb;Persist Security Info=False;User ID=accolite;Password=Test@123#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


        public string RegisterCustomer(CustomerCreation obj)
        {
            List<int> ids = new List<int>();
            if (obj.CustomerName == "")
            {
                // return "Please enter customer name";
                throw new Exception("Please enter customer name");
            }

            // SQL query to insert a record
            string insertQuery = $"INSERT INTO Users VALUES " + $"( \'{obj.CustomerName}\')" +
                                 "; SELECT SCOPE_IDENTITY();";

            // Values to be inserted

            string value1 = obj.CustomerName;


            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                    {

                        if (obj.multipleAccounts)
                        {
                            if (obj.numberofAccounts > 0)
                            {
                                for (int i = 0; i < obj.numberofAccounts; i++)
                                {
                                    object result = cmd.ExecuteScalar();

                                    if (result != null)
                                    {
                                        int lastInsertedId = Convert.ToInt32(result);
                                        ids.Add(lastInsertedId);

                                        string query =
                                            $"insert into Transactions values({lastInsertedId},100,0,100,\'{System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")}')";

                                        SqlCommand transcmd = new SqlCommand(query, connection);
                                        transcmd.ExecuteNonQuery();


                                        Console.WriteLine($"Last Inserted Identity Value: {lastInsertedId}");
                                    }

                                }
                            }
                            else
                            {
                                //  return "Please update how many accounts do you need";
                                throw new Exception("Please update how many accounts do you need?");
                            }
                        }
                        else
                        {
                            object result = cmd.ExecuteScalar();

                            if (result != null)
                            {
                                int lastInsertedId = Convert.ToInt32(result);

                                ids.Add(lastInsertedId);
                                string query =
                                    $"insert into Transactions values({lastInsertedId},100,0,100,\'{System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")}')";

                                SqlCommand transcmd = new SqlCommand(query, connection);
                                transcmd.ExecuteNonQuery();

                            }

                        }

                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return string.Join(Environment.NewLine, ids.ToArray()); ;
        }



        public string MakeTransaction(MoneyTranscation obj)
        {
            // SQL query to insert a record
            string insertQuery =
                $"select balance from Transactions where accountnumber={obj.CustomerId} order by lastUpdated desc";

            try
            {
                if (obj.Amount <= 0)
                {
                    //return "Please enter valid amount to do transaction";
                    throw new Exception("Please enter valid amount to do transaction");
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                    {
                        string query;
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            double balance = Convert.ToInt32(result);
                            if (obj.Type == 0)
                            {
                                if (obj.Amount > 10000)
                                {
                                    //return "You cannot deposit more than 10000";
                                    throw new Exception("You cannot deposit more than 10000");
                                }

                                query =
                                    $"update Transactions set credit = {obj.Amount},debit = 0, balance ={balance + obj.Amount}, lastUpdated = \'{System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")}' where accountnumber = {obj.CustomerId}";
                            }
                            else
                            {
                                double number = (obj.Amount / balance);
                                if (balance - obj.Amount < 100)
                                {
                                    //return "Balance amount should not be less than 100";
                                    throw new Exception("Balance amount should not be less than 100");
                                }

                                if (obj.Amount / balance * 100 > 90)
                                {
                                    //return "you cannot with draw more than 90% from your account.";
                                    throw new Exception("you cannot with draw more than 90% from your account.");
                                }

                                query =
                                    $"update Transactions set credit = 0,debit = {obj.Amount}, balance ={balance - obj.Amount}, lastUpdated = \'{System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")}' where accountnumber = {obj.CustomerId}";

                            }

                            SqlCommand transcmd = new SqlCommand(query, connection);
                            transcmd.ExecuteNonQuery();
                        }
                        else
                        {
                            //return "No record found with this customerId";
                            throw new Exception("No record found with this customerId");
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return "Success";
        }


        public List<CustomerResponseApi> GetUser(int? id, string customerName)
        {
            List<CustomerResponseApi> customers = new List<CustomerResponseApi>();

            string selectQuery = "";

            if (customerName == "null")
            {
                // SQL query to insert a record
                selectQuery = "Select * from Users where accountnumber = " + id;
            }
            else if (id == 0)
            {
                selectQuery = "Select * from Users where customerName = " + $" \'{customerName}'";
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                    {
                        // Create a DataReader to retrieve data
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Check if there are rows in the result set
                            if (reader.HasRows)
                            {
                                // Iterate through the rows and print the values
                                while (reader.Read())
                                {

                                    CustomerResponseApi obj = new CustomerResponseApi();
                                    obj.CustomerName = reader[1].ToString();
                                    obj.CustomerId = Convert.ToInt32(reader[0]);
                                    customers.Add(obj);
                                }
                            }

                        }
                    }
                }

                return customers;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Transactions> GetStatement(int id)
        {
            List<Transactions> transactions = new List<Transactions>();


            // SQL query to insert a record
            string selectQuery = "Select * from Transactions where accountnumber = " + id;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                    {
                        // Create a DataReader to retrieve data
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Check if there are rows in the result set
                            if (reader.HasRows)
                            {
                                // Iterate through the rows and print the values
                                while (reader.Read())
                                {

                                    Transactions obj = new Transactions();

                                    obj.CustomerId = Convert.ToInt32(reader[0]);
                                    obj.Credit = Convert.ToInt32(reader[1]);
                                    obj.Debit = Convert.ToInt32(reader[2]);
                                    obj.Balance = Convert.ToInt32(reader[3]);
                                    obj.lastUpdated = Convert.ToDateTime(reader[4]);

                                    transactions.Add(obj);
                                }
                            }

                        }
                    }
                }

                return transactions;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




        public string RemoveUser(int id, string customerName)
        {
            List<CustomerResponseApi> customers = new List<CustomerResponseApi>();
            string selectQuery = "";


            if (customerName == "null")
            {
                // SQL query to insert a record
                selectQuery = "Select * from Users where accountnumber = " + id;
            }
            else if (id == 0)
            {
                selectQuery = $"Select * from Users where customerName = " + $" \'{customerName}'";
            }



            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                {
                    // Create a DataReader to retrieve data
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check if there are rows in the result set
                        if (!reader.HasRows)
                        {
                            throw new Exception("Customer does not exists");
                        }
                        else
                        {
                            while (reader.Read())
                            {
                                CustomerResponseApi obj = new CustomerResponseApi();
                                obj.CustomerName = reader[1].ToString();
                                obj.CustomerId = Convert.ToInt32(reader[0]);
                                customers.Add(obj);
                            }
                        }
                    }
                }
            }

            foreach (CustomerResponseApi obj in customers)
            {
                selectQuery = "delete  from Transactions where accountnumber = " + obj.CustomerId;



                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    selectQuery = "delete  from Users where accountnumber = " + obj.CustomerId;
                    using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }


            return "Success";

        }

    }
}