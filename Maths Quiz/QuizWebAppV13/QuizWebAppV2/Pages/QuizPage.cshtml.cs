using MathsQuizV2WebApp.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SessionHelpers;

namespace QuizWebAppV2.Pages
{
    public class QuizPageModel : PageModel
    {

        private ISessionHelper _Session;

        public QuizPageModel(ISessionHelper sessionHelper)
        {
            _Session = sessionHelper;
        }

        internal Quiz quiz;
        public int questionIndex { get; set; }

        [BindProperty]
        public decimal QuestionInput { get; set; }

        public void OnGet()
        {
            int questionIndex = 0;
            _Session["questionIndex"] = questionIndex;
            quiz = (Quiz)_Session["Quiz"];
            if (quiz == null)
            {
                _Session["Error"] = "Invalid Choice Mate";
                Response.Redirect("/Index");
            }
            else
            {
                quiz = (Quiz)_Session["Quiz"];
                _Session["Error"] = null;
            }
        }

        public void OnPost()
        {
            quiz = (Quiz)_Session["Quiz"];

            questionIndex = (int)_Session["questionIndex"];

            TimeSpan TimeSubmitQuiz;
            TimeSubmitQuiz = DateTime.Now.TimeOfDay;
            int timeSubmit = (int)TimeSubmitQuiz.TotalSeconds;
            int timeStarted = quiz.StartTime;

            

            quiz.UserAnswers.Add(QuestionInput);

            if (quiz.UserAnswers.Count < quiz.QuizQuestions.Count)
            {
                questionIndex = questionIndex + 1;
                _Session["questionIndex"] = questionIndex;
            }

            else
            {
                _Session["Quiz"] = quiz;
                Response.Redirect("/ResultsPage");
            }

            if ((timeSubmit - timeStarted) > (10 * quiz.QuizQuestions.Count))
            {
                _Session["Error"] = "Nice Try Mate";
                Response.Redirect("/Index");
            }
            else
            {
                _Session["Error"] = null;
            }

        }

    }
}
