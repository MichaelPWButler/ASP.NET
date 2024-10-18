using Microsoft.AspNetCore.Mvc.RazorPages;
using SessionHelpers;

namespace QuizWebAppV2.Pages
{
    public class ResultsPageModel : PageModel
    {

        private ISessionHelper _Session;

        public ResultsPageModel(ISessionHelper sessionHelper)
        {
             _Session = sessionHelper;
        }

        internal int correct;
        internal List<string> IncorrectAnswers;

        public void OnGet()
        {
            correct = (int)_Session["Correct"];
            IncorrectAnswers = (List<string>)_Session["IncorretAnswers"];
        }
    }
}
