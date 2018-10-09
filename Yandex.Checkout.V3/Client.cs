using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
#if !NET40
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Yamdex.Checkout HTTP API client
    /// </summary>
    public class Client
    {
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
            _apiUrl = apiUrl;
            if (!_apiUrl.EndsWith("/"))
                _apiUrl = apiUrl + "/";
            _userAgent = userAgent;
            _authorization = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(shopId + ":" + secretKey));
        }

        /// <summary>
        /// Payment creation
        /// </summary>
        /// <param name="payment">Payment information, <see cref="NewPayment"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Payment CreatePayment(NewPayment payment, string idempotenceKey = null)
        {
            return Post<Payment>(payment, _apiUrl, idempotenceKey);
        }

        /// <summary>
        /// Payment capture
        /// </summary>
        /// <param name="payment">Payment information, <see cref="Payment"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Payment Capture(Payment payment, string idempotenceKey = null)
        {
            return Post<Payment>(payment, _apiUrl + payment.id + "/capture", idempotenceKey);
        }

        /// <summary>
        /// Query payment state
        /// </summary>
        /// <param name="payment">Payment information, <see cref="Payment"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Payment QueryPayment(Payment payment, string idempotenceKey = null)
        {
            return Post<Payment>(payment, _apiUrl + payment.id, idempotenceKey);
        }

        #region Async
#if !NET40
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly string _version = typeof(Client).Assembly.GetName().Version.ToString();

        /// <summary>
        /// Payment creation
        /// </summary>
        /// <param name="payment">Payment information, <see cref="NewPayment"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Payment"/></returns>
        public Task<Payment> CreatePaymentAsync(NewPayment payment, string idempotenceKey, CancellationToken cancellationToken)
            => PostAsync<Payment>(payment, _apiUrl, idempotenceKey, cancellationToken);

        /// <inheritdoc cref="CreatePaymentAsync(Yandex.Checkout.V3.NewPayment,string,System.Threading.CancellationToken)"/>
        public Task<Payment> CreatePaymentAsync(NewPayment payment, string idempotenceKey = null)
            => CreatePaymentAsync(payment, idempotenceKey, CancellationToken.None);

        /// <summary>
        /// Payment capture
        /// </summary>
        /// <param name="payment">Payment information, <see cref="Payment"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Payment"/></returns>
        public Task<Payment> CaptureAsync(Payment payment, string idempotenceKey, CancellationToken cancellationToken)
            => PostAsync<Payment>(payment, _apiUrl + payment.id + "/capture", idempotenceKey, cancellationToken);

        /// <inheritdoc cref="CaptureAsync(Yandex.Checkout.V3.Payment,string,System.Threading.CancellationToken)"/>
        public Task<Payment> CaptureAsync(Payment payment, string idempotenceKey = null)
            => CaptureAsync(payment, idempotenceKey, CancellationToken.None);

        /// <summary>
        /// Query payment state
        /// </summary>
        /// <param name="payment">Payment information, <see cref="Payment"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="Payment"/></returns>
        public Task<Payment> QueryPaymentAsync(Payment payment, string idempotenceKey, CancellationToken cancellationToken)
            => PostAsync<Payment>(payment, _apiUrl + payment.id, idempotenceKey, cancellationToken);
      
        /// <inheritdoc cref="QueryPaymentAsync(Yandex.Checkout.V3.Payment,string,System.Threading.CancellationToken)"/>
        public Task<Payment> QueryPaymentAsync(Payment payment, string idempotenceKey = null)
            => QueryPaymentAsync(payment, idempotenceKey, CancellationToken.None);

        private async Task<T> PostAsync<T>(object body, string url, string idempotenceKey, CancellationToken cancellationToken)
        {
            try
            {
                using (var request = CreateRequest(body, url, idempotenceKey))
                using (var response = await _httpClient.SendAsync(request, cancellationToken))
                {
                    var responceData = response.Content == null ? null : await response.Content.ReadAsStringAsync();

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new YandexCheckoutException((int) response.StatusCode,
                            string.IsNullOrEmpty(responceData) 
                                ? new Error {code = "unknown", description = "Unknown error"}
                                : JsonConvert.DeserializeObject<Error>(responceData));
                    }

                    return JsonConvert.DeserializeObject<T>(responceData);
                }
            }
            catch (HttpRequestException ex)
            {
                throw new YandexCheckoutException(ex.Message, ex);
            }

        }

        private T Post<T>(object body, string url, string idempotenceKey)
        {
            try
            {
                using (var request = CreateRequest(body, url, idempotenceKey))
                using (var response = _httpClient.SendAsync(request).Result)
                {
                    var responceData = response.Content?.ReadAsStringAsync().Result;

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new YandexCheckoutException((int) response.StatusCode,
                            string.IsNullOrEmpty(responceData) 
                                ? new Error {code = "unknown", description = "Unknown error"}
                                : JsonConvert.DeserializeObject<Error>(responceData));
                    }

                    return JsonConvert.DeserializeObject<T>(responceData);
                }
            }
            catch (HttpRequestException ex)
            {
                throw new YandexCheckoutException(ex.Message, ex);
            }

        }

        private HttpRequestMessage CreateRequest(object body, string url, string idempotenceKey)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8);

            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            
            request.Content = content;
            request.Headers.Add("Authorization", _authorization);
            request.Headers.Add("Idempotence-Key", idempotenceKey ?? Guid.NewGuid().ToString());
            
            if(!string.IsNullOrEmpty(_userAgent))
                request.Headers.UserAgent.Add(new ProductInfoHeaderValue(_userAgent, _version));

            return request;
        }
#else
        private T Post<T>(object body, string url, string idempotenceKey)
        {
            var request = CreateRequest<T>(body, url, idempotenceKey);

            using (WebResponse response = request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            using (var sr = new StreamReader(responseStream ?? throw new InvalidOperationException("Response stream is null.")))
            {
                string jsonResponse = sr.ReadToEnd();
                T info = JsonConvert.DeserializeObject<T>(jsonResponse);
                return info;
            }
        }

        private WebRequest CreateRequest<T>(object body, string url, string idempotenceKey)
        {
            var request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", _authorization);

            // Похоже, что этот заголовок обязателен, без него говорит (400) Bad Request.
            request.Headers.Add("Idempotence-Key", idempotenceKey ?? Guid.NewGuid().ToString());

            if (_userAgent != null)
            {
                ((HttpWebRequest) request).UserAgent = _userAgent;
            }

            var json = JsonConvert.SerializeObject(body);
            var postBytes = Encoding.UTF8.GetBytes(json);
            request.ContentLength = postBytes.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(postBytes, 0, postBytes.Length);
            }

            return request;
        }
#endif
        #endregion Async


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
                message = JsonConvert.DeserializeObject<Message>(jsonBody);
            }
            return message;
        }

        private static string ReadToEnd(Stream stream)
        {
            if (stream == null) return null;

            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
