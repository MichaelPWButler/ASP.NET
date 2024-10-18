using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MathsQuizV2WebApp.Pages
{
    public class IndexModel : PageModel
    {
     
        public void OnGet() 
        {

            //TO Do initiliase session variables so that when restarting they cant skip

            string? error = Request.Query["error"];
            string? time = Request.Query["time"];
            string? JavaScriptDisabled = Request.Query["JS"];

            if (!string.IsNullOrEmpty(error) && error == "InvalidSelection")
            {
                // Trigger the error message
                ViewData["ErrorMessage"] = "Invalid difficulty or number of questions. Please try again.";
            }

            if (!string.IsNullOrEmpty(time) && time == "OutOfTime")
            {
                // Trigger the error message
                ViewData["OutOfTime"] = "Too Slow, think Quicker!";
            }

            if (!string.IsNullOrEmpty(JavaScriptDisabled) && JavaScriptDisabled == "NiceTry")
            {
                // Trigger the error message
                ViewData["NiceTry"] = "Nice Try Buddy!";
            }
        }

    }
}
