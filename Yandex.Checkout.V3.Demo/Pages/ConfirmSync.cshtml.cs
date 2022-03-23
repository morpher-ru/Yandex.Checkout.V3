using Microsoft.AspNetCore.Mvc;
using System;
using Yandex.Checkout.V3.Demo.Pages.BaseModels;

namespace Yandex.Checkout.V3.Demo.Pages
{
    public class ConfirmSyncModel : ConfirmModel
    {
        public void OnGetAsync(int id)
        {
            var data = PaymentStorage.Payments[id];
            Id = id;

            var payment = data.Client.GetPayment(data.Payment.Id);
            ProcessPayment(payment);
        }

        public IActionResult OnPost()
        {
            var data = PaymentStorage.Payments[Id];

            switch (Action)
            {
                case "Confirm":
                    var capture = data.Client.CapturePayment(data.Payment.Id);
                    Payment = Serializer.SerializeObject(capture);
                    break;
                case "Cancel":
                    Payment = Serializer.SerializeObject(data.Client.CancelPayment(data.Payment.Id));
                    break;
                case "Return":
                    Payment = Serializer.SerializeObject(
                        data.Client.CreateRefund(new NewRefund { Amount = data.Payment.Amount, PaymentId = data.Payment.Id}));
                    break;
                default:
                    throw new InvalidOperationException(Action);
            }

            return RedirectToPage("FinishSync", new {Id = Id});
        }

    }
}
