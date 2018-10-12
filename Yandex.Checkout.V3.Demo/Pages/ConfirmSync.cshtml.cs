using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Yandex.Checkout.V3.Demo.Pages.BaseModels;

namespace Yandex.Checkout.V3.Demo.Pages
{
    public class ConfirmSyncModel : ConfirmModel
    {
        public void OnGetAsync(int id)
        {
            var data = PaymentStorage.Payments[id];
            Id = id;

            var payment = data.Client.QueryPayment(data.Payment.Id);
            ProcessPayment(payment);
        }

        public IActionResult OnPost()
        {
            var data = PaymentStorage.Payments[Id];

            var capture = data.Client.Capture(data.Payment.Id);
            Payment = Client.SerializeObject(capture);

            return RedirectToPage("FinishSync", new {Id = Id});
        }
    }
}
