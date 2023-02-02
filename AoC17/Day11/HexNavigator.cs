using AoC17.Common;

namespace AoC17.Day11
{
    // Today's problem solution credit goes to this page : https://www.redblobgames.com/grids/hexagons/
    // Using 3d dimensions, axis are N-S ; NW - SE ; NE - SW

    internal class HexNavigator
    {
        List<string> path = new();

        Coord3D Move(Coord3D position, string direction)
            => direction switch
            {
                "n" => position + new Coord3D(0, 1, -1),    // 1st axis
                "s" => position + new Coord3D(0, -1, 1),
                "se" => position + new Coord3D(1, -1, 0),   // 2nd axis
                "nw" => position + new Coord3D(-1, 1, 0),
                "ne" => position + new Coord3D(1, 0, -1),   // 3rd axis
                "sw" => position + new Coord3D(-1, 0, 1),
                _ => throw new InvalidOperationException("Unknown direction - " + direction)
            };

        void ParseDirs(string line)
        {
            var groups = line.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            groups.ToList().ForEach(x => path.Add(x));
        }

        public void ParseInput(List<string> lines)
            => ParseDirs(lines[0]);

        int FollowPath(int part =1)
        {
            Coord3D currentPosition = new Coord3D(0, 0, 0);
            int maxDistance = 0;
            int currentDistance = 0;
            foreach (var step in path)
            {
                currentPosition = Move(currentPosition, step);
                currentDistance = Math.Max(Math.Abs(currentPosition.x), Math.Max(Math.Abs(currentPosition.y), Math.Abs(currentPosition.x)));
                maxDistance = Math.Max(maxDistance, currentDistance);
            }
            
            return part ==1 ? currentDistance : maxDistance;
        }

        public int Solve(int part = 1)
            => FollowPath(part);
    }
}
