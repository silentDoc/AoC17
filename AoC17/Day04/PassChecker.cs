namespace AoC17.Day04
{
    internal class PassChecker
    {
        List<List<string>> phrases = new();
        public void ParseInput(List<string> lines)
            => lines.ForEach(line => phrases.Add(line.Split(" ").ToList()));

        bool CheckPolicy(List<string> wordGroup)
        {
            if (wordGroup.Distinct().Count() != wordGroup.Count)
                return false;

            foreach (string s in wordGroup)
                foreach (string comp in wordGroup.Where(x => x != s))
                {
                    var sorted1 = string.Join("", s.OrderBy(x => x).ToArray());
                    var sorted2 = string.Join("", comp.OrderBy(x => x).ToArray());

                    if (sorted2 == sorted1)
                        return false;
                }
            return true;
        }

        public int Solve(int part = 1)
            => (part == 1) ? phrases.Count(x => x.Distinct().Count() == x.Count) : phrases.Count(x => CheckPolicy(x));
    }
}
