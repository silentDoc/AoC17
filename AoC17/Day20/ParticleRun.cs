using AoC17.Common;
using System.Text.RegularExpressions;

namespace AoC17.Day20
{
    class Particle
    {
        public int id;
        public Coord3D position= new Coord3D(0,0,0); 
        public Coord3D velocity = new Coord3D(0, 0, 0); 
        public Coord3D acceleration = new Coord3D(0, 0, 0);

        public void Move()
        {
            velocity += acceleration;
            position += velocity;
        }

        // I am not sure if it this was the way to implement part1, but physics tells me that in the long run, 
        // the particle with least acceleration magnitude will stay the closest to origin. 
        public double AccelModule   
            => acceleration.VectorModule;
    }

    internal class ParticleRun
    {
        List<Particle> particles = new List<Particle>();

        Particle ParseLine(string line, int row)
        {
            Particle retVal = new();
            Regex regexParticle = new Regex(@"<(\s?-?\d+),(\s?-?\d+),(\s?-?\d+)>, v=<(\s?-?\d+),(\s?-?\d+),(\s?-?\d+)>, a=<(\s?-?\d+),(\s?-?\d+),(\s?-?\d+)>");
            var groups = regexParticle.Match(line).Groups;
            retVal.position = new Coord3D(int.Parse(groups[1].Value), int.Parse(groups[2].Value), int.Parse(groups[3].Value));
            retVal.velocity = new Coord3D(int.Parse(groups[4].Value), int.Parse(groups[5].Value), int.Parse(groups[6].Value));
            retVal.acceleration = new Coord3D(int.Parse(groups[7].Value), int.Parse(groups[8].Value), int.Parse(groups[9].Value));
            retVal.id = row;
            return retVal;
        }

        public void ParseInput(List<string> lines)
        {
            for (int i = 0; i < lines.Count; i++)
                particles.Add(ParseLine(lines[i], i));
        }

        public int RemoveCollisions()
        {
            int numRounds = 50;   // Started with 20000 - stabilizes at 39
            for (int i = 0; i < numRounds; i++)
            {
                particles.ForEach(x => x.Move());
                var posCounts = particles.Select(x => x.position).ToList().GroupBy(x => x)
                                         .ToDictionary(y => y.Key, y => y.Count())
                                         .OrderByDescending(z => z.Value);

                var collisions = posCounts.Where(x => x.Value>1).Select(x => x.Key).ToList();

                foreach (var collision in collisions)
                {
                    var particlesToRemove = particles.Where(x => x.position == collision).ToList();
                    foreach (var collidingParticle in particlesToRemove)
                        particles.Remove(collidingParticle);
                }
            }
            return particles.Count;
        }

        public int FindClosest()
        {
            var minAccel = particles.Min(x => x.AccelModule);
            var candidates = particles.Where(x => x.AccelModule == minAccel).ToList();
            return candidates[0].id;    // My input yielded a unique minimum
        }

        public int Solve(int part = 1)
            => part == 1 ? FindClosest() : RemoveCollisions();
    }
}
