using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Yandex.Checkout.V3.Demo.Pages.BaseModels
{
    public class ConfirmModel : PageModel
    {
        [BindProperty] public int Id { get; set; }
        [BindProperty] public string Action { get; set; }
        public string Payment { get; set; }
        public bool AllowConfirm { get; set; }
        public string Message { get; set; }

        protected void ProcessPayment(Payment payment)
        {
            Payment = Serializer.SerializeObject(payment);

            AllowConfirm = payment.Paid && payment.Status == PaymentStatus.WaitingForCapture;

            var sb = new StringBuilder();
            if (payment.Paid == false)
                sb.AppendLine("Not paid");
            if (payment.Status != PaymentStatus.WaitingForCapture)
                sb.AppendFormat("Status: {0}", payment.Status).AppendLine();
            if (payment.Status == PaymentStatus.Canceled)
                sb.AppendFormat("Payment canceled: {0} at {1}", payment.CancellationDetails.Reason,
                    payment.CancellationDetails.Party).AppendLine();

            Message = sb.ToString();
        }
    }
}
