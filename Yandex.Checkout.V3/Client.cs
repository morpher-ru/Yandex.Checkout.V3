using System;
using System.IO;
using System.Net;
using System.Text;

#if !SYNCONLY
using System.Net.Http;
using System.Net.Http.Headers;
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
    public class Client
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
        private static readonly HttpClient HttpClient = new HttpClient();
        #endif

        private readonly string _userAgent;
        private readonly string _apiUrl;
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
            string apiUrl = "https://payment.yandex.net/api/v3/payments/",
            string userAgent = "Yandex.Checkout.V3 .NET Client")
        {
            if (string.IsNullOrWhiteSpace(shopId))
                throw new ArgumentNullException(nameof(shopId));
            if (string.IsNullOrWhiteSpace(secretKey))
                throw new ArgumentNullException(nameof(shopId));
            if (string.IsNullOrWhiteSpace(apiUrl))
                throw new ArgumentNullException(nameof(apiUrl));
            if (!Uri.TryCreate(apiUrl, UriKind.Absolute, out var uri))
                throw new ArgumentException($"{nameof(apiUrl)} should be valid URL");

            _apiUrl = apiUrl;
            if (!_apiUrl.EndsWith("/"))
                _apiUrl = apiUrl + "/";
            _userAgent = userAgent;
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
            => Query<Payment>("POST", payment, _apiUrl, idempotenceKey);

        /// <summary>
        /// Payment capture
        /// </summary>
        /// <param name="id">Payment id, <see cref="Payment.Id"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Payment Capture(string id, string idempotenceKey = null)
            => Query<Payment>("POST", null, _apiUrl + id + "/capture", idempotenceKey);
        
        /// <summary>
        /// Payment capture, can be used to change payment amount.
        /// If you do not need to make any changes in paymnet use <see cref="Capture(string,string)"/>
        /// </summary>
        /// <param name="payment">New payment data</param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Payment Capture(Payment payment, string idempotenceKey = null)
            => Query<Payment>("POST", payment, _apiUrl + payment.Id + "/capture", idempotenceKey);

        /// <summary>
        /// Query payment state
        /// </summary>
        /// <param name="id">Payment id, <see cref="Payment.Id"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Payment QueryPayment(string id, string idempotenceKey = null)
            => Query<Payment>("GET", null, _apiUrl + id, idempotenceKey);

        /// <summary>
        /// Refound creation
        /// </summary>
        /// <param name="refound">Refound data</param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <returns><see cref="NewRefound"/></returns>
        public Refound Refound(NewRefound refound, string idempotenceKey = null)
            => Query<Refound>("POST", refound, _apiUrl + "refunds", idempotenceKey);

        /// <summary>
        /// Payment cancelation
        /// </summary>
        /// <param name="id">Payment id, <see cref="Payment.Id"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Payment Cancel(string id, string idempotenceKey = null)
            => Query<Payment>("POST", null, _apiUrl + id + "/cancel", idempotenceKey);

        #endregion Sync

        #if !SYNCONLY
        #region Async

        /// <summary>
        /// Payment creation
        /// </summary>
        /// <param name="payment">Payment information, <see cref="NewPayment"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Payment"/></returns>
        public Task<Payment> CreatePaymentAsync(NewPayment payment, string idempotenceKey, CancellationToken cancellationToken)
            => QueryAsync<Payment>(HttpMethod.Post, payment, _apiUrl, idempotenceKey, cancellationToken);

        /// <inheritdoc cref="CreatePaymentAsync(NewPayment,string,CancellationToken)"/>
        public Task<Payment> CreatePaymentAsync(NewPayment payment, string idempotenceKey = null)
            => CreatePaymentAsync(payment, idempotenceKey, CancellationToken.None);

        /// <summary>
        /// Payment capture
        /// </summary>
        /// <param name="id">Payment id, <see cref="Payment.Id"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Payment"/></returns>
        public Task<Payment> CaptureAsync(string id, string idempotenceKey, CancellationToken cancellationToken)
            => QueryAsync<Payment>(HttpMethod.Post, null, _apiUrl + id + "/capture", idempotenceKey, cancellationToken);

        /// <inheritdoc cref="CaptureAsync(string,string,CancellationToken)"/>
        public Task<Payment> CaptureAsync(string id, string idempotenceKey = null)
            => CaptureAsync(id, idempotenceKey, CancellationToken.None);
        
        /// <summary>
        /// Payment capture, can be used to change payment amount.
        /// If you do not need to make any changes in paymnet use <see cref="CaptureAsync(string,string,System.Threading.CancellationToken)"/>
        /// </summary>
        /// <param name="payment">New payment data</param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Payment"/></returns>
        public Task<Payment> CaptureAsync(Payment payment, string idempotenceKey, CancellationToken cancellationToken)
            => QueryAsync<Payment>(HttpMethod.Post, payment, _apiUrl + payment.Id + "/capture", idempotenceKey, cancellationToken);

        /// <inheritdoc cref="CaptureAsync(Payment,string,CancellationToken)"/>
        public Task<Payment> CaptureAsync(Payment payment, string idempotenceKey = null)
            => CaptureAsync(payment, idempotenceKey, CancellationToken.None);

        /// <summary>
        /// Query payment state
        /// </summary>
        /// <param name="id">Payment id, <see cref="Payment.Id"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Payment"/></returns>
        public Task<Payment> QueryPaymentAsync(string id, string idempotenceKey, CancellationToken cancellationToken)
            => QueryAsync<Payment>(HttpMethod.Get, null, _apiUrl + id, idempotenceKey, cancellationToken);

        /// <inheritdoc cref="QueryPaymentAsync(string,string,CancellationToken)"/>
        public Task<Payment> QueryPaymentAsync(string id, string idempotenceKey = null)
            => QueryPaymentAsync(id, idempotenceKey, CancellationToken.None);

        /// <summary>
        /// Refound creation
        /// </summary>
        /// <param name="refound">Refound data</param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="NewRefound"/></returns>
        public Task<Refound> RefoundAsync(NewRefound refound, string idempotenceKey, CancellationToken cancellationToken)
            => QueryAsync<Refound>(HttpMethod.Post, refound, _apiUrl + "refunds", idempotenceKey, cancellationToken);

        /// <inheritdoc cref="RefoundAsync(Yandex.Checkout.V3.NewRefound,string,System.Threading.CancellationToken)"/>
        public Task<Refound> RefoundAsync(NewRefound refound, string idempotenceKey = null)
            => RefoundAsync(refound, idempotenceKey, CancellationToken.None);

        /// <summary>
        /// Payment cancellation
        /// </summary>
        /// <param name="id">Payment id, <see cref="Payment.Id"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Payment"/></returns>
        public Task<Payment> CancelAsync(string id, string idempotenceKey, CancellationToken cancellationToken)
            => QueryAsync<Payment>(HttpMethod.Post, null, _apiUrl + id + "/cancel", idempotenceKey, cancellationToken);

        /// <inheritdoc cref="CancelAsync(string,string,CancellationToken)"/>
        public Task<Payment> CancelAsync(string id, string idempotenceKey = null)
            => CancelAsync(id, idempotenceKey, CancellationToken.None);

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
            if (requestHttpMethod == "POST" && requestContentType == "application/json; charset=UTF-8")
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
            using (var request = CreateAsyncRequest(method, body, url, idempotenceKey))
            {
                var response = await HttpClient.SendAsync(request, cancellationToken);
                using (response)
                {
                    var responseData = response.Content == null
                        ? null
                        : await response.Content.ReadAsStringAsync();

                    return ProcessResponse<T>(response.StatusCode, responseData);
                }
            }
        }


        private HttpRequestMessage CreateAsyncRequest(HttpMethod method, object body, string url,
            string idempotenceKey)
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(url);
            request.Method = method;
            var content = body != null
                ? new StringContent(SerializeObject(body), Encoding.UTF8)
                : new StringContent(string.Empty);

            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            
            request.Content = content;
            request.Headers.Add("Authorization", _authorization);
            request.Headers.Add("Idempotence-Key", idempotenceKey ?? Guid.NewGuid().ToString());
            
            if(!string.IsNullOrEmpty(_userAgent))
                request.Headers.UserAgent.ParseAdd(_userAgent);

            return request;
        }
        #endif

        private static T ProcessResponse<T>(HttpStatusCode statusCode, string responseData)
        {
            if (statusCode != HttpStatusCode.OK)
            {
                throw new YandexCheckoutException((int) statusCode,
                    string.IsNullOrEmpty(responseData)
                        ? new Error {Code = "unknown", Description = "Unknown error"}
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
                return ProcessResponse<T>(response.StatusCode, responseData);
            }
        }

        private HttpWebRequest CreateRequest(string method, object body, string url, string idempotenceKey)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", _authorization);

            // Похоже, что этот заголовок обязателен, без него говорит (400) Bad Request.
            request.Headers.Add("Idempotence-Key", idempotenceKey ?? Guid.NewGuid().ToString());

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
    }
}
