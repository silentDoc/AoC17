using System.Diagnostics;

namespace AoC17.Day18
{
    class Instruction
    {
        public string command = "";
        string op1 = "";
        string op2 = "";
        int index;

        long getValue(string op, Dictionary<string, long> registers)
        {
            long retVal = 0;
            if (!long.TryParse(op, out retVal))
                retVal = registers[op];
            return retVal;
        }
       
        public (int newIndex, long newValue) Run(Dictionary<string, long> registers)
            => command switch
            {
                "snd" => (index + 1, registers["lastSound"] = getValue(op1, registers)),
                "set" => (index + 1, registers[op1] = getValue(op2, registers)),
                "add" => (index + 1, registers[op1] += getValue(op2, registers)),
                "mul" => (index + 1, registers[op1] *= getValue(op2, registers)),
                "mod" => (index + 1, registers[op1] %= getValue(op2, registers)),
                "rcv" => (index + 1, registers[op1]!=0 ? registers[op1] = registers["received"] = registers["lastSound"] : -1),
                "jgz" => ((getValue(op1, registers) > 0) ? index + (int) getValue(op2, registers) : index + 1, (long) 0),
                _ => throw new Exception("Invalid command")
            };

        public Instruction(string line, int index)
        {
            this.index = index;
            var groups = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            command = groups[0];
            op1 = groups[1];

            if (groups.Count() ==2)
                return;
            op2 = groups[2];
        }
    }

    internal class DuetAssembly
    {
        Dictionary<string, long> registers = new();
        List<Instruction> program = new();

        public void ParseInput(List<string> lines)
        {
            for (int row = 0; row < lines.Count; row++)
                program.Add(new Instruction(lines[row], row));
        }

        long RunProgram(int part = 1)
        {
            int currentIndex = 0;
            for (char c = 'a'; c < 'z'; c++)    
                registers[c.ToString()] = 0;

            while (currentIndex < program.Count)
            {
                (currentIndex, _) = program[currentIndex].Run(registers);

                if (registers.ContainsKey("received"))
                    return registers["received"];
            }

            return -1;
        }

        public long Solve(int part = 1)
            => RunProgram(part);
    }
}

