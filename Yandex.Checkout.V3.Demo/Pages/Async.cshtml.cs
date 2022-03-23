using System.Threading.Tasks;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;
using Yandex.Checkout.V3.Demo.Pages.BaseModels;

namespace Yandex.Checkout.V3.Demo.Pages
{
    public class AsyncModel : NewPaymentModel
    {
        public async Task<IActionResult> OnPostAsync()
        {
            var client = new Client(ShopId, SecretKey).MakeAsync();
            var id = PaymentStorage.GetNextId();

            var url = Request.GetUri();
            var redirect = $"{url.Scheme}://{url.Authority}/ConfirmAsync?id={id}";

            var data = await client.CreatePaymentAsync(
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

            PaymentStorage.Payments[id] = new QueryData {AsyncClient = client, Payment = data};

            return Redirect(data.Confirmation.ConfirmationUrl);
        }
    }
}
