using System.Net.Http;

namespace Yandex.Checkout.V3;

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

    private static HttpClient NewHttpClient(Client client)
        => Confugure(new HttpClient(), client);

    public static HttpClient Confugure(this HttpClient httpClient, Client source) 
    {
        httpClient.BaseAddress = new Uri(source.ApiUrl);
        httpClient.DefaultRequestHeaders.Add(ClientBase.AuthorizationHeader, source.Authorization);
        if (!string.IsNullOrEmpty(source.UserAgent))
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(source.UserAgent);
        
        return httpClient;
    }

    public static HttpClient Confugure(this HttpClient httpClient, 
        string shopId,
        string secretKey,
        string apiUrl = null,
        string userAgent = ClientBase.DefaultUserAgent)
    {
        httpClient.BaseAddress = new Uri(ClientBase.GetApiUrl(apiUrl));
        httpClient.DefaultRequestHeaders.Add(ClientBase.AuthorizationHeader, ClientBase.AuthorizationHeaderValue(shopId, secretKey));

        if (!string.IsNullOrEmpty(userAgent))
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);

        return httpClient;
    }
}
