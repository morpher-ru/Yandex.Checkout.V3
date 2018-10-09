using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

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

            Payment = JsonConvert.SerializeObject(data.Client.QueryPayment(data.Payment));
        }
    }
}
