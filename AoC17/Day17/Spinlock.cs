using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC17.Day17
{
    internal class Spinlock
    {
        List<int> circularBuffer = new();
        int skip = 0;

        public void ParseInput(List<string> lines)
            => skip = int.Parse(lines[0]);

        int InsertValues()
        {
            circularBuffer.Add(0);
            int currentPosition = 0;
            for (int currentValue = 1; currentValue <= 2017; currentValue++)
            {
                currentPosition += skip +1;
                currentPosition %= circularBuffer.Count;
                circularBuffer.Insert(currentPosition, currentValue);
            }
            var pos2017 = circularBuffer.IndexOf(2017);
            return circularBuffer[(pos2017 +1) % circularBuffer.Count];
        }

        public int Solve(int part = 1)
            => InsertValues();
    }
}
