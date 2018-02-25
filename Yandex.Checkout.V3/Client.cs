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
        private readonly string _shopId;
        private readonly string _secretKey;
        private readonly string _apiUrl;

        public Client(
            string shopId, 
            string secretKey,
            string apiUrl = "https://payment.yandex.net/api/v3/payments/",
            string userAgent = ".NET API Yandex.Checkout.V3")
        {
            _shopId = shopId;
            _secretKey = secretKey;
            _apiUrl = apiUrl;
            _userAgent = userAgent;
        }

        public Payment CreatePayment(NewPayment payment, string idempotenceKey)
        {
            return Post<Payment>(payment, _apiUrl, idempotenceKey);
        }

        public void Capture(Payment payment)
        {
            Post<dynamic>(payment, _apiUrl + payment.id + "/capture", Guid.NewGuid().ToString());
        }

        public Message ParseMessage(string requestHttpMethod, string requestContentType, Stream requestInputStream)
        {
            Message message = null;
            if (requestHttpMethod == "POST" && requestContentType == "application/json; charset=UTF-8")
            {
                string json;
                using (var reader = new StreamReader(requestInputStream))
                {
                    json = reader.ReadToEnd();
                }

                message = JsonConvert.DeserializeObject<Message>(json);
            }
            return message;
        }

        private T Post<T>(object body, string url, string idempotenceKey)
        {
            string json = JsonConvert.SerializeObject(body);
            byte[] postBytes = Encoding.UTF8.GetBytes(json);
            string base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(_shopId + ":" + _secretKey));

            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.Headers.Add("Authorization", "Basic " + base64String);
            request.ContentType = "application/json";
            request.Headers.Add("Idempotence-Key", idempotenceKey);

            ((HttpWebRequest)request).UserAgent = _userAgent;
            request.ContentLength = postBytes.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(postBytes, 0, postBytes.Length);
            }

            using (WebResponse response = request.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(responseStream ?? throw new InvalidOperationException("Response is null.")))
                    {
                        string jsonResponse = sr.ReadToEnd();
                        T info = JsonConvert.DeserializeObject<T>(jsonResponse);
                        return info;
                    }
                }
            }
        }
    }
}
