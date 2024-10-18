using MathsQuizV2WebApp.Pages;
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
            Quiz quiz2 = (Quiz)_Session["Quiz"];
            if (quiz2 == null)
            {
                Response.Redirect($"/Index?error=InvalidSelection");
            }
            correct = quiz2.GetNumberOfCorrectAnswers();
            IncorrectAnswers = quiz2.GetWrongAnswerList();
        }
    }
}
