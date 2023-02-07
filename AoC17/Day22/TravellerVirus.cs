using AoC17.Common;

namespace AoC17.Day22
{
    class GridNode
    {
        public Coord2D Position = new(0,0); // x, y
        public bool Infected;

        public GridNode(int col, int row, bool infected)
        {
            Position = new(col, row);
            Infected = infected;
        }
    }

    enum Direction
    { 
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    internal class TravellerVirus
    {
        List<GridNode> cluster = new();

        void ParseLine(string line, int currentRow)
        {
            var size = line.Trim().Length;
            var firstIndex = (size - 1) / 2;
            for (int i = 0; i < size; i++)
                cluster.Add(new GridNode(i - firstIndex, currentRow - firstIndex, line[i] == '#'));
        }

        public void ParseInput(List<string> lines)
        {
            for (int row = 0; row < lines.Count; row++)
                ParseLine(lines[row], row);
        }

        Coord2D Move(Direction dir)
            => dir switch
            {
                Direction.Up => new Coord2D(0, -1),
                Direction.Right => new Coord2D(1, 0),
                Direction.Down => new Coord2D(0, 1),
                Direction.Left => new Coord2D(-1, 0),
                _ => throw new InvalidDataException("Unknown direction - " + dir.ToString())
            };

        Direction TurnRight(Direction dir)
            => dir switch
            {
                Direction.Up => Direction.Right,
                Direction.Right => Direction.Down,
                Direction.Down => Direction.Left,
                Direction.Left => Direction.Up,
                _ => throw new InvalidDataException("Unknown direction - " + dir.ToString())
            };

        Direction TurnLeft(Direction dir)
           => dir switch
           {
               Direction.Up => Direction.Left,
               Direction.Right => Direction.Up,
               Direction.Down => Direction.Right,
               Direction.Left => Direction.Down,
               _ => throw new InvalidDataException("Unknown direction - " + dir.ToString())
           };

        int CountInfections(int part = 1)
        {
            var numBursts = part == 1 ? 10000 : 0;
            var currentPosition = new Coord2D(0, 0);
            var currentDirection = Direction.Up;
            var infectionCount = 0;

            for (int burst = 0; burst < numBursts; burst++)
            {
                var node = cluster.FirstOrDefault(x => x.Position == currentPosition);
                if (node == null)
                {
                    node = new GridNode(currentPosition.x, currentPosition.y, false);
                    cluster.Add(node);
                }

                currentDirection = (node.Infected) ? TurnRight(currentDirection) : TurnLeft(currentDirection);
                node.Infected = !node.Infected;
                if (node.Infected) 
                    infectionCount++;
                currentPosition += Move(currentDirection);
            }
            return infectionCount;
        }

        public int Solve(int part = 1)
            => CountInfections(part);
    }
}
