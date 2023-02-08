namespace AoC17.Day23
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

        public (int newIndex, long newValue) Run(Dictionary<string, long> registers, int part = 1,
                                                 Queue<long>? myQueue = null, Queue<long>? otherProgramQueue = null)
            => command switch
            {
                "set" => (index + 1, registers[op1] = getValue(op2, registers)),
                "sub" => (index + 1, registers[op1] -= getValue(op2, registers)),
                "mul" => (index + 1, registers[op1] *= getValue(op2, registers)),
                "jnz" => ((getValue(op1, registers) != 0) ? index + (int)getValue(op2, registers) : index + 1, (long)0),
                _ => throw new Exception("Invalid command")
            };

        public Instruction(string line, int index)
        {
            this.index = index;
            var groups = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            command = groups[0];
            op1 = groups[1];

            if (groups.Count() == 2)
                return;
            op2 = groups[2];
        }
    }


    internal class CoProAssembly
    {
        List<Instruction> program = new();

        public void ParseInput(List<string> lines)
        {
            for (int row = 0; row < lines.Count; row++)
                program.Add(new Instruction(lines[row], row));
        }

        long RunProgram()
        {
            int currentIndex = 0;
            Dictionary<string, long> registers = new();
            for (char c = 'a'; c <= 'h'; c++)
                registers[c.ToString()] = 0;

            int numMuls = 0;
            while (currentIndex < program.Count)
            {
                if (program[currentIndex].command == "mul")
                    numMuls++;
                (currentIndex, _) = program[currentIndex].Run(registers);
            }
            return numMuls;
        }

        public long Solve(int part = 1)
           => part == 1 ? RunProgram() : 0;
    }
}
