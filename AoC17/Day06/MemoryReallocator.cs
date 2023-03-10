namespace AoC17.Day06
{
    internal class MemoryReallocator
    {
        List<int> memoryBanks = new();

        public void ParseInput(List<string> lines)
            => memoryBanks = lines[0].Split("\t", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                                     .Select(x => int.Parse(x))
                                     .ToList();

        public string GetCurrentState()
            => string.Join(',', memoryBanks.ToArray());

        int Redistribute(int part = 1)
        {
            HashSet<string> knownStates = new();
            List<string> knownStatesList = new();   
            var currentState = GetCurrentState();
            var steps = 0;

            while (knownStates.Add(currentState))
            {
                var count = memoryBanks.Max();
                var startingIndex = memoryBanks.IndexOf(count);

                memoryBanks[startingIndex] = 0;

                for (int i = 1; i <= count; i++)
                    memoryBanks[(startingIndex + i) % memoryBanks.Count]++;
                currentState = GetCurrentState();
                knownStatesList.Add(currentState);  // For Part 2. 

                steps++;
            }
            return (part == 1) ? steps : knownStatesList.Count - knownStatesList.IndexOf(currentState) -1;
        }

        public int Solve(int part = 1)
            => Redistribute(part);
    }
}
