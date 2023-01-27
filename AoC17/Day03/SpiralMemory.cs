using AoC17.Common;

namespace AoC17.Day03
{
    record SpiralElement
    {
        Coord2D position = new(0,0);
        public int value;
    }

    internal class SpiralMemory
    {
        int input = 0;

        public void ParseInput(List<string> lines)
            => input = int.Parse(lines[0]);

        int FindDistance(int part = 1)
        {
            var currentSpiralCorner = 1;
            var step = 0;
            
            while (currentSpiralCorner < input)
            {
                step += 2;
                currentSpiralCorner += 4*step;
            }

            var highLimit = currentSpiralCorner;
            var lowLimit = highLimit - step - 1;

            while (!(input >= lowLimit && input <= highLimit))
            {
                highLimit = lowLimit;
                lowLimit = highLimit - step - 1;
            }
            var offset = Math.Abs( ((lowLimit + highLimit) / 2) - input);

            return step/2 + offset + 1;
        }

        int GenerateSpiral()
        {


            return 0;
        }

        public int Solve(int part = 1)
            => FindDistance(part);
    }
}
