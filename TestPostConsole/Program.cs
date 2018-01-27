using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace TestPostConsole
{
    class Program
    {

        public class MyWebRequest
        {
            private WebRequest request;
            private Stream dataStream;

            private string status;

            public String Status
            {
                get
                {
                    return status;
                }
                set
                {
                    status = value;
                }
            }

            public MyWebRequest(string url)
            {
                // Create a request using a URL that can receive a post.

                request = WebRequest.Create(url);
            }

            public MyWebRequest(string url, string method)
                : this(url)
            {

                if (method.Equals("GET") || method.Equals("POST"))
                {
                    // Set the Method property of the request to POST.
                    request.Method = method;
                }
                else
                {
                    throw new Exception("Invalid Method Type");
                }
            }

            public MyWebRequest(string url, string method, string data)
                : this(url, method)
            {

                // Create POST data and convert it to a byte array.
                string postData = data;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";

                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;

                // Get the request stream.
                dataStream = request.GetRequestStream();

                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);

                // Close the Stream object.
                dataStream.Close();

            }

            public string GetResponse()
            {
                // Get the original response.
                WebResponse response = request.GetResponse();

                this.Status = ((HttpWebResponse)response).StatusDescription;

                // Get the stream containing all content returned by the requested server.
                dataStream = response.GetResponseStream();

                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);

                // Read the content fully up to the end.
                string responseFromServer = reader.ReadToEnd();

                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();

                return responseFromServer;
            }

        }


        


        static void Main(string[] args)
        {


            //////create the constructor with post type and few data
            ////MyWebRequest myRequest = new MyWebRequest("https://payment.yandex.net/api/v3/payments", "POST", "Idempotence-Key=test_iG5KnuKtiyOKps2FkZU8cuUYcVz5WtV5s3Bhlr9DCcE & value=10 &currency=RUB");
            //////show the response string on the console screen.
            ////Console.WriteLine(myRequest.GetResponse());
            ////Console.ReadKey();
            //////var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://payment.yandex.net/api/v3/payments");
            //////httpWebRequest.ContentType = "application/json";
            //////httpWebRequest.Method = "POST";

            //////using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            //////{
            //////    string json = "{\"user\":\"test\"," +
            //////                  "\"password\":\"bla\"}";

            //////    streamWriter.Write(json);
            //////    streamWriter.Flush();
            //////    streamWriter.Close();
            //////}

            //////var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            //////using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            //////{
            //////    var result = streamReader.ReadToEnd();
            ////}

            ////   JsonConvert

            //var user = new User()
            //{
            //    Id = "404",
            //    Email = "chernikov@gmail.com",
            //    UserName = "rollinx",
            //    Name = "Andrey",
            //    FirstName = "Andrey",
            //    MiddleName = "Alexandrovich",
            //    LastName = "Chernikov",
            //    Gender = "M"
            //};

            //var jsonUser = JsonConvert.SerializeObject(user);
            //System.Console.Write(jsonUser);

            //System.Console.ReadLine();



            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://payment.yandex.net/api/v3/payments");

            //JArray array = new JArray();
            //using (var twitpicResponse = (HttpWebResponse)request.GetResponse())
            //{

            //    using (var reader = new StreamReader(twitpicResponse.GetResponseStream()))
            //    {
            //        JavaScriptSerializer js = new JavaScriptSerializer();
            //        var objText = reader.ReadToEnd();

            //        JObject joResponse = JObject.Parse(objText);
            //        JObject result = (JObject)joResponse["result"];
            //        array = (JArray)result["Detail"];
            //        string statu = array[0]["dlrStat"].ToString();
            //    }

            ////}
            //string url = "https://payment.yandex.net/api/v3/payments";
            //var body = Encoding.UTF8.GetBytes(json);

            //var request = (HttpWebRequest)WebRequest.Create(url);

            //request.Method = "POST";
            //request.ContentType = "application/json";
            //request.ContentLength = body.Length;

            //using (Stream stream = request.GetRequestStream())
            //{
            //    stream.Write(body, 0, body.Length);
            //    stream.Close();
            //}

            //using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            //{
            //    response.Close();
            //}


            String username = "501156";
            String password = "test_iG5KnuKtiyOKps2FkZU8cuUYcVz5WtV5s3Bhlr9DCcE";
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));

            string url = "https://payment.yandex.net/api/v3/payments";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            CredentialCache credentialCache = new CredentialCache();
            credentialCache.Add(new System.Uri(url), "Basic", new NetworkCredential(username, password));

            WebRequest request = WebRequest.Create(url);
            request.Credentials = credentialCache;
            request.PreAuthenticate = true;

            Console.WriteLine(encoded);
            Console.ReadKey();

          //  httpWebRequest.Headers.Add("Authorization", "Basic " + encoded);
        }
    }
}
