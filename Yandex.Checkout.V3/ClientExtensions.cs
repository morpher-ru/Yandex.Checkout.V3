using System;
using System.Net.Http;

namespace Yandex.Checkout.V3
{
    public static class ClientExtensions
    {
        public static AsyncClient MakeAsync(this Client client) =>
            MakeAsync(client, null, null, true);

        public static AsyncClient MakeAsync(this Client client, TimeSpan timeout) =>
            MakeAsync(client, timeout, null, true);

        public static AsyncClient MakeAsync(this Client client, TimeSpan? timeout,
            HttpClient httpClient, bool disposeOfHttpClient = false)
        {
            if (httpClient == null)
            {
                disposeOfHttpClient = true;
            }
            httpClient ??= new HttpClient();
            httpClient.BaseAddress = new Uri(client.ApiUrl);
            httpClient.DefaultRequestHeaders.Add("Authorization", client.Authorization);
            if (timeout.HasValue)
            {
                httpClient.Timeout = timeout.Value;
            }
            return new AsyncClient(httpClient, disposeOfHttpClient);
        }
    }
}
