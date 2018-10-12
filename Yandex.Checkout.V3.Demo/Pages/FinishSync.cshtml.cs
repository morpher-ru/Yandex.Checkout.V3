using Microsoft.AspNetCore.Mvc.RazorPages;
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
    }
}
