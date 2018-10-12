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

            var capture = await data.Client.CaptureAsync(data.Payment.Id);
            Payment = Client.SerializeObject(capture);

            return RedirectToPage("FinishAsync", new {Id = Id});
        }
    }
}
