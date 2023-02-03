using System;
using System.Net.Http;

namespace Yandex.Checkout.V3
{
    public static class ClientExtensions
    {
        public static AsyncClient MakeAsync(this Client client, TimeSpan? timeout = null, HttpClient httpClient = null)
        {
            httpClient ??= new HttpClient();
            httpClient.BaseAddress = new Uri(client.ApiUrl);
            httpClient.DefaultRequestHeaders.Add("Authorization", client.Authorization);
            if (timeout.HasValue)
            {
                httpClient.Timeout = timeout.Value;
            }
            return new AsyncClient(httpClient, true);
        }
    }
}
