using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace AoC17.Day13
{
    enum ScanDirection
    { 
        Up = 0,
        Down = 1,
    }

    class FirewallLayer
    {
        public int Depth;
        public int ScanRange;
        public int ScanPos;
        public ScanDirection Direction;

        public FirewallLayer(int depth, int range)
        { 
            Depth = depth;
            ScanRange = range;
            ScanPos = 0;
            Direction = ScanDirection.Down;
        }

        public int Move()
        {
            if (Direction == ScanDirection.Down && ScanPos == ScanRange - 1)
                Direction = ScanDirection.Up;
            if (Direction == ScanDirection.Up && ScanPos == 0)
                Direction = ScanDirection.Down;

            ScanPos += (Direction == ScanDirection.Down ? 1 : -1);
            return ScanPos;
        }
    }

    internal class Firewall
    {
        List<FirewallLayer> layers = new();

        FirewallLayer ParseLine(string line)
        { 
            var input = line.Split(":", StringSplitOptions.RemoveEmptyEntries|StringSplitOptions.TrimEntries)
                            .Select(x => int.Parse(x))
                            .ToList();
            return new FirewallLayer(input[0], input[1]);
        }

        public void ParseInput(List<string> lines)
            => lines.ForEach(line => layers.Add(ParseLine(line)));

        int Severity()
        {
            List<int> detected = new();

            for (int i = 0; i <= layers.Max(x => x.Depth); i++)
            {
                var currentLayer = layers.FirstOrDefault(x => x.Depth == i);
                if (currentLayer != null)
                    if (currentLayer.ScanPos == 0)
                        detected.Add(i);

                layers.ForEach(x => x.Move());
            }

            return detected.Aggregate(0, (result, x) => result + x * layers.First(l => l.Depth == x).ScanRange);
        }

        public int Solve(int part = 1)
            => Severity();
    }

   
}
