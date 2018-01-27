using Newtonsoft.Json;

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;


namespace TestPageAPI
{


    public partial class ycheckout : System.Web.UI.Page
    {
        // string __json = "";
        class Amount
        {
            public string value { get; set; }
            public string currency { get; set; }
        }

        class Recipient
        {
            public string type { get; set; }
            public string confirmation_url { get; set; }
            public string return_url { get; set; }
        }

        class Confirmation_Return
        {
            public string type { get; set; }
            public string return_url { get; set; }
        }

        class Confirmation_Result
        {
            public string type { get; set; }
            public string confirmation_url { get; set; }
        }

        class Pay 
        {
            public bool capture { get; set; }
            public Amount amount = new Amount();
            public Confirmation_Return confirmation = new Confirmation_Return();

        }

        
        class Pay_result
        {
            public string id { get; set; }
            public string status{ get; set; }
            public string paid { get; set; }
            public string created_at { get; set; }

            public Amount amount = new Amount();
            public Confirmation_Result confirmation = new Confirmation_Result();
            public Recipient recipient = new Recipient();

        }


        private static ManualResetEvent allDone = new ManualResetEvent(false);

        protected void Page_Load(object sender, EventArgs e)
        {

            System.Text.StringBuilder displayValues = new System.Text.StringBuilder();
            System.Collections.Specialized.NameValueCollection
                postedValues = Request.Form;
            String nextKey;
            for (int i = 0; i < postedValues.AllKeys.Length - 1; i++)
            {
                nextKey = postedValues.AllKeys[i];
                if (nextKey.Substring(0, 2) != "__")
                {
                    displayValues.Append("<br>");
                    displayValues.Append(nextKey);
                    displayValues.Append(" = ");
                    displayValues.Append(postedValues[i]);
                }
            }

            if (displayValues.ToString() != string.Empty)
            {
                LabelPayInfo.Text = displayValues.ToString();
            }

            if (IsPostBack)
            {

            }

        }

        protected void submit_YandexPay_Click(object sender, EventArgs e)
        {

            if (sum.Text!=null)
            {
                SendPost(sum.Text);
            }
        }
        

        void SendPost(string sum)
        {



            var _json_Pay = new Pay() {capture = true,  amount = new Amount() { value = sum, currency="RUB" },
                                       confirmation = new Confirmation_Return() { type= "redirect", return_url = "https://apiyandexkassa.azurewebsites.net/ycheckout.aspx" } };


            string json = JsonConvert.SerializeObject(_json_Pay);

                       
            byte[] postBytes = Encoding.UTF8.GetBytes(json);
            var url = "https://payment.yandex.net/api/v3/payments";

            //body = Encoding.UTF8.GetBytes(_json);

            string _shopid = "501156";
            string _scid = "test_iG5KnuKtiyOKps2FkZU8cuUYcVz5WtV5s3Bhlr9DCcE";
            string _encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(_shopid + ":" + _scid));

            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;

            //CredentialCache credentialCache = new CredentialCache();
            //credentialCache.Add(new System.Uri(url), "Basic", new NetworkCredential(_shopid, _scid));

            WebRequest _request = WebRequest.Create(url);
            _request.Method = "POST";


            //_request.StatusCode == 401

            //_request.Credentials = credentialCache;    NTAxMTU2OnRlc3RfaUc1S251S3RpeU9LcHMyRmtaVThjdVVZY1Z6NVd0VjVzM0JobHI5RENjRQ==
            _request.Headers.Add("Authorization", "Basic NTAxMTU2OnRlc3RfQXMwT09OUm4xU3N2RnIwSVZseFVMeHN0NURCSW9XaV90eVZhZXpTUlRFSQ==");
                //"Basic " + _encoded);
            //_request.Timeout = 20000;
            //_request.PreAuthenticate = true;
            _request.ContentType = "application/json";
            _request.Headers.Add("Idempotence-Key", Guid.NewGuid().ToString());
            _request.ContentLength = postBytes.Length;



            ((HttpWebRequest)_request).UserAgent = ".NET Framework CMS";


            //_request.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), _request);

            //  allDone.WaitOne();

            using (var s = _request.GetRequestStream())
            {
                //using (var sw = new System.IO.StreamWriter(s))
                //    sw.Write(_json);
                s.Write(postBytes, 0, postBytes.Length);
            }

            using ( var s = _request.GetResponse())
            {
                using (var responseStream = s.GetResponseStream())
                {
                    using (var sr = new System.IO.StreamReader(responseStream))
                    {
                        var jsonResponse = sr.ReadToEnd();
                        //System.Diagnostics.Debug.WriteLine(String.Format("Response: {0}", jsonResponse));

                        Pay_result _info = JsonConvert.DeserializeObject<Pay_result>(jsonResponse);

                        string _url = "https://money.yandex.ru/api-pages/v2/payment-confirm/epl?orderId=" + _info.id;


                        //Server.Transfer(_url, true);

                        Response.Redirect(_url);
                    }

                }
            }


        }

        private static void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

            using (Stream postStream = request.EndGetRequestStream(asynchronousResult))
            {
                string postData = "";


                byte[] byteArray = Encoding.UTF8.GetBytes(postData);


                postStream.Write(byteArray, 0, postData.Length);

            }


            request.BeginGetResponse(new AsyncCallback(GetResponseCallback), request);
        }

        private static void GetResponseCallback(IAsyncResult asynchronousResult)
        {

            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

            using (HttpWebResponse _response = (HttpWebResponse)request.EndGetResponse(asynchronousResult))
            {
                using (Stream _streamResponse = _response.GetResponseStream())
                {
                    using (StreamReader _streamRead = new StreamReader(_streamResponse))
                    {
                        string responseString = _streamRead.ReadToEnd();
                    } 
                }
            }    

            allDone.Set();
        }



      

    }
}