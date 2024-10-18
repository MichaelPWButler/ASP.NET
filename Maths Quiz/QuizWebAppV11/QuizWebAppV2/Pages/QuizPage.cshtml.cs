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
        public int questionIndex {get; set;}

        [BindProperty]
        public decimal QuestionInput {  get; set; }

        public void OnGet()
        {
            int questionIndex = 0;
            _Session["questionIndex"] = questionIndex;
            quiz = (Quiz)_Session["Quiz"];
            if (quiz == null)
            {
                Response.Redirect($"/Index?error=InvalidSelection");
            }
            else
            {
                quiz = (Quiz)_Session["Quiz"];
            }
        }

        public void OnPost()
        {
            quiz = (Quiz)_Session["Quiz"];
            List<QuestionV2> QuestionsForQuiz = quiz.QuizQuestions;

            questionIndex = (int)_Session["questionIndex"];

            //MOVE TO QUIZ
            TimeSpan TimeSubmitQuiz;
            TimeSubmitQuiz = DateTime.Now.TimeOfDay;
            int timeSubmit = (int)TimeSubmitQuiz.TotalSeconds;
            int timeStarted = quiz.StartTime;

            if ((timeSubmit - timeStarted) > (10 * QuestionsForQuiz.Count))
            {
                Response.Redirect("/Index?JS=NiceTry");
            }

            quiz.UserAnswers.Add(QuestionInput);

            if (quiz.UserAnswers.Count < QuestionsForQuiz.Count)
            {
                questionIndex = questionIndex + 1;
                _Session["questionIndex"] = questionIndex;
            }

            
            else 
            { 
                _Session["Quiz"] = quiz;
                Response.Redirect("/ResultsPage");
            }
           
        }

    }
}
