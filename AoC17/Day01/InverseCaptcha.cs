namespace AoC17.Day01
{
    internal class InverseCaptcha
    {
        string input = "";

        public void ParseInput(List<string> lines)
            => input = lines[0];

        int FindCaptcha(int part = 1)
        {
            var offset = (part ==1) ? 1 : input.Length/2;
            return input.Select((x, index) => x == input[(index + offset) % input.Length] ? int.Parse(x.ToString()) : 0).Sum();
        }

        public int Solve(int part = 1)
            => FindCaptcha(part);
    }
}
