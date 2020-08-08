using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using TenmoClient.Data;

namespace TenmoClient
{
    public static class UserService
    {
        private static API_User user = new API_User();
        private readonly static string API_BASE_URL = "https://localhost:44315/";
        private static IRestClient client = new RestClient();

        public static void SetLogin(API_User u)
        {
            user = u;
        }

        public static int GetUserId()
        {
            return user.UserId;
        }

        public static bool IsLoggedIn()
        {
            return !string.IsNullOrWhiteSpace(user.Token);
        }

        public static string GetToken()
        {
            return user?.Token ?? string.Empty;
        }

        public static List<User> GetUsers()
        {
            List<User> users = new List<User>();
            string token = GetToken();
            RestRequest request = new RestRequest(API_BASE_URL + "user");
            client.Authenticator = new JwtAuthenticator(token);
            IRestResponse<List<User>> response = client.Get<List<User>>(request);
            
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                Console.WriteLine("An error occurred communicating with the server.");
                return null;
            }
            else if (!response.IsSuccessful)
            {
                Console.WriteLine("An error response was received from the server. The status code is " + (int)response.StatusCode);

                return null;
            }
            else
            {

                return response.Data;
            }
        }

    }
}
