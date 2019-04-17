namespace Yandex.Checkout.V3
{
    public static class ClientExtensions
    {
        public static AsyncClient MakeAsync(this Client client) => 
            new AsyncClient(client.Authorization, client.ApiUrl, client.UserAgent);
    }
}
