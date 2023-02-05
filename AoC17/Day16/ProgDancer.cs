using System.Text;

namespace AoC17.Day16
{
    class DanceMove
    {
        public char move = '.';
        public string op1 = "";
        public string op2 = "";

        public string Move(string progList)
        {
            StringBuilder sb = new();
            int pos1 = 0;
            int pos2 = 0;
            switch (move)
            {
                case 's':
                    int shift = int.Parse(op1) % progList.Length;
                    sb.Append(progList.Substring(progList.Length - shift) + progList.Substring(0, progList.Length-shift));
                    break;
                case 'x':
                    pos1 = int.Parse(op1);
                    pos2 = int.Parse(op2);
                    sb = new(progList);
                    sb[pos1] = progList[pos2];
                    sb[pos2] = progList[pos1];
                    break;
                case 'p':
                    pos1 = progList.IndexOf(char.Parse(op1));
                    pos2 = progList.IndexOf(char.Parse(op2));
                    sb = new(progList);
                    sb[pos1] = progList[pos2];
                    sb[pos2] = progList[pos1];
                    break;
                default:
                    throw new InvalidOperationException("Unknown move - " + move.ToString());
            }
            return sb.ToString();
        }
    }

    internal class ProgDancer
    {
        List<DanceMove> moves = new();
        string proglist = "abcdefghijklmnop";
        DanceMove ParseLine(string line)
        {
            DanceMove dmove = new();
            dmove.move = line[0];

            if (line[0] == 's')
                dmove.op1 = line.Substring(1).Trim();
            else
            {
                var ops = line.Substring(1).Split("/");
                dmove.op1 = ops[0].Trim();
                dmove.op2 = ops[1].Trim();
            }
            return dmove;
        }

        public void ParseInput(List<string> lines)
            => lines[0].Split(",").ToList().ForEach(line => moves.Add(ParseLine(line)));

        string DoDance()
        {
            foreach (var dancemove in moves)
                proglist = dancemove.Move(proglist);
            return proglist;
        }

        string DanceForever()
        {
            List<string> states = new();
            int loop = 0;
            while (!states.Contains(proglist))
            {
                states.Add(proglist);
                proglist = DoDance();
                loop++;
            }

            int firstIndex = proglist.IndexOf(proglist);
            int loopSize = loop - firstIndex;

            int remainder = (1000000000 - firstIndex) % loopSize;
            return states[firstIndex + remainder];
        }


        public string Solve(int part = 1)
            => part == 1 ? DoDance() : DanceForever();
    }
}
