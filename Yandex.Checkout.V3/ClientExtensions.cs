using System;
using System.Net.Http;

namespace Yandex.Checkout.V3
{
    public static class ClientExtensions
    {
        public static AsyncClient MakeAsync(this Client client) => 
            new(NewHttpClient(client), true);

        public static AsyncClient MakeAsync(this Client client, TimeSpan timeout)
        {
            HttpClient httpClient = NewHttpClient(client);
            httpClient.Timeout = timeout;
            return new AsyncClient(httpClient, true);
        }

        static HttpClient NewHttpClient(Client client)
        {
            var httpClient = new HttpClient {BaseAddress = new Uri(client.ApiUrl)};
            httpClient.DefaultRequestHeaders.Add("Authorization", client.Authorization);

            if (!string.IsNullOrEmpty(client.UserAgent))
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(client.UserAgent);
            return httpClient;
        }
    }
}
