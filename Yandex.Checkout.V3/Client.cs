using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace Yandex.Checkout.V3
{
    public class Client
    {
        public const string url_yandex_pay = "https://payment.yandex.net/api/v3/payments/";

        public string ConfirmationUrl { get; private set; }

        private WebRequest _request = null;
        private string _shopid { get; set; }
        private string _scid { get; set; }

        public Client(string shopId, string secretKey, string useragent = ".NET API Yandex.Checkout.V3")
        {
            _shopid = shopId;
            _scid = secretKey;
            InitializeComponent(useragent, url_yandex_pay);

        }

        private void InitializeComponent(string useragent, string url)
        {
            string _encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(_shopid + ":" + _scid));

            _request = WebRequest.Create(url);
            _request.Method = "POST";
            _request.Headers.Add("Authorization", "Basic " + _encoded);
            _request.ContentType = "application/json";
            _request.Headers.Add("Idempotence-Key", Guid.NewGuid().ToString());

            ((HttpWebRequest)_request).UserAgent = useragent;
        }

        private T GetJsonResponse<T>()
        {
            using (var _getresp = _request.GetResponse())
            {
                using (var responseStream = _getresp.GetResponseStream())
                {
                    using (var sr = new StreamReader(responseStream))
                    {
                        var jsonResponse = sr.ReadToEnd();
                        T _info = JsonConvert.DeserializeObject<T>(jsonResponse);
                        return _info;
                    }

                }
            }
        }

        public Client CreatePayment(float sum, string currency, string return_url, bool capture = false)
        {

            var _json_Pay = new Pay()
            {
                 capture = capture,
                amount = new Amount() { value = sum, currency = currency },
                confirmation = new Confirmation_Return() { type = "redirect", return_url = return_url }
            };


            string json = JsonConvert.SerializeObject(_json_Pay);
            byte[] postBytes = Encoding.UTF8.GetBytes(json);
            _request.ContentLength = postBytes.Length;



            using (var _stream = _request.GetRequestStream())
            {
                _stream.Write(postBytes, 0, postBytes.Length);
            }

            ConfirmationUrl = GetJsonResponse<Pay_Result>().confirmation.confirmation_url;
   

            return this;
        }



        public Client PaymentCapture (string _json,string useragent = ".NET API Yandex.Checkout.V3")
        {
          
            Waiting_For_Capture _info = JsonConvert.DeserializeObject<Waiting_For_Capture>(_json);
            string tmp = _info.object_pay.id;

            if (_info.event_status == "payment.waiting_for_capture")
            {
                if (_info.object_pay.id != null && _info.object_pay.paid == true)
                {
                   InitializeComponent(useragent, url_yandex_pay + tmp + "/capture");


                    var amount = new Amount() { value = _info.object_pay.amount.value, currency = _info.object_pay.amount.currency };
                    string json_fin = JsonConvert.SerializeObject(amount);
                    byte[] postBytes = Encoding.UTF8.GetBytes(json_fin);
                    _request.ContentLength = postBytes.Length;

                    using (var _stream = _request.GetRequestStream())
                    {
                        _stream.Write(postBytes, 0, postBytes.Length);
                    }
                }
            }

            dynamic succeeded = GetJsonResponse<dynamic>();


            /// TD
            /// 
            ///
            return this;

        }


    }
}
