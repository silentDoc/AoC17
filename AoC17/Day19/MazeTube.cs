using System.Data;
using System.Text;

namespace AoC17.Day19
{
    internal class MazeTube
    {
        List<string> maze = new();

        enum Direction
        { 
            Up =0,
            Down =1,
            Left =2,
            Right =3
        }

        public void ParseInput(List<string> lines)
            => lines.ForEach(line => maze.Add(line));

        (int row, int col) Move( (int currentRow, int currentCol) pos, Direction dir)
            => dir switch
            {
                Direction.Up => (pos.currentRow - 1, pos.currentCol),
                Direction.Down => (pos.currentRow + 1, pos.currentCol),
                Direction.Left => (pos.currentRow, pos.currentCol - 1),
                Direction.Right => (pos.currentRow, pos.currentCol + 1),
                _ => throw new InvalidOperationException("Unknonwn direction " + dir.ToString())
            };

        char GetPos((int row, int col) pos)
            => maze[pos.row][pos.col];

        List<(int row, int col)> GetNeighbors((int row, int col) pos)
            => new List<(int row, int col)>() { (pos.row - 1, pos.col), (pos.row + 1, pos.col), (pos.row, pos.col - 1), (pos.row, pos.col + 1) };

        string NavigateMaze(int part=1)
        {
            int row = 0;
            int col = maze[0].IndexOf("|");
            var currentPosition = (row, col);
            var currentDirection = Direction.Down;
            int mazeWidth = maze.Max(x => x.Length);
            int mazeHeight = maze.Count;
            StringBuilder retVal = new();
            int steps = 0;
            while (true)
            {
                var previousPosition = currentPosition;
                currentPosition = Move(currentPosition, currentDirection);
                steps++;
                var posChar = GetPos(currentPosition);

                if (posChar == ' ')
                    break;
                
                if (char.IsAsciiLetter(posChar))
                    retVal.Append(posChar.ToString());
                if (posChar == '+')
                {
                    var surroundings = GetNeighbors(currentPosition).Where(x => x.row != previousPosition.row || x.col != previousPosition.col).ToList();
                    surroundings = surroundings.Where(x => x.col >= 0 && x.col < mazeWidth).ToList();
                    surroundings = surroundings.Where(x => x.row >= 0 && x.row < mazeHeight).ToList();

                    var next = surroundings.First(x => GetPos(x) == '|' || GetPos(x) == '-' || char.IsLetter(GetPos(x)));

                    if (next.row < currentPosition.row)
                        currentDirection = Direction.Up;
                    else if (next.row > currentPosition.row)
                        currentDirection = Direction.Down;
                    else if (next.col < currentPosition.col)
                        currentDirection = Direction.Left;
                    else if (next.col > currentPosition.col)
                        currentDirection = Direction.Right;
                }
            }
            return part ==1 ? retVal.ToString() : steps.ToString();
        }

        public string Solve(int part = 1)
            => NavigateMaze(part);
    }
}
