using AoC17.Common;

namespace AoC17.Day22
{
    class GridNode
    {
        public Coord2D Position = new(0,0); // x, y
        public bool Infected;
        public int Status;  // 0 - clean, 1 - weakened , 2-infected , 3 - flagged

        public GridNode(int col, int row, bool infected)
        {
            Position = new(col, row);
            Infected = infected;
            Status = infected ? 2 : 0;
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

        Direction Reverse(Direction dir)
           => dir switch
           {
               Direction.Up => Direction.Down,
               Direction.Right => Direction.Left,
               Direction.Down => Direction.Up,
               Direction.Left => Direction.Right,
               _ => throw new InvalidDataException("Unknown direction - " + dir.ToString())
           };

        int CountInfections(int part = 1)
        {
            var numBursts = part == 1 ? 10000 : 10000000;
            var currentPosition = new Coord2D(0, 0);
            var currentDirection = Direction.Up;
            var infectionCount = 0;

            // Optimizations for part 2 - use of a hashset and a dictionary instead of the list
            // and avoiding enums and switches and using sums and module instead
            HashSet<Coord2D> knownPositions = new();            
            Dictionary<Coord2D, GridNode> lookup = new();
            foreach (var node in cluster)
            {
                knownPositions.Add(node.Position);
                lookup[node.Position] = node;
            }

            for (int burst = 0; burst < numBursts; burst++)
            {
                var isNewNode = knownPositions.Add(currentPosition);
                var node = (isNewNode) ? new GridNode(currentPosition.x, currentPosition.y, false) : lookup[currentPosition];

                if (isNewNode)
                    lookup[currentPosition] = node;

                if (part == 1)
                {
                    currentDirection = (node.Infected) ? TurnRight(currentDirection) : TurnLeft(currentDirection);
                    node.Infected = !node.Infected;
                    if (node.Infected)
                        infectionCount++;
                }
                else  // Part 2
                {
                    currentDirection = node.Status switch
                    {
                        0 => TurnLeft(currentDirection),
                        1 => currentDirection,
                        2 => TurnRight(currentDirection),
                        3 => Reverse(currentDirection),
                        _ => throw new InvalidDataException("Unknown status - " + node.Status.ToString())
                    };

                    node.Status = (node.Status+1) %4;
                    if (node.Status == 2)
                        infectionCount++;
                }
                currentPosition += Move(currentDirection);
            }
            return infectionCount;
        }

        public int Solve(int part = 1)
            => CountInfections(part);
    }
}
