namespace AoC17.Day10
{
    internal class KnotHasher
    {
        List<int> lengths = new();

        public void ParseLengths(string line)
        {
            var groups = line.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            lengths = groups.Select(x => int.Parse(x)).ToList();
        }

        public void ParseInput(List<string> lines)
            => ParseLengths(lines[0]);

        int KnotHash(int part = 1)
        {
            List<int> elements = new();
            for (int i = 0; i < 256; elements.Add(i++)) ;
            int currentPosition = 0;
            int skipsize = 0;

            foreach (var len in lengths)
            {
                // Build the list to reverse
                List<int> listToReverse = new List<int>();
                for(int p = 0; p<len; p++)
                    listToReverse.Add( elements[(currentPosition + p) % elements.Count] );

                listToReverse.Reverse();

                for (int p = 0; p < len; p++)
                    elements[(currentPosition + p) % elements.Count] = listToReverse[p];

                currentPosition += (skipsize + len);
                currentPosition %= elements.Count;
                skipsize++;
            }
            return elements[0] * elements[1];
        }

        public int Solve(int part = 1)
            => KnotHash(part);
    }
}
