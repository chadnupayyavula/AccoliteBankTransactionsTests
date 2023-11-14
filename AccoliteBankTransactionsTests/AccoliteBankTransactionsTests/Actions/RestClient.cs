using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.Net.Http;
using RestSharp;
using Method = RestSharp.Method;
using RestRequest = RestSharp.RestRequest;

namespace AccoliteBankTransactionsTests.Actions
{
    public class RestClientService
    {
        private static RestClient restClient = new RestClient("https://accolitebanktransactions.azurewebsites.net");

        public static RestResponse GetResponse(string url)
        {
            try
            {
                var request = new RestRequest(url, Method.Get);
                var response = restClient.Execute(request);
                return response;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public static RestResponse Post(string url, object template)
        {
            try
            {
                var request = new RestRequest(url, Method.Post);
                request.AddJsonBody(template);
                var response = restClient.Execute(request);
                return response;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public static RestResponse Update(string url, object template)
        {
            try
            {
                var request = new RestRequest(url, Method.Put);
                request.AddJsonBody(template);
                var response = restClient.Execute(request);
                return response;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }


        public static RestResponse Delete(string url)
        {
            try
            {
                var request = new RestRequest(url, Method.Delete);
                var response = restClient.Execute(request);
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }




    }
}
