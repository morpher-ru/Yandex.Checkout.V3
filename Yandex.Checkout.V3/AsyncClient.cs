using System.Net;
using System.Net.Http;
using System.Text;

namespace Yandex.Checkout.V3;
using static Client;

public partial class AsyncClient : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly bool _disposeOfHttpClient;
    public string UserAgent { get; }
    public string ApiUrl { get; }
    public string Authorization { get; }

    /// <param name="httpClient">
    /// Expects the <paramref name="httpClient"/>'s BaseAddress and Authorization headers to be set.
    /// </param>
    /// <param name="disposeOfHttpClient">
    /// <param name="shopId">Shop ID. If not provided <paramref name="httpClient"/> should be configured with <see cref="ClientExtensions.Confugure(HttpClient, string, string, string, string)"/>
    /// </param>
    /// <param name="secretKey">Secret web api key. If not provided <paramref name="httpClient"/> should be configured with <see cref="ClientExtensions.Confugure(HttpClient, string, string, string, string)"/></param>
    /// <param name="apiUrl">API URL</param>
    /// <param name="userAgent">Agent name</param>
    /// Dispose of the <paramref name="httpClient"/> when this AsyncClient is disposed.
    /// </param>
    public AsyncClient(HttpClient httpClient, 
        bool disposeOfHttpClient = false, 
        string shopId = null,
        string secretKey = null,
        string apiUrl = "https://api.yookassa.ru/v3/",
        string userAgent = "Yandex.Checkout.V3 .NET Client")
    {
        _httpClient = httpClient;
        _disposeOfHttpClient = disposeOfHttpClient;

        ApiUrl = apiUrl;
        if (!ApiUrl.EndsWith("/"))
            ApiUrl = apiUrl + "/";
        UserAgent = userAgent;
        Authorization = AuthorizationHeaderValue(shopId, secretKey);

    }

    /// <summary>
    /// Deal creation
    /// </summary>
    /// <param name="newDeal">Deal information, <see cref="NewDeal"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
    /// <returns><see cref="Deal"/></returns>
    public Task<Deal> CreateDealAsync(NewDeal newDeal, string idempotenceKey = null, CancellationToken cancellationToken = default)
        => QueryAsync<Deal>(HttpMethod.Post, newDeal, "deals", idempotenceKey, cancellationToken);

    /// <summary>
    /// Get deal by id
    /// </summary>
    /// <param name="id">Deal id, <see cref="Deal.Id"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Deal"/></returns>
    public Task<Deal> GetDealAsync(string id, CancellationToken cancellationToken = default)
        => QueryAsync<Deal>(HttpMethod.Get, null, $"deals/{id}", null, cancellationToken);

    /// <summary>
    /// Payment creation
    /// </summary>
    /// <param name="payment">Payment information, <see cref="NewPayment"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
    /// <returns><see cref="Payment"/></returns>
    public Task<Payment> CreatePaymentAsync(NewPayment payment, string idempotenceKey = null, CancellationToken cancellationToken = default)
        => QueryAsync<Payment>(HttpMethod.Post, payment, "payments", idempotenceKey, cancellationToken);

    /// <summary>
    /// Payout creation
    /// </summary>
    /// <param name="payout">Payout information, <see cref="NewPayout"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
    /// <returns><see cref="Payout"/></returns>
    public Task<Payout> CreatePayoutAsync(NewPayout payout, string idempotenceKey = null, CancellationToken cancellationToken = default)
        => QueryAsync<Payout>(HttpMethod.Post, payout, "payouts", idempotenceKey, cancellationToken);

    /// <summary>
    /// Payment capture
    /// </summary>
    /// <param name="id">Payment id, <see cref="Payment.Id"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
    /// <returns><see cref="Payment"/></returns>
    public Task<Payment> CapturePaymentAsync(string id, string idempotenceKey = null, CancellationToken cancellationToken = default)
        => QueryAsync<Payment>(HttpMethod.Post, null, $"payments/{id}/capture", idempotenceKey, cancellationToken);

    /// <summary>
    /// Payment capture, can be used to change payment amount.
    /// If you do not need to make any changes to the payment use <see cref="CapturePaymentAsync(string,string,CancellationToken)"/>
    /// </summary>
    /// <param name="payment">New payment data</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
    /// <returns><see cref="Payment"/></returns>
    public Task<Payment> CapturePaymentAsync(Payment payment, string idempotenceKey = null, CancellationToken cancellationToken = default)
        => CapturePaymentAsync(payment.Id, idempotenceKey, cancellationToken);

    /// <summary>
    /// Query payment state
    /// </summary>
    /// <param name="id">Payment id, <see cref="Payment.Id"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Payment"/></returns>
    public Task<Payment> GetPaymentAsync(string id, CancellationToken cancellationToken = default)
        => QueryAsync<Payment>(HttpMethod.Get, null, $"payments/{id}", null, cancellationToken);

    /// <summary>
    /// Payment cancellation
    /// </summary>
    /// <param name="id">Payment id, <see cref="Payment.Id"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
    /// <returns><see cref="Payment"/></returns>
    public Task<Payment> CancelPaymentAsync(string id, string idempotenceKey = null, CancellationToken cancellationToken = default)
        => QueryAsync<Payment>(HttpMethod.Post, null, $"payments/{id}/cancel", idempotenceKey, cancellationToken);

    /// <summary>
    /// Refund creation
    /// </summary>
    /// <param name="refund">Refund data</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
    /// <returns><see cref="Refund"/></returns>
    public Task<Refund> CreateRefundAsync(NewRefund refund, string idempotenceKey = null, CancellationToken cancellationToken = default)
        => QueryAsync<Refund>(HttpMethod.Post, refund, "refunds", idempotenceKey, cancellationToken);

    /// <summary>
    /// Query refund by id
    /// </summary>
    /// <param name="id">Refund id</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Refund"/></returns>
    public Task<Refund> GetRefundAsync(string id, CancellationToken cancellationToken = default)
        => QueryAsync<Refund>(HttpMethod.Get, null, $"refunds/{id}", null, cancellationToken);

    /// <summary>
    /// Создание чека отдельно от платежа или возврата:
    /// https://yookassa.ru/developers/api#create_receipt
    /// </summary>
    public Task<Receipt> CreateReceiptAsync(NewStandaloneReceipt receipt, string idempotenceKey = null,
        CancellationToken cancellationToken = default)
        => QueryAsync<Receipt>(HttpMethod.Post, receipt, "receipts", idempotenceKey, cancellationToken);

    /// <summary>
    /// Query receipt
    /// </summary>
    /// <remarks>
    /// See https://yookassa.ru/developers/api#get_receipt
    /// </remarks>
    public Task<Receipt> GetReceiptAsync(string id, CancellationToken cancellationToken = default)
        => QueryAsync<Receipt>(HttpMethod.Get, null, $"receipts/{id}", null, cancellationToken);

    private async Task<T> QueryAsync<T>(HttpMethod method, object body, string url, string idempotenceKey, CancellationToken cancellationToken)
    {
        using var request = CreateRequest(method, body, url, idempotenceKey ?? Guid.NewGuid().ToString());

        using var response = await _httpClient.SendAsync(request, cancellationToken);
            
        string responseData = response.Content == null
            ? null
            : await response.Content.ReadAsStringAsync();

        return ProcessResponse<T>(response.StatusCode, responseData, response.Content?.Headers?.ContentType?.MediaType ?? string.Empty);
    }

    private HttpRequestMessage CreateRequest(HttpMethod method, object body, string url, string idempotenceKey)
    {
        if (_httpClient.BaseAddress == null)
            url = ApiUrl + url;

        var request = new HttpRequestMessage(method, url)
        {
            Content = method == HttpMethod.Post
                ? new StringContent(Serializer.SerializeObject(body), Encoding.UTF8, ApplicationJson)
                : null
        };

        if (!_httpClient.DefaultRequestHeaders.Contains("Authorization"))
            request.Headers.Add("Authorization", Authorization);

        if (_httpClient.DefaultRequestHeaders.UserAgent.Count == 0)
            request.Headers.UserAgent.ParseAdd(UserAgent);

        if (!string.IsNullOrEmpty(idempotenceKey))
            request.Headers.Add("Idempotence-Key", idempotenceKey);

        return request;
    }

    public void Dispose()
    {
        if (_disposeOfHttpClient)
            _httpClient.Dispose();
    }

    /// <summary>
    /// Parses an HTTP request into a <see cref="Yandex.Checkout.V3.Notification"/> object.
    /// </summary>
    /// <returns>A <see cref="Notification"/> object subclass or null.</returns>
    public static async Task<Notification> ParseMessageAsync(string requestHttpMethod, string requestContentType, Stream requestInputStream)
    {
        return ParseMessage(requestHttpMethod, requestContentType, await ReadToEndAsync(requestInputStream));
    }

    private static async Task<string> ReadToEndAsync(Stream stream)
    {
        if (stream == null) return null;

        using var reader = new StreamReader(stream);

        return await reader.ReadToEndAsync();
    }
}
