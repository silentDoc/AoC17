using System.Diagnostics;

namespace AoC17.Day21
{
    public class FractalPattern
    {
        public char[][] InputPattern = null;
        List<char[][]> matches = new();
        public char[][] Output = null;
        int size = 0;

        public bool IsMatch(char[][] testPattern)
            => matches.Any(x => Match(x, testPattern));

        bool Match(char[][] input, char[][] test)
        {
            if (input.Length != test.Length)
                return false;

            var inputStrs = input.Select(x => new string(x)).ToList();
            var testStrs = test.Select(x => new string(x)).ToList();
            var retVal = true;
            for (int i = 0; i < inputStrs.Count; i++)
                retVal &= (inputStrs[i] == testStrs[i]);
            return retVal;
        }

        public FractalPattern(string input)
        {
            var parts = input.Split("=>", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var inputPatternGroup = parts[0].Split("/", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var outputPatternGroup = parts[1].Split("/", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            size = inputPatternGroup.Count();

            InputPattern = new char[size][];
            for(var i = 0; i < size; i++)
                InputPattern[i] = inputPatternGroup[i].ToCharArray();

            var outputSize = outputPatternGroup.Count();
            Output = new char[outputSize][];
            for (var i = 0; i < outputSize; i++)
                Output[i] = outputPatternGroup[i].ToCharArray();
            

            // Precalculate flips and rotations
            matches.Add(InputPattern);
            var rotate90 = InputPattern.Select(a => a.ToArray()).ToArray();     // Fast way to clone jagged arrays
            var rotate180 = InputPattern.Select(a => a.ToArray()).ToArray();    
            var rotate270 = InputPattern.Select(a => a.ToArray()).ToArray();    
            
            
            for (int row = 0; row < size; row++)
                for (int col = 0; col < size; col++)
                {
                    rotate90[row][col] = InputPattern[size - col-1][row];
                    rotate180[row][col] = InputPattern[size - row-1][size-col-1];
                    rotate270[row][col] = InputPattern[col][size - row - 1];
                    
                }
            matches.AddRange(new List<char[][]> { rotate90, rotate180, rotate270});
            var originalCount = matches.Count;
            // Add all the flips of the original and rotations
            for (int i=0; i< originalCount; i++)
            {
                var flipH = InputPattern.Select(a => a.ToArray()).ToArray();
                var flipV = InputPattern.Select(a => a.ToArray()).ToArray();
                for (int row = 0; row < size; row++)
                    for (int col = 0; col < size; col++)
                    {
                        flipH[row][col] = matches[i][size - row - 1][col];
                        flipV[row][col] = matches[i][row][size - col - 1];
                    }
                matches.Add(flipH);
                matches.Add(flipV);
            }
           

            
        }
    }

    internal class FractalGenerator
    {
        List<FractalPattern> patterns = new();
        char[][] fractalImage;

        public void ParseInput(List<string> lines)
            => lines.ForEach(line => patterns.Add(new FractalPattern(line)));

        char[][] FractalStep(char[][] currentImage)
        {
            var currentSize = currentImage.Length;
            var chunkSize = currentSize % 2 == 0 ? 2 : 3;
            var nextChunkSize = chunkSize == 2 ? 3 : 4;
            var chunkNum = currentSize / chunkSize;

            var nextSize = currentSize % 2 == 0 ? (currentSize / 2) * 3 : (currentSize / 3) * 4;
            char[][] nextImage = new char[nextSize][];
            for (int i= 0; i< nextSize; i++)
                nextImage[i] = new char[nextSize];

            var chunks = currentImage.Chunk(chunkSize); // groups of rows

            for (int r = 0; r < chunkNum; r++)
            {
                var selectedRows = currentImage[(r * chunkSize)..((r + 1) * chunkSize)];
                for (int c = 0; c < chunkNum; c++)
                {
                    char[][] in_pattern = new char[chunkSize][];
                    for (int k = 0; k < chunkSize; k++)
                        in_pattern[k] = selectedRows[k][(c * chunkSize)..((c + 1) * chunkSize)];

                    var matchingPattern = patterns.FirstOrDefault(x => x.IsMatch(in_pattern));

                    PrintFractal(in_pattern, true);
                    PrintFractal(matchingPattern.InputPattern, true);


                    for (int out_row = 0; out_row < nextChunkSize; out_row++)
                        for (int out_col = 0; out_col < nextChunkSize; out_col++)
                            nextImage[r * nextChunkSize + out_row][c * nextChunkSize + out_col] = matchingPattern.Output[out_row][out_col];
                }
            }

            return nextImage;
        }

        void PrintFractal(char[][] fractal, bool bTrace = false)
        {
            var strs = fractal.Select(x => new string(x)).ToList();
            if (bTrace)
            {
                strs.ForEach(s => Trace.WriteLine(s));
                Trace.WriteLine("");
                return;
            }

            strs.ForEach(s => Console.WriteLine(s));
            Console.WriteLine("");
        }

        int BuildFractal(int part = 1)
        {
            fractalImage = new char[3][];
            fractalImage[0] = (new string(".#.")).ToCharArray();
            fractalImage[1] = (new string("..#")).ToCharArray();
            fractalImage[2] = (new string("###")).ToCharArray();
            int rounds = (part == 1) ? 5 :0;

            PrintFractal(fractalImage);

            for (int i = 0; i < rounds; i++)
            {
                fractalImage = FractalStep(fractalImage);
                PrintFractal(fractalImage);
            }

            var litPixels = 0;
            foreach (var row in fractalImage)
                litPixels += row.Count(p => p == '#');

            return litPixels;
        }

        public int Solve(int part = 1)
            => BuildFractal(part);
    }
}
