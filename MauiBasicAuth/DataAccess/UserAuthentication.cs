
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MauiBasicAuth.DataAccess
{
    public class UserAuthentication
    {
        public async Task<bool> AuthenticateUser(
            string username, string password)
        {
            using var client = new HttpClient();

            // Address of our running REST API
            client.BaseAddress = new Uri("http://localhost:5227/");

            // Encode the username and password
            string authToken = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{username}:{password}"));

            // Add Basic Authentication header
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", authToken);

            // Send request to ValuesController
            HttpResponseMessage response =
                await client.GetAsync("api/Values");

            // Return true if authentication succeeds
            return response.IsSuccessStatusCode;
        }
    }
}
