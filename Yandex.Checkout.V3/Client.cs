using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace Yandex.Checkout.V3
{
    public class Client
    {
        private readonly string _userAgent;
        private readonly string _apiUrl;
        private readonly string _authorization;

        public Client(
            string shopId, 
            string secretKey,
            string apiUrl = "https://payment.yandex.net/api/v3/payments/",
            string userAgent = "Yandex.Checkout.V3 .NET Client")
        {
            _apiUrl = apiUrl;
            _userAgent = userAgent;
            _authorization = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(shopId + ":" + secretKey));
        }

        public Payment CreatePayment(NewPayment payment, string idempotenceKey = null)
        {
            return Post<Payment>(payment, _apiUrl, idempotenceKey);
        }

        public void Capture(Payment payment, string idempotenceKey = null)
        {
            Post<dynamic>(payment, _apiUrl + payment.id + "/capture", idempotenceKey);
        }

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
            if (requestHttpMethod == "POST"/* && requestContentType == "application/json; charset=UTF-8"*/)
            {
                message = JsonConvert.DeserializeObject<Message>(jsonBody);
            }
            return message;
        }

        public static string ReadToEnd(Stream stream)
        {
            if (stream == null) return null;

            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private T Post<T>(object body, string url, string idempotenceKey)
        {
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", _authorization);

            // Похоже, что этот заголовок обязателен, без него говорит (400) Bad Request.
            request.Headers.Add("Idempotence-Key", idempotenceKey ?? Guid.NewGuid().ToString());

            if (_userAgent != null)
            {
                ((HttpWebRequest)request).UserAgent = _userAgent;
            }

            string json = JsonConvert.SerializeObject(body);
            byte[] postBytes = Encoding.UTF8.GetBytes(json);
            request.ContentLength = postBytes.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(postBytes, 0, postBytes.Length);
            }

            using (WebResponse response = request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            using (var sr = new StreamReader(responseStream ?? throw new InvalidOperationException("Response stream is null.")))
            {
                string jsonResponse = sr.ReadToEnd();
                T info = JsonConvert.DeserializeObject<T>(jsonResponse);
                return info;
            }
        }
    }
}
