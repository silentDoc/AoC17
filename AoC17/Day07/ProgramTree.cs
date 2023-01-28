using System.Text.RegularExpressions;

namespace AoC17.Day07
{
    class AdventProgram
    {
        public string Name = "";
        public string Parent = "";
        public int Weight;
        public List<AdventProgram> Children = new();

        public int BalanceWeight
            => Weight + Children.Sum(x => x.BalanceWeight);

        public bool Balanced
            => Children.Select(x => x.BalanceWeight).Distinct().Count() == 1;
    }

    internal class ProgramTree
    {
        List<AdventProgram> programs = new();
     
        void ParseLine(string line)
        {
            Regex programRegex = new Regex(@"([a-z]+) \((\d+)\)( ->)?(.+)?");
            var groups = programRegex.Match(line).Groups;

            var name = groups[1].Value.Trim();
            var weight = int.Parse(groups[2].Value.Trim());

            var program = programs.Any(x => x.Name == name) ? programs.Where(x => x.Name == name).First() : new();
            program.Name = name;
            program.Weight = weight;

            programs.Add(program);
            if (string.IsNullOrEmpty(groups[4].Value))
                return;

            // We have children
            var childrenList = groups[4].Value.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach ( var strChild in childrenList ) 
            {
                var childrenProg = programs.Any(x => x.Name == strChild) ? programs.Where(x => x.Name == strChild).First() : new();
                childrenProg.Name = strChild;
                childrenProg.Parent = name;
                program.Children.Add(childrenProg);
                programs.Add(childrenProg);
            }
        }

        public void ParseInput(List<string> lines)
            => lines.ForEach(x => ParseLine(x));


        AdventProgram FindProblematicProg(AdventProgram? parentProg = null)
        {
            var currentProg = (parentProg == null) ? programs.First(x => string.IsNullOrEmpty(x.Parent)) : parentProg;
            if (!currentProg.Balanced)
            {
                // Find the dissident node
                var childrenWeights = currentProg.Children.Select(x => x.BalanceWeight).ToList();
                var weightCount = childrenWeights.Select(x => childrenWeights.Count(y => y == x)).ToList();
                var interestingIndex = weightCount.IndexOf(1);
                var interestingNode = currentProg.Children[interestingIndex];
                return FindProblematicProg(interestingNode);
            }
            // Our node has balanced children but was problematic 1 step ago, he is the culprit
            return currentProg;
        }

        int SolvePart2()
        {
            var problematicProg = FindProblematicProg();
            var parent = programs.First(x => x.Name == problematicProg.Parent);
            
            var childrenWeights = parent.Children.Select(x => x.BalanceWeight).ToList();
            var weightCount = childrenWeights.Select(x => childrenWeights.Count(y => y == x)).ToList();
            var difference = childrenWeights.Max() - childrenWeights.Min();

            if (problematicProg.BalanceWeight == childrenWeights.Max())
                return problematicProg.Weight - difference;
            else
                return problematicProg.Weight + difference;

        }

        string FindRootProg()
            => programs.Where(x => string.IsNullOrEmpty(x.Parent)).First().Name;

        public string Solve(int part = 1)
            => (part == 1) ? FindRootProg() : SolvePart2().ToString();
    }
}
