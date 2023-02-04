using AoC17.Day10;
using System.Collections.Generic;

namespace AoC17.Day14
{
    internal class Defragmenter
    {
        string hashRoot = "";
        List<string> diskMap = new();

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
                diskMap.Add(binaryHash);
            }
            return usedSpaces;
        }

        List<(int row, int col)> GetNeighbors((int row, int col) pos)
        {
            List<(int row, int col)> retVal = new() { (pos.row - 1, pos.col), (pos.row + 1, pos.col), 
                                                      (pos.row, pos.col - 1), (pos.row, pos.col + 1) };

            return retVal.Where(x => x.row>=0 && x.col>=0 && x.row<=127 && x.col<=127).ToList();
        }

        public void FindAllSectorsInRegion(int startRow, int startColumn, HashSet<(int row, int col)> sectorsInRegion)
        {
            Queue<(int row, int col)> activeNodes = new();
            activeNodes.Enqueue((startRow, startColumn));

            while(activeNodes.Count > 0) 
            {
                var currentSector = activeNodes.Dequeue();
                if (!sectorsInRegion.Add(currentSector))
                    continue;
                var neighs = GetNeighbors(currentSector);
                foreach (var neighbor in neighs)
                    if (!sectorsInRegion.Contains(neighbor) && diskMap[neighbor.row][neighbor.col] == '1')
                        activeNodes.Enqueue(neighbor);
            }
        }

        public int FindRegions()
        {
            DefragmentDisk();
            HashSet<(int row, int col)> sectorsAlreadyInRegion = new();
            int numRegions = 0;

            for (int row = 0; row < 128; row++)
                for (int col = 0; col < 128; col++)
                {
                    var current = (row, col);
                    if (diskMap[row][col] == '1' && !sectorsAlreadyInRegion.Contains(current))
                    {
                        numRegions++;
                        FindAllSectorsInRegion(row, col, sectorsAlreadyInRegion);
                    }
                }
            return numRegions;
        }

        public int Solve(int part = 1)
            => part == 1 ? DefragmentDisk() : FindRegions();
    }
}
