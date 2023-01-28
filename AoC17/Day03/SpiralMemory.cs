using AoC17.Common;
using System.Diagnostics;

namespace AoC17.Day03
{
    record SpiralElement
    {
        public Coord2D position = new(0,0);
        public int value;
    }

    internal class SpiralMemory
    {
        int input = 0;

        readonly Coord2D DIR_UP = new Coord2D(0, -1);
        readonly Coord2D DIR_LEFT = new Coord2D(-1, 0);
        readonly Coord2D DIR_DOWN = new Coord2D(0, 1);
        readonly Coord2D DIR_RIGHT = new Coord2D(1, 0);

        public void ParseInput(List<string> lines)
            => input = int.Parse(lines[0]);

        int FindDistance()
        {
            var currentSpiralCorner = 1;
            var step = 0;
            
            while (currentSpiralCorner < input)
            {
                step += 2;
                currentSpiralCorner += 4*step;
            }

            var highLimit = currentSpiralCorner;
            var lowLimit = highLimit - step - 1;

            while (!(input >= lowLimit && input <= highLimit))
            {
                highLimit = lowLimit;
                lowLimit = highLimit - step - 1;
            }
            var offset = Math.Abs( ((lowLimit + highLimit) / 2) - input);

            return step/2 + offset + 1;
        }

        int GenerateSpiral()
        {
            List<SpiralElement> spiral = new();

            Coord2D referenceTopRight = new Coord2D(1, -1);
            Coord2D referenceTopLeft = new Coord2D(-1, -1);
            Coord2D referenceBottomLeft = new Coord2D(-1, 1);
            Coord2D referenceBottomRight = new Coord2D(1, 1);

            Coord2D cornerTopRight = new Coord2D(1, -1);
            Coord2D cornerTopLeft = new Coord2D(-1, -1);
            Coord2D cornerBottomLeft = new Coord2D(-1, 1);
            Coord2D cornerBottomRight = new Coord2D(1, 1);

            SpiralElement start = new SpiralElement();
            start.value = 1;
            var currentValue = 1;
            var step = 1;

            spiral.Add(start);
            var currentNode = new SpiralElement();
            currentNode.position += DIR_RIGHT;
            currentNode.value = 1;
            spiral.Add(currentNode);

            while (currentValue < input)
            {
                var previousNode = currentNode;
                currentNode = new SpiralElement();

                Coord2D direction = new(0,0);
                if (!spiral.Any(x => x.position == cornerTopRight))
                    direction = DIR_UP;
                else if (!spiral.Any(x => x.position == cornerTopLeft))
                    direction = DIR_LEFT;
                else if (!spiral.Any(x => x.position == cornerBottomLeft))
                    direction = DIR_DOWN;
                else if (!spiral.Any(x => x.position == cornerBottomRight))
                    direction = DIR_RIGHT;
                else
                {
                    // We covered all the nodes, the spiral grows
                    direction = DIR_RIGHT;
                    step++;
                    cornerTopRight = referenceTopRight * step;
                    cornerTopLeft = referenceTopLeft * step;
                    cornerBottomRight = referenceBottomRight * step;
                    cornerBottomLeft = referenceBottomLeft * step;
                }

                currentNode.position = previousNode.position + direction;
                var neighs = currentNode.position.GetNeighbors8();
                currentNode.value = spiral.Where(x => neighs.Contains(x.position)).Sum(s => s.value);
                currentValue = currentNode.value;
                spiral.Add(currentNode);
            }
            return currentValue;
        }

        public int Solve(int part = 1)
            => (part == 1) ? FindDistance() : GenerateSpiral();
    }
}
