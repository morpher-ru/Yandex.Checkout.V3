using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
        /// <summary>
        /// Payment creation
        /// </summary>
        /// <param name="payment">Payment information, <see cref="NewPayment"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Task<Payment> CreatePaymentAsync(NewPayment payment, string idempotenceKey = null)
            => PostAsync<Payment>(payment, _apiUrl, idempotenceKey);

        /// <summary>
        /// Payment capture
        /// </summary>
        /// <param name="payment">Payment information, <see cref="Payment"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Task<Payment> CaptureAsync(Payment payment, string idempotenceKey = null)
            => PostAsync<Payment>(payment, _apiUrl + payment.id + "/capture", idempotenceKey);

        /// <summary>
        /// Query payment state
        /// </summary>
        /// <param name="payment">Payment information, <see cref="Payment"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Task<Payment> QueryPaymentAsync(Payment payment, string idempotenceKey = null)
            => PostAsync<Payment>(payment, _apiUrl + payment.id, idempotenceKey);

        private async Task<T> PostAsync<T>(object body, string url, string idempotenceKey)
        {
            var request = CreateRequest<T>(body, url, idempotenceKey);

            using (var response = await request.GetResponseAsync())
            using (var responseStream = response.GetResponseStream())
            using (var sr = new StreamReader(responseStream ?? throw new InvalidOperationException("Response stream is null.")))
            {
                string jsonResponse = await sr.ReadToEndAsync();
                T info = JsonConvert.DeserializeObject<T>(jsonResponse);
                return info;
            }
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
    }
}
