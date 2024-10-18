using System;

namespace QuizWebAppV2.Pages.Shared
{
    public class BIDMAS
    {

        public List<decimal> Nums { get; set; }

        public List<string> Ops { get; set; }

        internal decimal calculate(List<decimal> Nums, List<string> Ops)
        {
            while (Ops.Count > 0)
            {
                int index = 0;

                if (Ops.Contains("**"))
                {
                    index = Ops.IndexOf("**");
                }
                else if (Ops.Contains("/"))
                {
                    index = Ops.IndexOf("/");
                }
                else if (Ops.Contains("*"))
                {
                    index = Ops.IndexOf("*");
                }
                else if (Ops.Contains("+"))
                {
                    index = Ops.IndexOf("+");
                }
                else if (Ops.Contains("-"))
                {
                    index = Ops.IndexOf("-");
                }

                decimal result = 0 ;
                decimal num1 = Nums[index];
                decimal num2 = Nums[index + 1];

                if (Ops[index] == "**")
                {
                    result = Convert.ToDecimal(Math.Pow(Convert.ToDouble(num1), Convert.ToDouble(num2)));
                }
                else if (Ops[index] == "/")
                {
                    result = num1 / num2;
                }
                else if (Ops[index] == "*")
                {
                    result = num1 * num2;
                }
                else if (Ops[index] == "+")
                {
                    result = num1 + num2;
                }
                else if (Ops[index] == "-")
                {
                    result = num1 - num2;
                }

                Nums[index] = result; 
                Nums.RemoveAt(index + 1); 
                Ops.RemoveAt(index);
                
            }
            decimal Answer = Nums[0];
            return Answer;
        }


    }
}
