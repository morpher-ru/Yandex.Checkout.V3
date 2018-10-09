using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Yandex.Checkout.V3.Demo.Pages
{
    public class ConfirmAsyncModel : PageModel
    {
        [BindProperty] public int Id { get; set; }
        public string Payment { get; private set; }

        public async Task OnGetAsync(int id)
        {
            var data = PaymentStorage.Payments[id];
            Id = id;

            Payment = JsonConvert.SerializeObject(await data.Client.QueryPaymentAsync(data.Payment));
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var data = PaymentStorage.Payments[Id];

            var capture = await data.Client.CaptureAsync(data.Payment);
            Payment = JsonConvert.SerializeObject(capture);

            return RedirectToPage("FinishAsync", new {Id = Id});
        }
    }
}
