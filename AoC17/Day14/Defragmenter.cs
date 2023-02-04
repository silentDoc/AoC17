using AoC17.Day10;

namespace AoC17.Day14
{
    internal class Defragmenter
    {
        string hashRoot = "";

        public void ParseInput(List<string> lines)
            => hashRoot = lines[0].Trim();

        string HexToBinary(char hexChar)
            => hexChar switch
            {
                '0' => "0000",
                '1' => "0001",
                '2' => "0010",
                '3' => "0011",
                '4' => "0100",
                '5' => "0101",
                '6' => "0110",
                '7' => "0111",
                '8' => "1000",
                '9' => "1001",
                'a' => "1010",
                'b' => "1011",
                'c' => "1100",
                'd' => "1101",
                'e' => "1110",
                'f' => "1111",
                _ => throw new InvalidOperationException("Unknown hex digit")
            };

        int DefragmentDisk()
        {
            int usedSpaces = 0;
            for (int row = 0; row < 128; row++)
            {
                KnotHasher hasher = new KnotHasher();
                var hashKey = hashRoot + "-" + row.ToString();
                hasher.ParseInput(new List<string> { hashKey });
                var hexHash = hasher.KnotHash(2).ToLower();
                var binaryHash = string.Concat(hexHash.Select(x => HexToBinary(x)).ToList());
                usedSpaces += binaryHash.Count(x => x == '1');
            }
            return usedSpaces;
        }

        public int Solve(int part = 1)
            => DefragmentDisk();
    }
}
