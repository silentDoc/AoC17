using System.Diagnostics;

namespace AoC17
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int day = 18;
            int part = 1;
            bool test = !false;

            string input = "./Input/day" + day.ToString("00");
            input += (test) ? "_test.txt" : ".txt";

            Console.WriteLine("AoC 2017 - Day {0} , Part {1} - Test Data {2}", day, part, test);
            Stopwatch st = new();
            st.Start();
            string result = day switch
            {
                1 => day1(input, part).ToString(),
                2 => day2(input, part).ToString(),
                3 => day3(input, part).ToString(),
                4 => day4(input, part).ToString(),
                5 => day5(input, part).ToString(),
                6 => day6(input, part).ToString(),
                7 => day7(input, part).ToString(),
                8 => day8(input, part).ToString(),
                9 => day9(input, part).ToString(),
                10 => day10(input, part).ToString(),
                11 => day11(input, part).ToString(),
                12 => day12(input, part).ToString(),
                13 => day13(input, part).ToString(),
                14 => day14(input, part).ToString(),
                15 => day15(input, part).ToString(),
                16 => day16(input, part).ToString(),
                17 => day17(input, part).ToString(),
                18 => day18(input, part).ToString(),
                _ => throw new ArgumentException("Wrong day number - unimplemented")
            };
            st.Stop();
            Console.WriteLine("Result : {0}", result);
            Console.WriteLine("Ellapsed : {0}", st.Elapsed.TotalSeconds);
        }

        static int day1(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day01.InverseCaptcha ic = new();
            ic.ParseInput(lines);

            return ic.Solve(part);
        }

        static int day2(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day02.CheckSumSolver checksum = new();
            checksum.ParseInput(lines);

            return checksum.Solve(part);
        }

        static int day3(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day03.SpiralMemory spiral = new();
            spiral.ParseInput(lines);
            
            return spiral.Solve(part);
        }
        
        static int day4(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day04.PassChecker passChecker = new();
            passChecker.ParseInput(lines);
            
            return passChecker.Solve(part);
        }

        static int day5(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day05.CPUJumper jumper = new();
            jumper.ParseInput(lines);

            return jumper.Solve(part);
        }

        static int day6(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day06.MemoryReallocator memoRealloc = new();
            memoRealloc.ParseInput(lines);

            return memoRealloc.Solve(part);
        }

        static string day7(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day07.ProgramTree progTree = new();
            progTree.ParseInput(lines);

            return progTree.Solve(part);
        }

        static int day8(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day08.RegisterCalculator calc = new();
            calc.ParseInput(lines);

            return calc.Solve(part);
        }

        static int day9(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day09.StringStreamChecker checker = new();
            checker.ParseInput(lines);
            return checker.Solve(part);
        }

        static string day10(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day10.KnotHasher hasher = new();
            hasher.ParseInput(lines);
            return hasher.Solve(part);
        }

        static int day11(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day11.HexNavigator navi = new();
            navi.ParseInput(lines);
            return navi.Solve(part);
        }

        static int day12(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day12.DigitalPlumber plumber = new();
            plumber.ParseInput(lines);
            return plumber.Solve(part);
        }

        static int day13(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day13.Firewall firewall = new();
            firewall.ParseInput(lines);
            return firewall.Solve(part);
        }

        static int day14(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day14.Defragmenter defrag = new();
            defrag.ParseInput(lines);
            return defrag.Solve(part);
        }
        
        static int day15(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day15.GeneratorDuel duel = new();
            duel.ParseInput(lines);
            return duel.Solve(part);
        }

        static string day16(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day16.ProgDancer dancer = new();
            dancer.ParseInput(lines);
            return dancer.Solve(part);
        }

        static int day17(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            Day17.Spinlock spin = new();
            spin.ParseInput(lines);
            return spin.Solve(part);
        }

        static int day18(string input, int part)
        {
            var lines = File.ReadAllLines(input).ToList();
            return 0;
        }
    }
}