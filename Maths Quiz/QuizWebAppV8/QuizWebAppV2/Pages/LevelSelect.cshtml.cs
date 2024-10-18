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


        [BindProperty] 
        public string selectedDifficulty { get; set; }

        public void OnPost()
        {
            if (Enum.TryParse<Levels>(selectedDifficulty, out var difficulty))
            {
                selectedDifficulty = difficulty.ToString();
                _Session["Difficulty"] = selectedDifficulty;
                Response.Redirect("/NumberOfQuestions");

            }
            else
            {
                Response.Redirect("/LevelSelect");
            }

            

        }
    }
}
