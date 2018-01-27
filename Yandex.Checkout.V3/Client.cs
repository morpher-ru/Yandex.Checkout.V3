using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace Yandex.Checkout.V3
{
    public class Client
    {
        public const string url = "https://payment.yandex.net/api/v3/payments";

        public string ConfirmationUrl { get; private set; }

        private WebRequest _request = null;
        private string _shopid { get; set; }
        private string _scid { get; set; }

        public Client(string shopId, string secretKey, string useragent = ".NET API Yandex.Checkout.V3")
        {
            _shopid = shopId;
            _scid = secretKey;

            string _encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(_shopid + ":" + _scid));
           
            _request = WebRequest.Create(url);
            _request.Method = "POST";
            _request.Headers.Add("Authorization", "Basic " + _encoded);
            _request.ContentType = "application/json";
            _request.Headers.Add("Idempotence-Key", Guid.NewGuid().ToString());

            ((HttpWebRequest)_request).UserAgent = useragent;

        }

        public Client CreatePayment(float sum, string currency, string return_url )
        {

            var _json_Pay = new Pay()
            {
                capture = true,
                amount = new Amount() { value = sum, currency = currency },
                confirmation = new Confirmation_Return() { type = "redirect", return_url = return_url }
            };


            string json = JsonConvert.SerializeObject(_json_Pay);
            byte[] postBytes = Encoding.UTF8.GetBytes(json);
            _request.ContentLength = postBytes.Length;

 

            using (var _stream = _request.GetRequestStream())
            {
                //using (var sw = new System.IO.StreamWriter(s))
                //    sw.Write(_json);
                _stream.Write(postBytes, 0, postBytes.Length);
            }


            using (var _getresp = _request.GetResponse())
            {
                using (var responseStream = _getresp.GetResponseStream())
                {
                    using (var sr = new StreamReader(responseStream))
                    {
                        var jsonResponse = sr.ReadToEnd();

                        Pay_Result _info = JsonConvert.DeserializeObject<Pay_Result>(jsonResponse);

                        ConfirmationUrl = _info.confirmation.confirmation_url;
                        
                    }

                }
            }

            return this;
        }
    }
}
