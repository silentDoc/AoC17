using AoC17.Day02;

namespace AoC17.Day04
{
    internal class PassChecker
    {
        List<List<string>> phrases = new();

        public void ParseInput(List<string> lines)
            => lines.ForEach(line => phrases.Add(line.Split(" ").ToList() ) );

        public int Solve(int part = 1)
            => phrases.Count(x => x.Distinct().Count() == x.Count);
    }
}
