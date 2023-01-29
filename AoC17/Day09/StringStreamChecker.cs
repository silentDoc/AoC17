namespace AoC17.Day09
{
    internal class StringStreamChecker
    {
        string text = "";
        public void ParseInput(List<string> lines)
            => text = lines[0];

        int CheckText(int part = 1)
        {
            var score = 0;
            var nestLevel = 0;
            bool inGarbage = false;
            int cancelledChars = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (inGarbage && text[i] == '!')
                {
                    i++;
                    continue;
                }

                if (inGarbage && text[i] != '>')
                {
                    cancelledChars++;
                    continue;
                }

                if (inGarbage && text[i] == '>')
                    inGarbage = false;
                
                if (!inGarbage && text[i] == '<')
                    inGarbage = true;
                if (text[i] == '{')
                    nestLevel++;
                if (text[i] == '}')
                {
                    score += nestLevel;
                    nestLevel--;
                }
            }
            return (part==1) ? score : cancelledChars;
        }

        public int Solve(int part = 1)
            => CheckText(part);
    }
}
