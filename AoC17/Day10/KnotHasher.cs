namespace AoC17.Day10
{
    internal class KnotHasher
    {
        List<int> lengths = new();
        List<int> elements = new();
        string input = "";

        public void ParseLengths(string line)
        {
            input = line;
            if (input.IndexOf(",") == -1)       // Modified to suport Day 14 :)
                return;
            var groups = line.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            lengths = groups.Select(x => int.Parse(x)).ToList();
        }

        public void ParseInput(List<string> lines)
            => ParseLengths(lines[0]);


        void KnotHashRound(ref int currentPosition, ref int skipSize)
        {
            if(elements.Count== 0)
                for (int i = 0; i < 256; elements.Add(i++)) ;

            foreach (var len in lengths)
            {
                // Build the list to reverse
                List<int> listToReverse = new List<int>();
                for(int p = 0; p<len; p++)      // Probably prettier with Takes and Skips, but this requires less thought :P
                    listToReverse.Add( elements[(currentPosition + p) % elements.Count] );

                listToReverse.Reverse();

                for (int p = 0; p < len; p++)
                    elements[(currentPosition + p) % elements.Count] = listToReverse[p];

                currentPosition += (skipSize + len);
                currentPosition %= elements.Count;
                skipSize++;
                skipSize %= elements.Count;
            }
        }

        public string KnotHash(int part =1) // Made public to support Day14
        {
            int currentPosition = 0;
            int skipSize = 0;

            if (part == 1)
            {
                KnotHashRound(ref currentPosition, ref skipSize);
                return (elements[0] * elements[1]).ToString();
            }
            // Part 2
            lengths.Clear();
            foreach (var c in input)
                lengths.Add((int)c);
            lengths.AddRange(new List<int> { 17, 31, 73, 47, 23 });

            for (int i = 0; i < 64; i++)
                KnotHashRound(ref currentPosition, ref skipSize);

            List<int> denseHash = new List<int>();
            for (int i = 0; i < 16; i++)
            {
                var block = elements.Skip(i*16).Take(16).ToList(); 
                int xorResult = block.Aggregate((acc,val) => acc^ val);
                denseHash.Add(xorResult);
            }

            return string.Concat(denseHash.Select(x => x.ToString("X2"))).ToLower();
        }

        public string Solve(int part = 1)
            => KnotHash(part).ToString();
    }
}
