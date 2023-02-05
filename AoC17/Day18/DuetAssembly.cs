using Microsoft.Win32;
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

        long sendToOtherProgram(Queue<long>? otherProgramQueue, long value)
        {
            otherProgramQueue.Enqueue(value);
            return value;
        }

        public (int newIndex, long newValue) Run(Dictionary<string, long> registers, int part = 1,
                                                 Queue<long>? myQueue = null, Queue<long>? otherProgramQueue = null)
            => command switch
            {
                "snd" => part == 1
                                ? (index + 1, registers["lastSound"] = getValue(op1, registers))
                                : (index + 1, sendToOtherProgram(otherProgramQueue, getValue(op1, registers))),
                "set" => (index + 1, registers[op1] = getValue(op2, registers)),
                "add" => (index + 1, registers[op1] += getValue(op2, registers)),
                "mul" => (index + 1, registers[op1] *= getValue(op2, registers)),
                "mod" => (index + 1, registers[op1] %= getValue(op2, registers)),
                "rcv" => part == 1
                                ? (index + 1, registers[op1] != 0 ? registers[op1] = registers["received"] = registers["lastSound"] : -1)
                                : myQueue.Count > 0 ? (index + 1, registers[op1] = myQueue.Dequeue()) : (index, -1234567),
                "jgz" => ((getValue(op1, registers) > 0) ? index + (int)getValue(op2, registers) : index + 1, (long)0),
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

        long RunPrograms()
        {
            Dictionary<string, long> registers_0 = new();
            Dictionary<string, long> registers_1 = new();
            Queue<long> received_0 = new();
            Queue<long> received_1 = new();
            int currentIndex_0 = 0;
            int currentIndex_1 = 0;
            long retVal1 = 0;
            long retVal2 = 0;
            bool inDeadlock = false;

            for (char c = 'a'; c < 'z'; c++)
                registers_0[c.ToString()] = registers_1[c.ToString()] = 0;

            registers_0["p"] = 0;
            registers_1["p"] = 1;

            int numProgram1Sends = 0;

            while (currentIndex_0<program.Count && currentIndex_1<program.Count && !inDeadlock)
            {
                (currentIndex_0, retVal1) = program[currentIndex_0].Run(registers_0, 2, received_0, received_1);
                numProgram1Sends += program[currentIndex_1].command == "snd" ? 1 : 0;
                (currentIndex_1, retVal2) = program[currentIndex_1].Run(registers_1, 2, received_1, received_0);

                inDeadlock = retVal1 == -1234567 && retVal2 == -1234567;
            }
            return numProgram1Sends;

        }

        public long Solve(int part = 1)
            => part == 1 ? RunProgram() : RunPrograms();
    }
}

