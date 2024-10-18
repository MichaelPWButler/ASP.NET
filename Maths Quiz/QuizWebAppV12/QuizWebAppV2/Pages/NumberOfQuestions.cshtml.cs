using MathsQuizV2WebApp.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizWebAppV2.Pages.Shared;
using SessionHelpers;
using System.ComponentModel.DataAnnotations;

namespace QuizWebAppV2.Pages
{
    public class NumberOfQuestionsModel : PageModel
    {

        private ISessionHelper _Session;

        public NumberOfQuestionsModel(ISessionHelper sessionHelper)
        {
            _Session = sessionHelper;
        }

        [BindProperty]
        public int NumberOfQuestions { get; set; }
        


        public void OnGet()
        {
            if (_Session["Difficulty"] == null)
            {
                Response.Redirect($"/Index?error=InvalidSelection");
            }
   
        }
        public void OnPost()
        {
            
            Levels difficulty = (Levels)_Session["Difficulty"];
            int maxDifficulty = 0;
            switch (difficulty)
            {
                case Levels.Easy: 
                    maxDifficulty = 1;
                    break;
                case Levels.Medium: 
                    maxDifficulty = 3; 
                    break;
                case Levels.Hard: 
                    maxDifficulty = 5;
                    break;
            }

            QuizGenerator quizGenerator = new QuizGenerator();
            Quiz GeneratedQuiz  = quizGenerator.GenerateSingleQuiz(NumberOfQuestions, maxDifficulty);

            _Session["Quiz"] = GeneratedQuiz;

            Response.Redirect("/QuizPage");
        }
    }
}
