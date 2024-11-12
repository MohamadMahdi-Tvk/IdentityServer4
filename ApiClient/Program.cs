using IdentityModel.Client;

namespace ApiClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            var discoveryDocument = client.GetDiscoveryDocumentAsync("https://localhost:44313/").Result;

            if (discoveryDocument.IsError)
            {
                Console.WriteLine(discoveryDocument.Error);
                return;
            }


            var accessToken = client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "ApiHavaShenasi",
                ClientSecret = "12345",
                Scope = "ApiHava"
            }).Result;

            if (accessToken.IsError)
            {
                Console.WriteLine(discoveryDocument.Error);
                return;
            }

            Console.WriteLine(accessToken.Json);

            Console.WriteLine("-----------------------------------------");

            HttpClient apiClient = new HttpClient();
            apiClient.SetBearerToken(accessToken.AccessToken);
            var result = apiClient.GetAsync("https://localhost:7059/WeatherForecast").Result;
            var content = result.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content);

        }
    }
}
