using QuizWebAppV2.Pages.Shared;

namespace MathsQuizV2WebApp.Pages
{
    internal class QuestionV2
    {
        public List<decimal> Nums { get; set; }

        public List<string> Ops { get; set; }

        public int Max { get; set; }

        public decimal Answer { get; private set; }

        internal BIDMAS calculateAnswer = new BIDMAS();

        public QuestionV2(List<decimal> nums, List<string> ops , int max)
        {
            Nums = nums;
            Ops = ops;
            Max = 25 * max; //Setting the difficulty, limiting the answer
            Answer = GetAnswer();
        }

        //Looping through the numbers and operators, adding them to a string and returing it 
        public string ReturnFullQuestion()
        {
            string question = Nums[0].ToString();
            for (int i = 0; i < Ops.Count; i++)
            {
                question += $" {Ops[i]} {Nums[i + 1]}";
            }
            return question;
        }

        //Calculating the answer based on the number of numbers and operators
        private decimal GetAnswer()
        {
            List<decimal> numsCopy = new List<decimal>(Nums);
            List<string> opsCopy = new List<string>(Ops);
            decimal result = calculateAnswer.calculateAnswer(numsCopy, opsCopy);
            return result;
            
        }


        // Checking that the anwser of the question is a valid answer.
        public bool CheckAnswer()
        {
            bool IsAnswerInRange = (Answer > Max || Answer < 0);
            bool IsDivisionValid = (Ops.Contains("/") && (Answer % 1 != 0));

            return IsAnswerInRange || IsDivisionValid;
        }

    }
}
