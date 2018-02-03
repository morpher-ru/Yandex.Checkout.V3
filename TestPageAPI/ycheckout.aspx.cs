
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Web;
using Yandex.Checkout.V3;

namespace TestPageAPI
{


    public partial class ycheckout : System.Web.UI.Page
    {
   
      
        protected void Page_Load(object sender, EventArgs e)
        {



            if (Request.HttpMethod == "POST" && Request.ContentType == "application/json; charset=UTF-8")
            {
               string json;
                using (var reader = new StreamReader(Request.InputStream))
                {
                    json = reader.ReadToEnd();
                }


                var _url = new Yandex.Checkout.V3.Client("501156", "test_As0OONRn1SsvFr0IVlxULxst5DBIoWi_tyVaezSRTEI")
                    .PaymentCapture(json);
                                     
            }
        }

   

        protected void submit_YandexPay_Click(object sender, EventArgs e)
        {

            if (sum.Text!=null)
            {

               // string urlpay = "https://apiyandexkassa.azurewebsites.net/ConfirmPay.aspx";
                string urlpay = "https://apiyandexkassa.azurewebsites.net/ycheckout.aspx";


                float fsum = float.Parse(sum.Text, CultureInfo.InvariantCulture.NumberFormat);
                string _url = new Yandex.Checkout.V3.Client("501156", "test_As0OONRn1SsvFr0IVlxULxst5DBIoWi_tyVaezSRTEI")
                                         .CreatePayment(fsum, "RUB", urlpay).ConfirmationUrl;
                Response.Redirect(_url);
            }
        }
        
      

    }
}