using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Yandex.Checkout.V3.Demo.Pages
{
    public class ConfirmSyncModel : PageModel
    {
        [BindProperty] public int Id { get; set; }
        public string Payment { get; private set; }

        public void OnGetAsync(int id)
        {
            var data = PaymentStorage.Payments[id];
            Id = id;

            Payment = Client.SerializeObject(data.Client.QueryPayment(data.Payment.Id));
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
