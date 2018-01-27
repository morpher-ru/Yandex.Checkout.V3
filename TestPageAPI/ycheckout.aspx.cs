
using System;
using System.Globalization;


namespace TestPageAPI
{


    public partial class ycheckout : System.Web.UI.Page
    {
   
      
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
   
                float fsum = float.Parse(sum.Text, CultureInfo.InvariantCulture.NumberFormat);
                string _url = new Yandex.Checkout.V3.Client("501156", "test_As0OONRn1SsvFr0IVlxULxst5DBIoWi_tyVaezSRTEI")
                                         .CreatePayment(fsum, "RUB", "https://morpher.ru/ycheckout.aspx").ConfirmationUrl;
                Response.Redirect(_url);
            }
        }
        
      

    }
}