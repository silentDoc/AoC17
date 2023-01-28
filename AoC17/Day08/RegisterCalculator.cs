namespace AoC17.Day08
{
    class Operation
    {
        public string register = "";
        public string operation = "";
        public int value = 0;
        public string register_cond = "";
        public string comp_op = "";
        public int value_cond = 0;

        public void Run(Dictionary<string, int> registers)
        {
            if (!registers.ContainsKey(register))
                registers[register] = 0;
            if (!registers.ContainsKey(register_cond))
                registers[register_cond] = 0;

            var valComp = registers[register_cond];
            var compResult = comp_op switch
            {
                ">" => valComp > value_cond,
                "<" => valComp < value_cond,
                ">=" => valComp >= value_cond,
                "<=" => valComp <= value_cond,
                "==" => valComp == value_cond,
                "!=" => valComp != value_cond,
                _ => throw new InvalidOperationException("Unknown operation - " + comp_op)
            };

            if (!compResult)
                return;

            registers[register] += (operation == "inc") ? value : -1 * value;
        }
    }

    internal class RegisterCalculator
    {
        List<Operation> operations = new();

        Operation ParseLine(string line)
        { 
            Operation result = new();
            var parts = line.Replace("if", "").Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            result.register = parts[0];
            result.operation = parts[1];
            result.value = int.Parse(parts[2]);
            result.register_cond = parts[3];
            result.comp_op = parts[4];
            result.value_cond = int.Parse(parts[5]);
            return result;
        }

        public void ParseInput(List<string> lines)
            => lines.ForEach(x => operations.Add(ParseLine(x)));

        int RunProgram(int part = 1)
        { 
            Dictionary<string, int> registers = new();

            for (int i = 0; i < operations.Count; i++)
                operations[i].Run(registers);
            
            return registers.Values.Max();
        }

        public int Solve(int part = 1)
            => RunProgram(part);
    }
}
