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

            var payment = await data.AsyncClient.GetPaymentAsync(data.Payment.Id);
            ProcessPayment(payment);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var data = PaymentStorage.Payments[Id];

            switch (Action)
            {
                case "Confirm":
                    var capture = await data.AsyncClient.CapturePaymentAsync(data.Payment.Id);
                    Payment = Serializer.SerializeObject(capture);
                    break;
                case "Cancel":
                    Payment = Serializer.SerializeObject(await data.AsyncClient.CancelPaymentAsync(data.Payment.Id));
                    break;
                case "Return":
                    Payment = Serializer.SerializeObject(
                        await data.AsyncClient.CreateRefundAsync(new NewRefund { Amount = data.Payment.Amount, PaymentId = data.Payment.Id}));
                    break;
                default:
                    throw new InvalidOperationException(Action);
            }

            return RedirectToPage("FinishAsync", new {Id = Id});
        }
    }
}
