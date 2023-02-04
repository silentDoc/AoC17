using System.Diagnostics;

namespace AoC17.Day15
{
    internal class GeneratorDuel
    {
        long generatorA = 0;
        long generatorB = 0;

        public void ParseInput(List<string> lines)
        { 
            generatorA = long.Parse(lines[0].Replace("Generator A starts with ", "").Trim());
            generatorB = long.Parse(lines[1].Replace("Generator B starts with ", "").Trim());
        }

        int Run(int times)
        {
            int numMatches = 0;
            for(int i=0; i<times; i++) 
            {
                generatorA *= 16807;
                generatorB *= 48271;
                generatorA %= 2147483647;
                generatorB %= 2147483647;

                var binaryA = Convert.ToString(generatorA, 2).PadLeft(16, '0');
                var binaryB = Convert.ToString(generatorB, 2).PadLeft(16, '0');
                binaryA = binaryA.Substring(binaryA.Length - 16, 16);
                binaryB = binaryB.Substring(binaryB.Length - 16, 16);

                if (binaryA == binaryB)
                    numMatches++;
            }
            return numMatches;
        }

        public int Solve(int part = 1)
            => Run(40000000);
    }
}
