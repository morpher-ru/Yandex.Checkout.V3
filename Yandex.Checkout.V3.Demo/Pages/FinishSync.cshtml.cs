using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using Yandex.Checkout.V3.Demo.Pages.BaseModels;

namespace Yandex.Checkout.V3.Demo.Pages
{
    public class FinishSyncModel : FinishModel
    {
        public void OnGet(int id)
        {
            var data = PaymentStorage.Payments[id];
            Id = id;

            Payment = Client.SerializeObject(data.Client.QueryPayment(data.Payment.Id));
        }

        public void OnPost()
        {
            var data = PaymentStorage.Payments[Id];

            switch (Action)
            {
                case "Cancel":
                    Payment = Client.SerializeObject(data.Client.CancelPayment(data.Payment.Id));
                    break;
                case "Return":

                    var returnInfo = data.Client.CreateRefund(new NewRefund() { Amount = data.Payment.Amount, PaymentId = data.Payment.Id});
                    var paymentInfo = data.Client.QueryPayment(data.Payment.Id);

                    Payment = Client.SerializeObject(new
                    {
                        ReturnInfo = returnInfo,
                        PaymentInfo = paymentInfo
                    });

                    break;
                default:
                    throw new InvalidOperationException(Action);
            }
        }

    }
}
