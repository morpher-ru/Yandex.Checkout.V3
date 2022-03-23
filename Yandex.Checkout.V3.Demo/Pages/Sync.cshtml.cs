using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;
using Yandex.Checkout.V3.Demo.Pages.BaseModels;

namespace Yandex.Checkout.V3.Demo.Pages
{
    public class SyncModel : NewPaymentModel
    {
        public IActionResult OnPost()
        {
            var client = new Client(ShopId, SecretKey);
            var id = PaymentStorage.GetNextId();

            var url = Request.GetUri();
            var redirect = $"{url.Scheme}://{url.Authority}/ConfirmSync?id={id}";

            var data = client.CreatePayment(
                new NewPayment
                {
                    Amount = new Amount
                    {
                        Value = Amount,
                    },
                    Confirmation = new Confirmation
                    {
                        Type = ConfirmationType.Redirect,
                        ReturnUrl = redirect
                    },
                    Description = "Order"
                });

            PaymentStorage.Payments[id] = new QueryData {Client = client, AsyncClient = client.MakeAsync(), Payment = data};

            return Redirect(data.Confirmation.ConfirmationUrl);
        }

    }
}
