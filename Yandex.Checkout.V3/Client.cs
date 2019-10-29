using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Yandex.Checkout.V3
{
    /// <summary>
    /// Yamdex.Checkout HTTP API client
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
            string apiUrl = "https://payment.yandex.net/api/v3/",
            string userAgent = "Yandex.Checkout.V3 .NET Client")
        {
            if (string.IsNullOrWhiteSpace(shopId))
                throw new ArgumentNullException(nameof(shopId));
            if (string.IsNullOrWhiteSpace(secretKey))
                throw new ArgumentNullException(nameof(shopId));
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
        /// Payment creation
        /// </summary>
        /// <param name="payment">Payment information, <see cref="NewPayment"/></param>
        /// <param name="idempotenceKey">Idempotence key, use <value>null</value> to generate a new one</param>
        /// <returns><see cref="Payment"/></returns>
        public Payment CreatePayment(NewPayment payment, string idempotenceKey = null)
            => Query<Payment>("POST", payment, "payments/", idempotenceKey);

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
            => Query<Payment>("POST", payment, $"payments/{payment.Id}/capture", idempotenceKey);

        /// <summary>
        /// Query payment state
        /// </summary>
        /// <param name="id">Payment id, <see cref="Payment.Id"/></param>
        /// <returns><see cref="Payment"/></returns>
        public Payment GetPayment(string id)
            => Query<Payment>("GET", null, $"payments/{id}", null);

        /// <summary>
        /// Payment cancelation
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
        /// <returns><see cref="NewRefund"/></returns>
        public Refund CreateRefund(NewRefund refund, string idempotenceKey = null)
            => Query<Refund>("POST", refund, "refunds", idempotenceKey);

        /// <summary>
        /// Query refund
        /// </summary>
        /// <param name="id">Refund id</param>
        /// <returns><see cref="NewRefund"/></returns>
        public Refund GetRefund(string id)
            => Query<Refund>("GET", null, $"refunds/{id}", null);

        #endregion Sync

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
                message = Serializer.DeserializeObject<Message>(jsonBody);
            }
            return message;
        }

        #endregion Parse

        #region Helpers

        private static readonly HashSet<HttpStatusCode> KnownErrors = new HashSet<HttpStatusCode>
        {
            HttpStatusCode.BadRequest, 
            HttpStatusCode.Unauthorized, 
            HttpStatusCode.Forbidden, 
            HttpStatusCode.NotFound, 
            (HttpStatusCode) 429, // Too Many Requests
            HttpStatusCode.InternalServerError
        };

        internal static readonly string ApplicationJson = "application/json";

        internal static T ProcessResponse<T>(HttpStatusCode statusCode, string responseData, string contentType)
        {
            if (statusCode != HttpStatusCode.OK)
            {
                throw new YandexCheckoutException(statusCode,
                    string.IsNullOrEmpty(responseData) || ! KnownErrors.Contains(statusCode) || !contentType.StartsWith(ApplicationJson)
                        ? new Error {Code = statusCode.ToString(), Description = statusCode.ToString()}
                        : Serializer.DeserializeObject<Error>(responseData));
            }

            return Serializer.DeserializeObject<T>(responseData);
        }

        private T Query<T>(string method, object body, string url, string idempotenceKey)
        {
            HttpWebRequest request = CreateRequest(method, body, url, idempotenceKey ?? Guid.NewGuid().ToString());
            using var response = (HttpWebResponse)request.GetResponse();
            using Stream responseStream = response.GetResponseStream();
            using var sr = new StreamReader(responseStream ?? throw new InvalidOperationException("Response stream is null."));
            string responseData = sr.ReadToEnd();
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

            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        #endregion Helpers
    }
}
