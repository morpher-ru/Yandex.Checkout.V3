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

            Payment = Serializer.SerializeObject(await data.AsyncClient.GetPaymentAsync(data.Payment.Id));
        }

        public async Task OnPostAsync()
        {
            var data = PaymentStorage.Payments[Id];

            switch (Action)
            {
                case "Cancel":
                    Payment = Serializer.SerializeObject(await data.AsyncClient.CancelPaymentAsync(data.Payment.Id));
                    break;
                case "Return":

                    var returnInfo = await data.AsyncClient.CreateRefundAsync(new NewRefund { Amount = data.Payment.Amount, PaymentId = data.Payment.Id});
                    var paymentInfo = await data.AsyncClient.GetPaymentAsync(data.Payment.Id);

                    Payment = Serializer.SerializeObject(new
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
