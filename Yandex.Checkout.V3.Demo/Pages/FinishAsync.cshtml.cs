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
    }
}
