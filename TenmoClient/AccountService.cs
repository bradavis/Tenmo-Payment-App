using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient
{
    public class AccountService
    {
        private readonly static string API_BASE_URL = "https://localhost:44315/";
        private readonly IRestClient client = new RestClient();

        public decimal GetCurrentBalance()
        {
            string token = UserService.GetToken();
            RestRequest request = new RestRequest(API_BASE_URL + "account/balance");
            client.Authenticator = new JwtAuthenticator(token);
            IRestResponse<decimal> response = client.Get<decimal>(request);
            
            

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                Console.WriteLine("An error occurred communicating with the server.");
                return -1;
            }
            else if (!response.IsSuccessful)
            {
                    Console.WriteLine("An error response was received from the server. The status code is " + (int)response.StatusCode);
                
                return -1;
            }
            else
            {
                
                return Convert.ToDecimal(response.Data);
            }
            //jwt c# web api get user

        }


    }
}
