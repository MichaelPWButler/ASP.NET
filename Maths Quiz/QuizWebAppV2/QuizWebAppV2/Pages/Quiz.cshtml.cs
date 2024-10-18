using MathsQuizV2WebApp.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SessionHelpers;
using System.ComponentModel.DataAnnotations;

namespace QuizWebAppV2.Pages
{
    public class QuizModel : PageModel
    {

        private ISessionHelper _Session;

        public QuizModel(ISessionHelper sessionHelper)
        {
            _Session = sessionHelper;
        }


        internal List<QuestionV2> QuestionsForQuiz = new List<QuestionV2>();
        private QuestionGenerator QuestionGenerator = new QuestionGenerator();
        private int maxDifficulty;
        internal int correct;
        List<string> Incorrectanswers = new List<string>();


        [BindProperty]
        public List<int>? QuestionInput {  get; set; }


        public void OnGet()
        {
            string difficulty = (string)_Session["Difficulty"];
            int NumberOfQuestions = (int)_Session["NumberOfQuestions"];


            switch (difficulty)
            {
                case "Easy": maxDifficulty = 1; break;
                case "Medium": maxDifficulty = 3; break;
                case "Hard": maxDifficulty = 5; break;
            }

            for (int i = 0; i < NumberOfQuestions; i++)
            {
                QuestionV2 question;
                do
                {
                    question = QuestionGenerator.SingleQuestionGenerator(maxDifficulty);
                }
                while (question.CheckAnswer());

                QuestionsForQuiz.Add(question);
            }
            
            _Session["QuestionsForQuiz"] = QuestionsForQuiz;

        }

        public void OnPost() 
        {
            List<QuestionV2> QuestionsForQuiz = (List<QuestionV2>)_Session["QuestionsForQuiz"];
            for (int i = 0; i < QuestionsForQuiz.Count; i++)
            {
                if (QuestionInput[i] == QuestionsForQuiz[i].Answer)
                {
                    correct++;
                }
                else
                {
                    Incorrectanswers.Add("Question " +(i +1) + ": " + QuestionsForQuiz[i].ReturnFullQuestion());
                }
            }
            _Session["Correct"] = correct;
            _Session["IncorretAnswers"] = Incorrectanswers;
            Response.Redirect("/ResultsPage");
        }

    }
}
