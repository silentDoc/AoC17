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
                var currentJumpOffset = instructions[currentIndex];
                instructions[currentIndex]+= (part ==1) ? 1 : (currentJumpOffset >= 3) ? -1 : 1;
                currentIndex += currentJumpOffset;
                steps++;
            }
            return steps;
        }

        public int Solve(int part)
            => RunJumps(part);
    }
}
