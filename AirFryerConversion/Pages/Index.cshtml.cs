using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AirFryerConversion.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public int OvenTemp { get; set; }

        [BindProperty]
        public int OvenTime { get; set; }

        public int AirFryerTemp { get; set; }
        public int AirFryerTime { get; set; }
        public bool ConversionDone { get; set; } = false;

        public IActionResult OnPost()
        {
            AirFryerTemp = Convert.ToInt32(OvenTemp * 0.8);
            AirFryerTime = Convert.ToInt32(OvenTime * 0.75);

            ConversionDone = true;
            return Page();
        }
    }
}
