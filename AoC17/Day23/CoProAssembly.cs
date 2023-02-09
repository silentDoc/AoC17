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

        long RunProgram(int part =1)
        {
            if (part == 2)
                return CodeInInput5();

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

        // Using gotos in 2023. You gotta laugh :)
        long CodeInInput()
        {
            Dictionary<string, long> registers = new();
            for (char c = 'a'; c <= 'h'; c++)
                registers[c.ToString()] = 0;
            registers["a"] = 1;

            registers["b"] = 99;                //0
            registers["c"] = registers["b"];    //1
            if (registers["a"] != 0)            //2
                goto plus2_0;
            if (1 != 0)                         //3
                goto plus5;
            plus2_0:
            registers["b"] *= 100;              //4
            registers["b"] -= -100000;          //5
            registers["c"] = registers["b"];    //6
            registers["c"] -= -17000;           //7
            plus5:
            minus_23:
            registers["f"] = 1;                 //8
            registers["d"] = 2;                 //9
            minus13:
            registers["e"] = 2;                 //10
            minus8:
            registers["g"] = registers["d"];    //11
            registers["g"] *= registers["e"];   //12
            registers["g"] -= registers["b"];   //13
            if (registers["g"] != 0)            //14
                goto plus2_1;
            registers["f"] = 0;                 //15
            plus2_1:
            registers["e"] -= 1;                //16
            registers["g"] = registers["e"];    //17
            registers["g"] -= registers["b"];   //18
            if (registers["g"] != 0)            //19
                goto minus8;
            registers["d"] -= 1;                //20
            registers["g"] = registers["d"];    //21
            registers["g"] -= registers["b"];    //22
            if (registers["g"] != 0)            //23
                goto minus13;
            if (registers["f"] != 0)            //24
                goto plus_2_2;
            registers["h"] -= 1;                //25
            plus_2_2:
            registers["g"] = registers["b"];    //26
            registers["g"] -= registers["c"];   //27
            if (registers["g"] != 0)            //28
                goto plus_2_3;
            if (1 != 0)                         //29
                goto plus_3;
            plus_2_3:
            registers["b"] -= 17;               //30
            if (1 != 0)                         //31
                goto minus_23;
            plus_3:
            Console.WriteLine("Part 2 - Finished execution - h Equals: " + registers["h"].ToString());
            return registers["h"];
        }

        long CodeInInput2()
        {
            long a, b, c, d, e, f, g, h;
            a = b = c = d = e = f = g = h = 0;

            b = 99;                      // set b 99
            c = b;                       // set c b
            if (a != 0)
            {                           // jnz a 2
                                        // jnz 1 5
                b *= 100;               // mul b 100
                b += 100000;            // sub b -100000
                c = b;                  // set c b
                c += 17000;             // sub c -17000
            }
            do
            {
                f = 1;                  // set f 1
                d = 2;                  // set d 2
                do
                {
                    e = 2;               // set e 2
                    do
                    {
                        g = d;         // set g d
                        g *= e;          // mul g e
                        g -= b;          // sub g b
                        if (g == 0)    // jnz g 2
                            f = 0;       // set f 0
                        e++;             // sub e -1
                        g = e;           // set g e
                        g -= b;          // sub g b
                    } while (g != 0);    // jnz g -8
                    d++;                 // sub d -1
                    g = d;               // set g d
                    g -= b;              // sub g b
                } while (g != 0);        // jnz g -13
                if (f == 0)              // jnz f 2
                    h++;                 // sub h -1
                g = b;                   // set g b // these last 4 lines mean loop x1 in part 1 and x1000 in part 2
                g -= c;                  // sub g c
                if (g == 0)            // jnz g 2
                    return h;            // jnz 1 3
                b += 17;                 // sub b -17
            } while (true);
        }

        long CodeInInput3()
        {
            long a, b, c, d, e, f, g, h;
            a = b = c = d = e = f = g = h = 0;

            b = 99;                      // set b 99
            c = b;                       // set c b
            if (a != 0)
            {                           // jnz a 2
                                        // jnz 1 5
                b = (b * 100) + 100000; // mul b 100 // sub b -100000
                c = b + 17000;          // set c b // sub c -17000
                c += 17000;             
            }
            do
            {
                f = 1;                  // set f 1
                d = 2;                  // set d 2
                do
                {
                    e = 2;               // set e 2
                    do
                    {
                        g = d * e -b;   // set g d
                        if (g == 0)    // jnz g 2
                            f = 0;       // set f 0
                        e++;             // sub e -1
                        g = e -b;           // set g e
                    } while (g != 0);    // jnz g -8

                    d++;                 // sub d -1
                    g = d - b;               // set g d
                } while (g != 0);        // jnz g -13

                if (f == 0)              // jnz f 2
                    h++;                 // sub h -1
                g = b -c;                   // set g b // these last 4 lines mean loop x1 in part 1 and x1000 in part 2
                if (g == 0)            // jnz g 2
                    return h;            // jnz 1 3
                b += 17;                 // sub b -17
            } while (true);
        }

        long CodeInInput4()
        {
            long f, h, b, d, e;
            f = h = 0;
            for (b = 109900; b != 126900; b += 17)
            {
                f = 1;                 
                for (d = 2; d != b; d++)
                {
                    for (e = 2; e != b; e++)
                    {
                        if ((d * e) == b)       
                        {
                            // We are trying to find combinations so that d*e (less than b) equals b
                            // So that if b can be retrieved by factors, we put f=0 --> we increment h
                            f = 0;
                            // We are finding the NON PRIME numbers in this range
                        }
                    }
                }
                if (f == 0)              
                    h++;                
            }
            return h;
        }

        bool IsPrime(int num)
        {
            for (int a = 2; a <= Math.Sqrt(num); a++)
                if (num % a == 0)
                    return false;
            return true;
        }

        long CodeInInput5()
        {
            long h=0;
            for (int b = 109900; b <= 126900; b += 17)
                if (!IsPrime(b))
                    h++;
            return h;
        }

        public long Solve(int part = 1)
           =>  RunProgram(part);
    }
}
