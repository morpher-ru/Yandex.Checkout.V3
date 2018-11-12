using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

#if !SYNCONLY
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
#endif

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Yamdex.Checkout HTTP API client
    /// </summary>
    public class Client : IDisposable
    {
        private static readonly IContractResolver ContractResolver = new DefaultContractResolver()
        {
            NamingStrategy = new SnakeCaseNamingStrategy()
        };

        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = ContractResolver,
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Ignore,
        };
        
        #if !SYNCONLY
        private readonly HttpClient _httpClient = new HttpClient();
        #endif

        private readonly string _userAgent;
        private readonly string _authorization;

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
            string apiUrl = "https://payment.yandex.net/api/v3/",
            string userAgent = "Yandex.Checkout.V3 .NET Client")
        {
            if (string.IsNullOrWhiteSpace(shopId))
                throw new ArgumentNullException(nameof(shopId));
            if (string.IsNullOrWhiteSpace(secretKey))
                throw new ArgumentNullException(nameof(shopId));
            _userAgent = userAgent;
            _httpClient.BaseAddress = new Uri(apiUrl, UriKind.Absolute);
            _authorization = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(shopId + ":" + secretKey));
        }

        #region Sync

        /// <summary>
        /// Payment creation
        /// </summary>
        /// <param name="payment">Payment information, <see cref="NewPayment"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Payment CreatePayment(NewPayment payment, string idempotenceKey = null)
            => Query<Payment>("POST", payment, "payments/", idempotenceKey ?? Guid.NewGuid().ToString());

        /// <summary>
        /// Payment capture
        /// </summary>
        /// <param name="id">Payment id, <see cref="Payment.Id"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Payment CapturePayment(string id, string idempotenceKey = null)
            => Query<Payment>("POST", null, $"payments/{id}/capture", idempotenceKey ?? Guid.NewGuid().ToString());

        /// <summary>
        /// Payment capture, can be used to change payment amount.
        /// If you do not need to make any changes in payment use <see cref="CapturePayment(string,string)"/>
        /// </summary>
        /// <param name="payment">New payment data</param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Payment CapturePayment(Payment payment, string idempotenceKey = null)
            => Query<Payment>("POST", payment,$"payments/{payment.Id}/capture", idempotenceKey ?? Guid.NewGuid().ToString());

        /// <summary>
        /// Query payment state
        /// </summary>
        /// <param name="id">Payment id, <see cref="Payment.Id"/></param>
        /// <returns><see cref="Payment"/></returns>
        public Payment QueryPayment(string id)
            => Query<Payment>("GET", null, $"payments/{id}", null);

        /// <summary>
        /// Payment cancelation
        /// </summary>
        /// <param name="id">Payment id, <see cref="Payment.Id"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Payment CancelPayment(string id, string idempotenceKey = null)
            => Query<Payment>("POST", null, $"payments/{id}/cancel", idempotenceKey ?? Guid.NewGuid().ToString());

        /// <summary>
        /// Refund creation
        /// </summary>
        /// <param name="refund">Refund data</param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <returns><see cref="NewRefund"/></returns>
        public Refund CreateRefund(NewRefund refund, string idempotenceKey = null)
            => Query<Refund>("POST", refund, $"refunds", idempotenceKey ?? Guid.NewGuid().ToString());

        /// <summary>
        /// Query refund
        /// </summary>
        /// <param name="id">Refund id</param>
        /// <returns><see cref="NewRefund"/></returns>
        public Refund QueryRefund(string id)
            => Query<Refund>("GET", null, $"refunds/{id}", null);

        #endregion Sync

        #if !SYNCONLY
        #region Async

        /// <summary>
        /// Payment creation
        /// </summary>
        /// <param name="payment">Payment information, <see cref="NewPayment"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Task<Payment> CreatePaymentAsync(NewPayment payment, CancellationToken cancellationToken, string idempotenceKey = null)
            => QueryAsync<Payment>(HttpMethod.Post, payment, $"payments", idempotenceKey ?? Guid.NewGuid().ToString(), cancellationToken);

        /// <inheritdoc cref="CreatePaymentAsync(Yandex.Checkout.V3.NewPayment,System.Threading.CancellationToken,string)"/>
        public Task<Payment> CreatePaymentAsync(NewPayment payment, string idempotenceKey = null)
            => CreatePaymentAsync(payment, CancellationToken.None, idempotenceKey);

        /// <summary>
        /// Payment capture
        /// </summary>
        /// <param name="id">Payment id, <see cref="Payment.Id"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Task<Payment> CapturePaymentAsync(string id, CancellationToken cancellationToken, string idempotenceKey = null)
            => QueryAsync<Payment>(HttpMethod.Post, null, $"payments/{id}/capture", idempotenceKey ?? Guid.NewGuid().ToString(), cancellationToken);

        /// <inheritdoc cref="CapturePaymentAsync(string,System.Threading.CancellationToken,string)"/>
        public Task<Payment> CapturePaymentAsync(string id, string idempotenceKey = null)
            => CapturePaymentAsync(id, CancellationToken.None, idempotenceKey);

        /// <summary>
        /// Payment capture, can be used to change payment amount.
        /// If you do not need to make any changes in payment use <see cref="CapturePaymentAsync(string,System.Threading.CancellationToken,string)"/>
        /// </summary>
        /// <param name="payment">New payment data</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Task<Payment> CapturePaymentAsync(Payment payment, CancellationToken cancellationToken, string idempotenceKey = null)
            => QueryAsync<Payment>(HttpMethod.Post, payment, $"payments/{payment.Id}/capture", idempotenceKey ?? Guid.NewGuid().ToString(), cancellationToken);

        /// <inheritdoc cref="CapturePaymentAsync(Yandex.Checkout.V3.Payment,System.Threading.CancellationToken,string)"/>
        public Task<Payment> CapturePaymentAsync(Payment payment, string idempotenceKey = null)
            => CapturePaymentAsync(payment, CancellationToken.None, idempotenceKey);

        /// <summary>
        /// Query payment state
        /// </summary>
        /// <param name="id">Payment id, <see cref="Payment.Id"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Payment"/></returns>
        public Task<Payment> QueryPaymentAsync(string id, CancellationToken cancellationToken)
            => QueryAsync<Payment>(HttpMethod.Get, null, $"payments/{id}", null, cancellationToken);

        /// <inheritdoc cref="QueryPaymentAsync(string,CancellationToken)"/>
        public Task<Payment> QueryPaymentAsync(string id)
            => QueryPaymentAsync(id, CancellationToken.None);

        /// <summary>
        /// Payment cancellation
        /// </summary>
        /// <param name="id">Payment id, <see cref="Payment.Id"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Task<Payment> CancelPaymentAsync(string id, CancellationToken cancellationToken, string idempotenceKey = null)
            => QueryAsync<Payment>(HttpMethod.Post, null, $"payments/{id}/cancel", idempotenceKey ?? Guid.NewGuid().ToString(), cancellationToken);

        /// <inheritdoc cref="CancelPaymentAsync(string,System.Threading.CancellationToken,string)"/>
        public Task<Payment> CancelPaymentAsync(string id, string idempotenceKey = null)
            => CancelPaymentAsync(id, CancellationToken.None, idempotenceKey);

        /// <summary>
        /// Refund creation
        /// </summary>
        /// <param name="refund">Refund data</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <returns><see cref="Refund"/></returns>
        public Task<Refund> CreateRefundAsync(NewRefund refund, CancellationToken cancellationToken, string idempotenceKey = null)
            => QueryAsync<Refund>(HttpMethod.Post, refund, $"refunds", idempotenceKey ?? Guid.NewGuid().ToString(), cancellationToken);

        /// <inheritdoc cref="CreateRefundAsync(Yandex.Checkout.V3.NewRefund,System.Threading.CancellationToken,string)"/>
        public Task<Refund> CreateRefundAsync(NewRefund refund, string idempotenceKey = null)
            => CreateRefundAsync(refund, CancellationToken.None, idempotenceKey);

        /// <summary>
        /// Query refund
        /// </summary>
        /// <param name="id">Refund id</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Refund"/></returns>
        public Task<Refund> QueryRefundAsync(string id, CancellationToken cancellationToken)
            => QueryAsync<Refund>(HttpMethod.Post, null, $"refunds/{id}", null, cancellationToken);

        /// <inheritdoc cref="QueryRefundAsync(string,System.Threading.CancellationToken)"/>
        public Task<Refund> QueryRefundAsync(string id)
            => QueryRefundAsync(id, CancellationToken.None);

        #endregion Async
        #endif

        #region Parse

        /// <summary>
        /// Parses an HTTP request into a <see cref="Message"/> object.
        /// </summary>
        /// <returns>A <see cref="Message"/> object or null.</returns>
        public static Message ParseMessage(string requestHttpMethod, string requestContentType, Stream requestInputStream)
        {
            return ParseMessage(requestHttpMethod, requestContentType, ReadToEnd(requestInputStream));
        }

        /// <summary>
        /// Parses an HTTP request into a <see cref="Message"/> object.
        /// </summary>
        /// <returns>A <see cref="Message"/> object or null.</returns>
        public static Message ParseMessage(string requestHttpMethod, string requestContentType, string jsonBody)
        {
            Message message = null;
            if (requestHttpMethod == "POST" && requestContentType.StartsWith(ApplicationJson))
            {
                message = DeserializeObject<Message>(jsonBody);
            }
            return message;
        }

        #endregion Parse

        #region Serialization

        public static T DeserializeObject<T>(string data) => JsonConvert.DeserializeObject<T>(data, SerializerSettings);

        public static string SerializeObject(object value) => JsonConvert.SerializeObject(value, SerializerSettings);

        #endregion Serialization

        #region Helpers

        #if !SYNCONLY
        private async Task<T> QueryAsync<T>(HttpMethod method, object body, string url, string idempotenceKey, CancellationToken cancellationToken)
        {
            using (var request = CreateRequest(method, body, url, idempotenceKey))
            {
                var response = await _httpClient.SendAsync(request, cancellationToken);
                using (response)
                {
                    var responseData = response.Content == null
                        ? null
                        : await response.Content.ReadAsStringAsync();

                    return ProcessResponse<T>(response.StatusCode, responseData, response.Content?.Headers?.ContentType?.MediaType ?? string.Empty);
                }
            }
        }

        private HttpRequestMessage CreateRequest(HttpMethod method, object body, string url,
            string idempotenceKey)
        {
            var request = new HttpRequestMessage {RequestUri = new Uri(url), Method = method };

            if (body != null)
            {
                request.Content = new StringContent(SerializeObject(body), Encoding.UTF8, ApplicationJson);
            }
            request.Headers.Add("Authorization", _authorization);

            if (!string.IsNullOrEmpty(idempotenceKey))
                request.Headers.Add("Idempotence-Key", idempotenceKey);
            
            if(!string.IsNullOrEmpty(_userAgent))
                request.Headers.UserAgent.ParseAdd(_userAgent);

            return request;
        }
        #endif

        private static readonly HashSet<HttpStatusCode> KnownErrors = new HashSet<HttpStatusCode>
        {
            HttpStatusCode.BadRequest, 
            HttpStatusCode.Unauthorized, 
            HttpStatusCode.Forbidden, 
            HttpStatusCode.NotFound, 
            (HttpStatusCode) 429, // Too Many Requests
            HttpStatusCode.InternalServerError
        };

        static readonly string ApplicationJson = "application/json";

        private static T ProcessResponse<T>(HttpStatusCode statusCode, string responseData, string contentType)
        {
            if (statusCode != HttpStatusCode.OK)
            {
                throw new YandexCheckoutException(statusCode,
                    string.IsNullOrEmpty(responseData) || ! KnownErrors.Contains(statusCode) || !contentType.StartsWith(ApplicationJson)
                        ? new Error {Code = statusCode.ToString(), Description = statusCode.ToString()}
                        : DeserializeObject<Error>(responseData));
            }

            return DeserializeObject<T>(responseData);
        }

        private T Query<T>(string method, object body, string url, string idempotenceKey)
        {
            HttpWebRequest request = CreateRequest(method, body, url, idempotenceKey);
            using (var response = (HttpWebResponse)request.GetResponse())
            using (Stream responseStream = response.GetResponseStream())
            using (var sr = new StreamReader(responseStream ?? throw new InvalidOperationException("Response stream is null.")))
            {
                string responseData = sr.ReadToEnd();
                return ProcessResponse<T>(response.StatusCode, responseData, response.ContentType);
            }
        }

        private HttpWebRequest CreateRequest(string method, object body, string url, string idempotenceKey)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.ContentType = ApplicationJson;
            request.Headers.Add("Authorization", _authorization);

            if (!string.IsNullOrEmpty(idempotenceKey))
                request.Headers.Add("Idempotence-Key", idempotenceKey);

            if (_userAgent != null)
            {
                request.UserAgent = _userAgent;
            }

            if (body != null)
            {
                var json = SerializeObject(body);
                var postBytes = Encoding.UTF8.GetBytes(json);
                request.ContentLength = postBytes.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(postBytes, 0, postBytes.Length);
                }
            }

            return request;
        }

        private static string ReadToEnd(Stream stream)
        {
            if (stream == null) return null;

            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        #endregion Helpers

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
