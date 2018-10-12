using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

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

        public static T DeserializeObject<T>(string data) => JsonConvert.DeserializeObject<T>(data, SerializerSettings);

        public static string SerializeObject(object value) => JsonConvert.SerializeObject(value, SerializerSettings);

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
            return Query<Payment>("POST", payment, _apiUrl, idempotenceKey);
        }

        /// <summary>
        /// Payment capture
        /// </summary>
        /// <param name="payment">Payment information, <see cref="Payment"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Payment Capture(Payment payment, string idempotenceKey = null)
        {
            return Query<Payment>("POST", payment, _apiUrl + payment.Id + "/capture", idempotenceKey);
        }

        /// <summary>
        /// Query payment state
        /// </summary>
        /// <param name="payment">Payment information, <see cref="Payment"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Payment QueryPayment(Payment payment, string idempotenceKey = null)
        {
            return Query<Payment>("GET", payment, _apiUrl + payment.Id, idempotenceKey);
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
            => QueryAsync<Payment>(HttpMethod.Post.Method, payment, _apiUrl, idempotenceKey, cancellationToken);

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
            => QueryAsync<Payment>(HttpMethod.Post.Method, null, _apiUrl + payment.Id + "/capture", idempotenceKey, cancellationToken);

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
            => QueryAsync<Payment>(HttpMethod.Get.Method, payment, _apiUrl + payment.Id, idempotenceKey, cancellationToken);
      
        /// <inheritdoc cref="QueryPaymentAsync(Yandex.Checkout.V3.Payment,string,System.Threading.CancellationToken)"/>
        public Task<Payment> QueryPaymentAsync(Payment payment, string idempotenceKey = null)
            => QueryPaymentAsync(payment, idempotenceKey, CancellationToken.None);

        private async Task<T> QueryAsync<T>(string method, object body, string url, string idempotenceKey, CancellationToken cancellationToken)
        {
            using (var request = CreateRequest(method, body, url, idempotenceKey))
            using (var response = await _httpClient.SendAsync(request, cancellationToken))
            {
                var responceData = response.Content == null 
                    ? null 
                    : await response.Content.ReadAsStringAsync();

                return ProcessResponce<T>(response, responceData);
            }
        }

        private T Query<T>(string method, object body, string url, string idempotenceKey)
        {
            using (var request = CreateRequest(method, body, url, idempotenceKey))
            using (var response = _httpClient.SendAsync(request).Result)
            {
                var responceData = response.Content?.ReadAsStringAsync().Result;

                return ProcessResponce<T>(response, responceData);
            }
        }

        private static T ProcessResponce<T>(HttpResponseMessage response, string responceData)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new YandexCheckoutException((int) response.StatusCode,
                    string.IsNullOrEmpty(responceData)
                        ? new Error {Code = "unknown", Description = "Unknown error"}
                        : DeserializeObject<Error>(responceData));
            }

            return DeserializeObject<T>(responceData);
        }

        private HttpRequestMessage CreateRequest(string method, object body, string url, string idempotenceKey)
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(url);
            request.Method = new HttpMethod(method);
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
#else
        private T Query<T>(string method, object body, string url, string idempotenceKey)
        {
            var request = CreateRequest(method, body, url, idempotenceKey);
            using (var response = (HttpWebResponse)request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            using (var sr = new StreamReader(responseStream ?? throw new InvalidOperationException("Response stream is null.")))
            {
                var responceData = sr.ReadToEnd();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new YandexCheckoutException((int) response.StatusCode,
                        string.IsNullOrEmpty(responceData) 
                            ? new Error {Code = "unknown", Description = "Unknown error"}
                            : DeserializeObject<Error>(responceData));
                }
                T info = DeserializeObject<T>(responceData);
                return info;
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

            var json = SerializeObject(body);
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
                message = DeserializeObject<Message>(jsonBody);
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
