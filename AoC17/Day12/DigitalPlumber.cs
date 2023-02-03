namespace AoC17.Day12
{
    record struct AdventProgram
    {
        public int Num;
        public List<int> ConnectedPrograms;
    }

    internal class DigitalPlumber
    {
        List<AdventProgram> adventProgramList = new();

        AdventProgram ParseLine(string line)
        { 
            var parts = line.Split(" <-> ");
            AdventProgram retVal = new();
            retVal.Num = int.Parse(parts[0]);
            retVal.ConnectedPrograms = parts[1].Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                                               .Select(x => int.Parse(x))
                                               .ToList();
            return retVal;
        }

        public void ParseInput(List<string> lines)
            => lines.ForEach(x => adventProgramList.Add(ParseLine(x)));


        int FindConnections(int part = 1)
        {
            HashSet<int> visitedNodes = new();
            var startNode = adventProgramList.First(x => x.Num == 0);

            Queue<AdventProgram> activeNodes = new();
            activeNodes.Enqueue(startNode);

            while (activeNodes.Count > 0)
            {
                var currentNode = activeNodes.Dequeue();
                if (!visitedNodes.Add(currentNode.Num))
                    continue;

                foreach (var progNum in currentNode.ConnectedPrograms)
                    if (!visitedNodes.Contains(progNum))
                        activeNodes.Enqueue(adventProgramList.First(x => x.Num == progNum));
            }
            return visitedNodes.Count; 
        }

        public int Solve(int part = 1)
            => FindConnections(part);

    }
}
