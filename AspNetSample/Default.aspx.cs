using System;
using System.Globalization;
using System.IO;
using Yandex.Checkout.V3;

namespace AspNetSample
{
    public partial class ycheckout : System.Web.UI.Page
    {
        readonly Client _client = new Client("501156", "test_As0OONRn1SsvFr0IVlxULxst5DBIoWi_tyVaezSRTEI");

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Log($"Page_Load: Request.HttpMethod={Request.HttpMethod}, Request.ContentType={Request.ContentType}, Request.InputStream has {Request.InputStream.Length} bytes");
                Message message = Client.ParseMessage(Request.HttpMethod, Request.ContentType, Request.InputStream);
                Payment payment = message?.@object;
                if (message?.@event == Event.PaymentWaitingForCapture && payment.id != default(Guid) && payment.paid)
                {
                    Log($"Got message: payment.id={payment.id}, payment.paid={payment.paid}");
                    _client.Capture(payment);
                }
            }
            catch (Exception exception)
            {
                Log(exception.ToString());
            }
        }

        protected void submit_YandexPay_Click(object sender, EventArgs e)
        {
            File.Delete(Server.MapPath("log.txt"));

            decimal amount = decimal.Parse(sum.Text, CultureInfo.InvariantCulture.NumberFormat);
            var idempotenceKey = Guid.NewGuid().ToString();
            var newPayment = new NewPayment
            {
                amount = new Amount { value = amount, currency = "RUB" },
                confirmation = new Confirmation { type = ConfirmationType.redirect, return_url = Request.Url.AbsoluteUri }
            };
            Payment payment = _client.CreatePayment(newPayment, idempotenceKey);
            string url = payment.confirmation.confirmation_url;
            Response.Redirect(url);
        }

        void Log(string msg)
        {
            File.AppendAllLines(Server.MapPath("log.txt"), new[] { $"{DateTime.UtcNow} {msg}" });
        }
    }
}
