using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yandex.Checkout.V3.Demo.Pages.BaseModels;

namespace Yandex.Checkout.V3.Demo.Pages
{
    public class ConfirmAsyncModel : ConfirmModel
    {
        public async Task OnGetAsync(int id)
        {
            var data = PaymentStorage.Payments[id];
            Id = id;

            var payment = await data.Client.QueryPaymentAsync(data.Payment.Id);
            ProcessPayment(payment);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var data = PaymentStorage.Payments[Id];

            switch (Action)
            {
                case "Confirm":
                    var capture = await data.Client.CaptureAsync(data.Payment.Id);
                    Payment = Client.SerializeObject(capture);
                    break;
                case "Cancel":
                    Payment = Client.SerializeObject(await data.Client.CancelAsync(data.Payment.Id));
                    break;
                case "Return":
                    Payment = Client.SerializeObject(
                        await data.Client.RefoundAsync(new NewRefound() { Amount = data.Payment.Amount, PaymentId = data.Payment.Id}));
                    break;
                default:
                    throw new InvalidOperationException(Action);
            }

            return RedirectToPage("FinishAsync", new {Id = Id});
        }
    }
}
