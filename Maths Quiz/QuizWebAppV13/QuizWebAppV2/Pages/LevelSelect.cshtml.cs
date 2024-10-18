using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizWebAppV2.Pages.Shared;
using SessionHelpers;
using System.ComponentModel.DataAnnotations;


namespace QuizWebAppV2.Pages
{
    public class LevelSelectModel : PageModel
    {

        private ISessionHelper _Session;

        public LevelSelectModel(ISessionHelper sessionHelper)
        {
            _Session = sessionHelper;
        }


        [BindProperty, Required] 
        public string selectedDifficulty { get; set; }

        public void OnPost()
        {
            if (Enum.TryParse<Levels>(selectedDifficulty, out Levels difficulty))
            {
                _Session["Difficulty"] = difficulty;
                Response.Redirect("/NumberOfQuestions");

            }
            else
            {
                Response.Redirect("/LevelSelect");
            }

            

        }
    }
}
