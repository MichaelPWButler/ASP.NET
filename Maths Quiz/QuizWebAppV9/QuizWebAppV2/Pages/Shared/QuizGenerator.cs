using MathsQuizV2WebApp.Pages;

namespace QuizWebAppV2.Pages.Shared
{
    public class QuizGenerator
    {
        internal Quiz QuizGeneration(int NumberOfQuestions, int maxDifficulty) 
        {
            TimeSpan TimeStartedQuiz;
            TimeStartedQuiz = DateTime.Now.TimeOfDay;
            int timeStarted = (int)TimeStartedQuiz.TotalSeconds;

            List<QuestionV2> QuestionsForQuiz = new List<QuestionV2>();
            QuestionGenerator QuestionGenerator = new QuestionGenerator();
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

            Quiz quiz = new Quiz();
            quiz.NumberOfQuestion = NumberOfQuestions;
            quiz.StartTime = timeStarted;
            quiz.QuizQuestions = QuestionsForQuiz;
            quiz.UserAnswers = new List<decimal>();
           


            return quiz;
        }
    }
}
