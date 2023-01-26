using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC17.Day01
{
    internal class InverseCaptcha
    {
        string input = "";

        public void ParseInput(List<string> lines)
            => input = lines[0];

        int FindCaptcha(int part = 1)
        {
            var innerSum = input.Select((x, index) => (index < input.Length - 1 && x == input[index + 1]) ? int.Parse(x.ToString()) : 0).Sum();
            return innerSum + (input[0] == input[input.Length - 1] ? int.Parse(input[0].ToString()) : 0);
        }

        public int Solve(int part = 1)
            => FindCaptcha(part);
    }
}
