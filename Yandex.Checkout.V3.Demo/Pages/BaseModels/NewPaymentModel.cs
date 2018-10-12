using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Yandex.Checkout.V3.Demo.Pages.BaseModels
{
    public class NewPaymentModel : PageModel
    {
        [BindProperty, Required] 
        public string ShopId { get; set; } = "501156";

        [BindProperty, Required]
        public string SecretKey { get; set; } = "test_As0OONRn1SsvFr0IVlxULxst5DBIoWi_tyVaezSRTEI";

        [BindProperty, Range(1, 2000), Required]
        public decimal Amount { get; set; } = 2000;

        [BindProperty]public string Payment { get; set; }
    }
}
