using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Yandex.Checkout.V3.Demo.Pages.BaseModels
{
    public class FinishModel : PageModel
    {
        public int Id { get; set; }
        public string Payment { get; set; }
    }
}