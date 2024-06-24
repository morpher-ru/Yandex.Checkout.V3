using System.Net;

namespace Yandex.Checkout.V3;

public abstract class ClientBase
{
    public string UserAgent { get; }
    public string ApiUrl { get; }
    public string Authorization { get; }
    protected internal const string DefaultUserAgent = "Yandex.Checkout.V3 .NET Client";
    protected internal const string ApplicationJson = "application/json";
    protected internal const string AuthorizationHeader = "Authorization";
    protected const string IdempotenceKeyHeader = "Idempotence-Key";
    protected const string DefaultApiUrl = "https://api.yookassa.ru/v3/";


    protected ClientBase(string shopId,
        string secretKey,
        string apiUrl = null,
        string userAgent = DefaultUserAgent)
    {
        UserAgent = userAgent;

        ApiUrl = GetApiUrl(apiUrl);

        if (!string.IsNullOrEmpty(secretKey) && !string.IsNullOrEmpty(shopId))
            Authorization = AuthorizationHeaderValue(shopId, secretKey);
    }

    public static string GetApiUrl(string apiUrl = null)
    {
        if (!string.IsNullOrEmpty(apiUrl))
        {
            if (!Uri.TryCreate(apiUrl, UriKind.Absolute, out Uri _))
                throw new ArgumentException($"'{nameof(apiUrl)}' is not a valid URL.");

            if (!apiUrl.EndsWith("/"))
                apiUrl = apiUrl + "/";

            return apiUrl;
        }

        return DefaultApiUrl;
    }


    protected internal static string AuthorizationHeaderValue(string shopId, string secretKey)
    {
        if (string.IsNullOrWhiteSpace(shopId))
            throw new ArgumentNullException(nameof(shopId));
        if (string.IsNullOrWhiteSpace(secretKey))
            throw new ArgumentNullException(nameof(secretKey));

        return "Basic " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(shopId + ":" + secretKey));
    }

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

    private static string ReadToEnd(Stream stream)
    {
        if (stream == null) return null;

        using var reader = new StreamReader(stream);

        return reader.ReadToEnd();
    }

    #endregion

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
}
