using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace AoC17.Day07
{
    class AdventProgram
    {
        public string Name = "";
        public string Parent = "";
        public List<AdventProgram> Children = new();
        public int Value;

        public int BalanceWeight
            => Value + Children.Sum(x => x.BalanceWeight);

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
            var size = int.Parse(groups[2].Value.Trim());

            var node = programs.Any(x => x.Name == name) ? programs.Where(x => x.Name == name).First() : new();

            node.Name = name;
            node.Value = size;

            if (!string.IsNullOrEmpty(groups[3].Value))
            {
                var childrenList = groups[4].Value.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                foreach ( var strChild in childrenList ) 
                {
                    var childrenNode = programs.Any(x => x.Name == strChild) ? programs.Where(x => x.Name == strChild).First() : new();
                    childrenNode.Name = strChild;
                    childrenNode.Parent = name;
                    programs.Add(childrenNode);

                    node.Children.Add(childrenNode);
                }
            }
            programs.Add(node);
        }

        public void ParseInput(List<string> lines)
            => lines.ForEach(x => ParseLine(x));


        AdventProgram FindProblematicNode(AdventProgram? parentNode = null)
        {
            var currentNode = (parentNode == null) ? programs.First(x => string.IsNullOrEmpty(x.Parent)) : parentNode;
            if (!currentNode.Balanced)
            {
                // Find the dissident node
                var childrenWeights = currentNode.Children.Select(x => x.BalanceWeight).ToList();
                var weightCount = childrenWeights.Select(x => childrenWeights.Count(y => y == x)).ToList();
                var interestingIndex = weightCount.IndexOf(1);
                var interestingNode = currentNode.Children[interestingIndex];

                return FindProblematicNode(interestingNode);
            }
            // Our node has balanced children but was problematic 1 step ago, he is the culprit
            return currentNode;
        }

        int SolvePart2()
        {
            var problematicNode = FindProblematicNode();
            var parent = programs.First(x => x.Name == problematicNode.Parent);
            
            var childrenWeights = parent.Children.Select(x => x.BalanceWeight).ToList();
            var weightCount = childrenWeights.Select(x => childrenWeights.Count(y => y == x)).ToList();
            var difference = childrenWeights.Max() - childrenWeights.Min();

            if (problematicNode.BalanceWeight == childrenWeights.Max())
                return problematicNode.Value - difference;
            else
                return problematicNode.Value + difference;

        }

        string FindRootElement()
            => programs.Where(x => string.IsNullOrEmpty(x.Parent)).First().Name;

        public string Solve(int part = 1)
            => (part == 1) ? FindRootElement() : SolvePart2().ToString();
    }
}
