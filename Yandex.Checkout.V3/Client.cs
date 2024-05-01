using System.Net;
using System.Text;

namespace Yandex.Checkout.V3;

/// <summary>
/// Yandex.Checkout HTTP API client
/// </summary>
public class Client
{
    public string UserAgent { get; }
    public string ApiUrl { get; }
    public string Authorization { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="shopId">Shop ID</param>
    /// <param name="secretKey">Secret web api key</param>
    /// <param name="apiUrl">API URL</param>
    /// <param name="userAgent">Agent name</param>
    public Client(
        string shopId,
        string secretKey,
        string apiUrl = "https://api.yookassa.ru/v3/",
        string userAgent = "Yandex.Checkout.V3 .NET Client")
    {
        if (string.IsNullOrWhiteSpace(shopId))
            throw new ArgumentNullException(nameof(shopId));
        if (string.IsNullOrWhiteSpace(secretKey))
            throw new ArgumentNullException(nameof(secretKey));
        if (string.IsNullOrWhiteSpace(apiUrl))
            throw new ArgumentNullException(nameof(apiUrl));
        if (!Uri.TryCreate(apiUrl, UriKind.Absolute, out Uri _))
            throw new ArgumentException($"'{nameof(apiUrl)}' is not a valid URL.");

        ApiUrl = apiUrl;
        if (!ApiUrl.EndsWith("/"))
            ApiUrl = apiUrl + "/";
        UserAgent = userAgent;
        Authorization = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(shopId + ":" + secretKey));
    }

    #region Sync

    /// <summary>
    /// Deal creation
    /// </summary>
    /// <param name="newDeal">Deal information, <see cref="NewDeal"/></param>
    /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
    /// <returns><see cref="Deal"/></returns>
    public Deal CreateDeal(NewDeal newDeal, string idempotenceKey = null)
        => Query<Deal>("POST", newDeal, "deals", idempotenceKey);

    /// <summary>
    /// Get deal by id
    /// </summary>
    /// <param name="id">Deal id, <see cref="Deal.Id"/></param>
    /// <returns><see cref="Deal"/></returns>
    public Deal GetDeal(string id)
        => Query<Deal>("GET", null, $"deals/{id}", null);

    /// <summary>
    /// Payment creation
    /// </summary>
    /// <param name="payment">Payment information, <see cref="NewPayment"/></param>
    /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
    /// <returns><see cref="Payment"/></returns>
    public Payment CreatePayment(NewPayment payment, string idempotenceKey = null)
        => Query<Payment>("POST", payment, "payments/", idempotenceKey);

    /// <summary>
    /// Payout creation
    /// </summary>
    /// <param name="payout">Payout information, <see cref="NewPayout"/></param>
    /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
    /// <returns><see cref="Payout"/></returns>
    public Payout CreatePayout(NewPayout payout, string idempotenceKey = null)
        => Query<Payout>("POST", payout, "payouts", idempotenceKey);

    /// <summary>
    /// Payment capture
    /// </summary>
    /// <param name="id">Payment id, <see cref="Payment.Id"/></param>
    /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
    /// <returns><see cref="Payment"/></returns>
    public Payment CapturePayment(string id, string idempotenceKey = null)
        => Query<Payment>("POST", null, $"payments/{id}/capture", idempotenceKey);

    /// <summary>
    /// Payment capture, can be used to change payment amount.
    /// If you do not need to make any changes in payment use <see cref="CapturePayment(string,string)"/>
    /// </summary>
    /// <param name="payment">New payment data</param>
    /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
    /// <returns><see cref="Payment"/></returns>
    public Payment CapturePayment(Payment payment, string idempotenceKey = null)
        => CapturePayment(payment.Id, idempotenceKey);

    /// <summary>
    /// Query payment state
    /// </summary>
    /// <param name="id">Payment id, <see cref="Payment.Id"/></param>
    /// <returns><see cref="Payment"/></returns>
    public Payment GetPayment(string id)
        => Query<Payment>("GET", null, $"payments/{id}", null);

