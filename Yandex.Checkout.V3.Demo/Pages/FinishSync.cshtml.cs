using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Yandex.Checkout.V3.Demo.Pages
{
    public class FinishSyncModel : PageModel
    {
        public int Id { get; private set; }
        public string Payment { get; private set; }

        public void OnGet(int id)
        {
            var data = PaymentStorage.Payments[id];
            Id = id;

            Payment = Client.SerializeObject(data.Client.QueryPayment(data.Payment.Id));
        }
    }
}
