using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

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

            Payment = JsonConvert.SerializeObject(data.Client.QueryPayment(data.Payment));
        }

        public IActionResult OnPost()
        {
            var data = PaymentStorage.Payments[Id];

            var capture = data.Client.Capture(data.Payment);
            Payment = JsonConvert.SerializeObject(capture);

            return RedirectToPage("FinishSync", new {Id = Id});
        }
    }
}
