using System;
using System.Threading.Tasks;
using Yandex.Checkout.V3.Demo.Pages.BaseModels;

namespace Yandex.Checkout.V3.Demo.Pages
{
    public class FinishAsyncModel : FinishModel
    {
        public async Task OnGetAsync(int id)
        {
            var data = PaymentStorage.Payments[id];
            Id = id;

            Payment = Client.SerializeObject(await data.Client.QueryPaymentAsync(data.Payment.Id));
        }

        public async Task OnPostAsync()
        {
            var data = PaymentStorage.Payments[Id];

            switch (Action)
            {
                case "Cancel":
                    Payment = Client.SerializeObject(await data.Client.CancelAsync(data.Payment.Id));
                    break;
                case "Return":

                    var returnInfo = await data.Client.RefundAsync(new NewRefund() { Amount = data.Payment.Amount, PaymentId = data.Payment.Id});
                    var paymentInfo = await data.Client.QueryPaymentAsync(data.Payment.Id);

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
