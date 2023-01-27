using System.Diagnostics;

namespace AoC17.Day03
{
    internal class SpiralMemory
    {
        int input = 0;

        public void ParseInput(List<string> lines)
            => input = int.Parse(lines[0]);

        int FindDistance(int part = 1)
        {
            var currentRollCorner = 1;
            var step = 0;
            
            while (currentRollCorner < input)
            {
                step += 2;
                currentRollCorner += 4*step;
            }

            var highLimit = currentRollCorner;
            var lowLimit = highLimit - step - 1;

            while (!(input >= lowLimit && input <= highLimit))
            {
                highLimit = lowLimit;
                lowLimit = highLimit - step - 1;
            }
            var offset = Math.Abs( ((lowLimit + highLimit) / 2) - input);

            return step/2 + offset + 1;
        }


        public int Solve(int part = 1)
            => FindDistance(part);
    }
}