    /// <summary>
    /// Payment cancellation
    /// </summary>
    /// <param name="id">Payment id, <see cref="Payment.Id"/></param>
    /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
    /// <returns><see cref="Payment"/></returns>
    public Payment CancelPayment(string id, string idempotenceKey = null)
        => Query<Payment>("POST", null, $"payments/{id}/cancel", idempotenceKey);

    /// <summary>
    /// Refund creation
    /// </summary>
    /// <param name="refund">Refund data</param>
    /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
    /// <returns><see cref="Refund"/></returns>
    public Refund CreateRefund(NewRefund refund, string idempotenceKey = null)
        => Query<Refund>("POST", refund, "refunds", idempotenceKey);

    /// <summary>
    /// Query refund by id
    /// </summary>
    /// <param name="id">Refund id</param>
    /// <returns><see cref="Refund"/></returns>
    public Refund GetRefund(string id)
        => Query<Refund>("GET", null, $"refunds/{id}", null);

    /// <summary>
    /// Создание чека отдельно от платежа или возврата:
    /// https://yookassa.ru/developers/api#create_receipt
    /// </summary>
    public Receipt CreateReceipt(NewStandaloneReceipt receipt, string idempotenceKey = null)
        => Query<Receipt>("POST", receipt, "receipts", idempotenceKey);

    /// <summary>
    /// Query receipt by id
    /// </summary>
    /// <remarks>
    /// See https://yookassa.ru/developers/api#get_receipt
    /// </remarks>
    public Receipt GetReceipt(string id)
        => Query<Receipt>("GET", null, $"receipts/{id}", null);

    /// <summary>
    /// Query receipts by filter criteria
    /// </summary>
    /// <remarks>
    /// See https://yookassa.ru/developers/api#get_receipts_list
    /// </remarks>
    public IEnumerable<Receipt> GetReceipts(
        ReceiptFilter filter = null,
        ListOptions options = null)
    {
        return GetList<Receipt>("receipts", filter, options);
    }

    /// <summary>
    /// Query refunds by filter criteria
    /// </summary>
    /// <remarks>
    /// See https://yookassa.ru/developers/api#get_refunds_list
    /// </remarks>
    public IEnumerable<Refund> GetRefunds(
        RefundFilter filter = null,
        ListOptions options = null)
    {
        return GetList<Refund>("refunds", filter, options);
    }

    /// <summary>
    /// Gets payments matching given filter criteria.
    /// </summary>
    /// <remarks>
    /// See https://yookassa.ru/developers/api#get_payments_list
    /// </remarks>
    public IEnumerable<Payment> GetPayments(
        PaymentFilter filter = null,
        ListOptions options = null)
    {
        return GetList<Payment>("payments", filter, options);
    }

    /// <summary>
    /// Query deals by given search criteria
    /// </summary>
    /// <remarks>
    /// See https://yookassa.ru/developers/api#get_deals_list
    /// </remarks>
    public IEnumerable<Deal> GetDeals(
        DealFilter filter = null,
        ListOptions options = null)
    {
        return GetList<Deal>("deals", filter, options);
    }

    private IEnumerable<T> GetList<T>(
        string path, 
        object filter,
        ListOptions options)
    {
        string cursor = null;
        do
        {
            var batch = Query<ListBatch<T>>("GET", 
                body: null, 
                UrlHelper.MakeUrl(path, filter, cursor, options?.PageSize), 
                null);

            foreach (T item in batch.Items)
            {
                yield return item;
            }

            cursor = batch.NextCursor;
                
        } while (!string.IsNullOrEmpty(cursor));
    }

    #endregion Sync

    #region Parse

    /// <summary>
    /// Parses an HTTP request into a <see cref="Message"/> object.
    /// </summary>
    /// <returns>A <see cref="Notification"/> object subclass or null.</returns>
    public static Notification ParseMessage(string requestHttpMethod, string requestContentType, Stream requestInputStream)
    {
        return ParseMessage(requestHttpMethod, requestContentType, ReadToEnd(requestInputStream));
    }

