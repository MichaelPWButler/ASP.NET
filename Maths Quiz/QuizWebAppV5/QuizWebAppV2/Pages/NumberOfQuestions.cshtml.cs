using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
            string difficulty = (string)_Session["Difficulty"];
            if (difficulty == null)
            {
                Response.Redirect($"/Index?error=InvalidSelection");
            }
   
        }
        public void OnPost()
        {
            _Session["NumberOfQuestions"] = NumberOfQuestions;
            Response.Redirect("/Quiz");
        }
    }
}
