using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace AoC17.Day07
{
    class AdventProgram
    {
        public string Name = "";
        public string Parent = "";
        public List<string> Children = new();
        public int Value;
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

                    node.Children.Add(strChild);
                }
            }
            programs.Add(node);
        }

        public void ParseInput(List<string> lines)
            => lines.ForEach(x => ParseLine(x));


        string FindRootElement(int part = 1)
            => programs.Where(x => string.IsNullOrEmpty(x.Parent)).First().Name;

        public string Solve(int part = 1)
            => FindRootElement(part);
    }
}