    /// <summary>
    /// Parses an HTTP request into a <see cref="Notification"/> object.
    /// </summary>
    /// <returns>A <see cref="Notification"/> object subclass or null.</returns>
    public static Notification ParseMessage(string requestHttpMethod, string requestContentType, string jsonBody)
    {
        if (requestHttpMethod != "POST")
        {
            return null;
        }

        if (!requestContentType.StartsWith(ApplicationJson))
        {
            return null;
        }
            
        Message message = Serializer.DeserializeObject<Message>(jsonBody);

        return message.Event switch
        {
            "payment.waiting_for_capture" =>
                Serializer.DeserializeObject<PaymentWaitingForCaptureNotification>(jsonBody),
            "payment.succeeded" =>
                Serializer.DeserializeObject<PaymentSucceededNotification>(jsonBody),
            "payment.canceled" =>
                Serializer.DeserializeObject<PaymentCanceledNotification>(jsonBody),
            "refund.succeeded" =>
                Serializer.DeserializeObject<RefundSucceededNotification>(jsonBody),

            _ => null // Keep our options open in case new event types are added in the future
        };
    }

    #endregion Parse

    #region Helpers

    private static readonly HashSet<HttpStatusCode> KnownErrors = new()
    {
        HttpStatusCode.BadRequest,
        HttpStatusCode.Unauthorized,
        HttpStatusCode.Forbidden,
        HttpStatusCode.NotFound,
        (HttpStatusCode) 429, // Too Many Requests
        HttpStatusCode.InternalServerError
    };

    internal const string ApplicationJson = "application/json";

    internal static T ProcessResponse<T>(HttpStatusCode statusCode, string responseData, string contentType)
    {
        if (statusCode != HttpStatusCode.OK)
        {
            throw new YandexCheckoutException(statusCode,
                string.IsNullOrEmpty(responseData) || !KnownErrors.Contains(statusCode) || !contentType.StartsWith(ApplicationJson)
                    ? new Error { Code = statusCode.ToString(), Description = statusCode.ToString() }
                    : Serializer.DeserializeObject<Error>(responseData));
        }

        return Serializer.DeserializeObject<T>(responseData);
    }

    private T Query<T>(string method, object body, string url, string idempotenceKey)
    {
        HttpWebRequest request = CreateRequest(method, body, url, idempotenceKey ?? Guid.NewGuid().ToString());
        try
        {
            using var response = (HttpWebResponse)request.GetResponse();
            return GetResponse<T>(response);
        }
        catch (WebException e)
        {
            using var response = (HttpWebResponse)e.Response;
            return GetResponse<T>(response);
        }
    }

    private static T GetResponse<T>(HttpWebResponse response)
    {
        using Stream responseStream = response.GetResponseStream();
        using var reader = new StreamReader(responseStream ?? throw new InvalidOperationException("Response stream is null."));
        string responseData = reader.ReadToEnd();
        return ProcessResponse<T>(response.StatusCode, responseData, response.ContentType);
    }

    private HttpWebRequest CreateRequest(string method, object body, string url, string idempotenceKey)
    {
        var request = (HttpWebRequest)WebRequest.Create(ApiUrl + url);
        request.Method = method;
        request.ContentType = ApplicationJson;
        request.Headers.Add("Authorization", Authorization);

        if (!string.IsNullOrEmpty(idempotenceKey))
            request.Headers.Add("Idempotence-Key", idempotenceKey);

        if (UserAgent != null)
        {
            request.UserAgent = UserAgent;
        }

        if (body != null)
        {
            string json = Serializer.SerializeObject(body);
            byte[] postBytes = Encoding.UTF8.GetBytes(json);
            request.ContentLength = postBytes.Length;
            using Stream stream = request.GetRequestStream();
            stream.Write(postBytes, 0, postBytes.Length);
        }

        return request;
    }

    private static string ReadToEnd(Stream stream)
    {
        if (stream == null) return null;

        using var reader = new StreamReader(stream);
            
        return reader.ReadToEnd();
    }

    #endregion Helpers
}
