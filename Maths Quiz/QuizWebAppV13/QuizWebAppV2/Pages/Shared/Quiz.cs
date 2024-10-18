namespace MathsQuizV2WebApp.Pages
{
    public class Quiz
    {

        public int NumberOfQuestion { get; set; }
        public int StartTime { get; set; }
        internal List<QuestionV2>? QuizQuestions { get; set; }
        public List<decimal>? UserAnswers { get; set; }


        internal List<string> GetWrongAnswerList()
        {
            List<string> ListOfWrong = new List<string> { };
            for (int i = 0; i < QuizQuestions.Count; i++)
            {
                if (!(UserAnswers[i] == QuizQuestions[i].Answer))
                {
                    ListOfWrong.Add($"Question {i + 1} : {QuizQuestions[i].ReturnFullQuestion()}");
                }
            }
            return ListOfWrong;
        }

        internal int GetNumberOfCorrectAnswers()
        {
            int correct = 0;
            for (int i = 0; i < QuizQuestions.Count; i++)
            {
                if ((UserAnswers[i] == QuizQuestions[i].Answer))
                {
                    correct = correct + 1;
                }
            }
            return correct;
        }
    }
}
