using System.Net.Http;

namespace Yandex.Checkout.V3;

public static class ClientExtensions
{
    public static AsyncClient MakeAsync(this Client client) => 
        new(NewHttpClient(), true, client);

    public static AsyncClient MakeAsync(this Client client, TimeSpan timeout)
    {
        HttpClient httpClient = NewHttpClient();
        httpClient.Timeout = timeout;
        return new AsyncClient(httpClient, true, client);
    }

    /// <summary>
    /// Creates an AsyncClient that uses the given HttpClient.
    /// </summary>
    public static AsyncClient MakeAsync(this Client client, HttpClient httpClient)
    {
        return new AsyncClient(httpClient, disposeOfHttpClient: false, client);
    }

    private static HttpClient NewHttpClient()
    {
        return new HttpClient();
    }
}
