using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestPageAPI
{
    public partial class ConfirmPay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

                NameValueCollection coll;
                coll = Request.Headers;

                foreach (string key in HttpContext.Current.Request.Form.AllKeys)
                {
                    /// Response.WriteLine(Request.Form[key]);
                    /// 
                    PayInfo.Text += key.ToString() + " ";


                }


            


            //using (var _getresp = Request.GetResponse())
            //{
            //    using (var responseStream = _getresp.GetResponseStream())
            //    {
            //        using (var sr = new StreamReader(responseStream))
            //        {
            //            var jsonResponse = sr.ReadToEnd();

            //            Pay_Result _info = JsonConvert.DeserializeObject<Pay_Result>(jsonResponse);

            //            ConfirmationUrl = _info.confirmation.confirmation_url;

            //        }

            //    }
            //}





        }
    }
}