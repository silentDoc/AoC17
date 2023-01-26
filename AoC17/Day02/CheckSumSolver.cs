namespace AoC17.Day02
{
    internal class CheckSumSolver
    {
        List<int[]> spreadSheet = new();

        public void ParseInput(List<string> lines)
        { 
            foreach(var line in lines) 
            {
                var lineWithSpaces = line.Replace("\t", " ");
                var groups = lineWithSpaces.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                spreadSheet.Add(groups.Select(x => int.Parse(x)).ToArray());
            }
        }
        

        int FindChecksum(int part = 1)
            => spreadSheet.Select(x => x.Max() - x.Min()).Sum();

        public int Solve(int part = 1)
            => FindChecksum(part);
    }
}
