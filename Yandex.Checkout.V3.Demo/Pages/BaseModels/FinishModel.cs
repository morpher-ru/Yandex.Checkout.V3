using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Yandex.Checkout.V3.Demo.Pages.BaseModels
{
    public class FinishModel : PageModel
    {
        [BindProperty] public int Id { get; set; }
        [BindProperty] public string Action { get; set; }
        public string Payment { get; set; }
    }
}
