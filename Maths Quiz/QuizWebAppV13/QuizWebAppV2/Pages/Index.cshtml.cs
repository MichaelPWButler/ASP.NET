using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SessionHelpers;

namespace MathsQuizV2WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private ISessionHelper _Session;

        public IndexModel(ISessionHelper sessionHelper)
        {
            _Session = sessionHelper;
        }
        internal string error;

        public void OnGet() 
        {

            error = (string)_Session["Error"];

            _Session["Difficulty"] = null;
            _Session["Quiz"] = null;
        }

    }
}
