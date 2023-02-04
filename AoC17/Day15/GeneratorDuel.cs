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

        int RunPart1()
        {
            int numMatches = 0;
            for(int i=0; i< 40000000; i++) 
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

        int RunPart2()
        {
            List<string> resultsGenA = new();
            List<string> resultsGenB = new();

            int numMatches = 0;
            while(resultsGenA.Count < 5000000 || resultsGenB.Count<5000000)
            {
                generatorA *= 16807;
                generatorB *= 48271;
                generatorA %= 2147483647;
                generatorB %= 2147483647;

                if (generatorA % 4 == 0)
                {
                    var binaryA = Convert.ToString(generatorA, 2).PadLeft(16, '0');
                    binaryA = binaryA.Substring(binaryA.Length - 16, 16);
                    resultsGenA.Add(binaryA);
                }

                if(generatorB % 8 == 0) 
                {
                    var binaryB = Convert.ToString(generatorB, 2).PadLeft(16, '0');
                    binaryB = binaryB.Substring(binaryB.Length - 16, 16);
                    resultsGenB.Add(binaryB);
                }
            }

            for (int j = 0; j < 5000000; j++)
                if (resultsGenA[j] == resultsGenB[j])
                    numMatches++;
            
            return numMatches;
        }

        public int Solve(int part = 1)
            => part == 1 ? RunPart1() : RunPart2();
    }
}
