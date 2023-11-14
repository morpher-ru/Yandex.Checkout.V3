using System;
using System.Globalization;
using System.IO;
using System.Net;
using Yandex.Checkout.V3;

namespace AspNetSample
{
    public partial class ycheckout : System.Web.UI.Page
    {
        // It's best to have only one instance of Yandex.Checkout.V3.Client for the lifetime of an application
        // (same as with HttpClient). Hence using static.
        static readonly Client _client = new Client("501156", "test_As0OONRn1SsvFr0IVlxULxst5DBIoWi_tyVaezSRTEI");

        protected void submit_YandexPay_Click(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            File.Delete(Server.MapPath("log.txt"));

            // 1. Создайте платеж и получите ссылку для оплаты
            decimal amount = decimal.Parse(sum.Text, CultureInfo.InvariantCulture.NumberFormat);
            var newPayment = new NewPayment
            {
                Amount = new Amount { Value = amount, Currency = "RUB" },
                Confirmation = new Confirmation { Type = ConfirmationType.Redirect, ReturnUrl = Request.Url.AbsoluteUri }
            };
            Payment payment = _client.CreatePayment(newPayment);
            
            // 2. Перенаправьте пользователя на страницу оплаты
            string url = payment.Confirmation.ConfirmationUrl;
            Response.Redirect(url);
        }

        // 3. Дождитесь уведомления о платеже
        protected void Page_Load(object sender, EventArgs e)
        {
            // Чтобы получить это уведомление, нужно указать адрес этой страницы
            // в настройках магазина (https://kassa.yandex.ru/my/tunes).
            try
            {
                Log($"Page_Load: Request.HttpMethod={Request.HttpMethod}, Request.ContentType={Request.ContentType}, Request.InputStream has {Request.InputStream.Length} bytes");
                Message message = Client.ParseMessage(Request.HttpMethod, Request.ContentType, Request.InputStream);
                Payment payment = message?.Object;
                if (message?.Event == Event.PaymentWaitingForCapture && payment.Paid)
                {
                    Log($"Got message: payment.id={payment.Id}, payment.paid={payment.Paid}");

                    // 4. Подтвердите готовность принять платеж
                    _client.CapturePayment(payment.Id);
                }
            }
            catch (Exception exception)
            {
                Log(exception.ToString());
            }
        }

        void Log(string msg)
        {
            File.AppendAllLines(Server.MapPath("log.txt"), new[] { $"{DateTime.UtcNow} {msg}" });
        }
    }
}
