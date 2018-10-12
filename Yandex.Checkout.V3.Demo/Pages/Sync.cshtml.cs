using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Yandex.Checkout.V3.Demo.Pages
{
    public class SyncModel : PageModel
    {
        [BindProperty, Required] 
        public string ShopId { get; set; } = "501156";

        [BindProperty, Required]
        public string SecretKey { get; set; } = "test_As0OONRn1SsvFr0IVlxULxst5DBIoWi_tyVaezSRTEI";

        [BindProperty, Range(1, 2000), Required]
        public decimal Amount { get; set; } = 2000;

        [BindProperty]public string Payment { get; set; }

        public IActionResult OnPost()
        {
            var client = new Client(ShopId, SecretKey);
            var id = PaymentStorage.GetNextId();

            var url = Request.GetUri();
            var redirect = $"{url.Scheme}://{url.Authority}/ConfirmSync?id={id}";

            var data = client.CreatePayment(
                new NewPayment()
                {
                    Amount = new Amount()
                    {
                        Value = Amount,
                    },
                    Confirmation = new Confirmation()
                    {
                        Type = ConfirmationType.Redirect,
                        ReturnUrl = redirect
                    },
                    Description = "Order"
                });

            PaymentStorage.Payments[id] = new QueryData() {Client = client, Payment = data};

            return Redirect(data.Confirmation.ConfirmationUrl);
        }

    }
}
