using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public bool AllowConfirm { get; set; }

        public string Message { get; set; }

        public async Task OnGetAsync(int id)
        {
            var data = PaymentStorage.Payments[id];
            Id = id;

            var payment = await data.Client.QueryPaymentAsync(data.Payment);
            Payment = Client.SerializeObject(payment);

            AllowConfirm = payment.Paid && payment.Status == PaymentStatus.WaitingForCapture;

            var sb = new StringBuilder();
            if (payment.Paid == false)
                sb.AppendLine("Not payed");
            if (payment.Status != PaymentStatus.WaitingForCapture)
                sb.AppendFormat("Status: {0}", payment.Status).AppendLine();
            if (payment.Status == PaymentStatus.Canceled)
                sb.AppendFormat("Payment canceled: {0} at {1}", payment.CancellationDetails.Reason,
                    payment.CancellationDetails.Party).AppendLine();

            Message = sb.ToString();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var data = PaymentStorage.Payments[Id];

            var capture = await data.Client.CaptureAsync(new Payment() {Id = data.Payment.Id});
            Payment = Client.SerializeObject(capture);

            return RedirectToPage("FinishAsync", new {Id = Id});
        }
    }
}
