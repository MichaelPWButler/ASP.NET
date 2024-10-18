namespace MathsQuizV2WebApp.Pages
{
    internal class QuestionGenerator
    {
        internal QuestionV2 SingleQuestionGenerator(int max)
        {
            List<string> operations = new List<string>() {"+", "-", "*", "/", "**" };
            //Creating the intial values needed to create a question object
            var rand = new Random();
            decimal numbers = rand.Next(2, 4);
            List<decimal> allNums = new List<decimal>();
            List<string> allOps = new List<string>();

            //Using a for loop to generate the correct number of operators
            for (int j = 0; j < (numbers - 1); j++)
            {
                string op = operations[(rand.Next(operations.Count))];
                allOps.Add(op);
            }
            //Using a for loop to generate the correct number of numbers 
            for (int j = 0; j < numbers; j++)
            {
                if (allOps.Contains("**")) //If a power operator is used, limit the numbers so to not generate a massive number
                {
                    decimal num = rand.Next(1, 5);
                    allNums.Add(num);
                }
                else
                {
                    decimal num = rand.Next(1, 25 * max);
                    allNums.Add(num);
                }

            }

            QuestionV2 Question = new QuestionV2(allNums, allOps, max);

            return Question;
        }
    }
}
