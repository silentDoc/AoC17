namespace AoC17.Day05
{
    internal class CPUJumper
    {
        List<int> instructions = new();

         public void ParseInput(List<string> lines)
            => lines.ForEach(line => instructions.Add(int.Parse(line.Trim())) );

        int RunJumps(int part = 1)
        {
            var currentIndex = 0;
            var steps = 0;
            while (currentIndex < instructions.Count)
            {
                instructions[currentIndex]++;
                currentIndex += (instructions[currentIndex] - 1);
                steps++;
            }
            return steps;
        }

        public int Solve(int part)
            => RunJumps(part);
    }
}
