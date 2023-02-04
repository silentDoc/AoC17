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

        // Standard BFS search
        List<int> FindConnections(int targetNode)
        {
            HashSet<int> visitedNodes = new();
            var startNode = adventProgramList.First(x => x.Num == targetNode);

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
            return visitedNodes.ToList(); 
        }

        int FindGroups()
        { 
            var listPrograms = adventProgramList.Select(x => x.Num).OrderBy(x =>x).ToList();
            var groups = 0;
            while (listPrograms.Count > 0)
            {
                var target = listPrograms[0];
                var visited = FindConnections(target);
                groups++;
                visited.ForEach(x => listPrograms.Remove(x));
            }
            return groups;
        }

        public int Solve(int part = 1)
            => part ==1 ? FindConnections(0).Count : FindGroups();
    }
}
