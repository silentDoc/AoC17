namespace AoC17.Day17
{
    internal class Spinlock
    {
        int skip = 0;

        public void ParseInput(List<string> lines)
            => skip = int.Parse(lines[0]);

        int InsertValues()
        {
            List<int> circularBuffer = new();
            circularBuffer.Add(0);
            int currentPosition = 0;
            for (int currentValue = 1; currentValue <= 2017; currentValue++)
            {
                currentPosition += skip +1;
                currentPosition %= circularBuffer.Count;
                circularBuffer.Insert(currentPosition, currentValue);
            }
            var pos2017 = circularBuffer.IndexOf(2017);
            return circularBuffer[(pos2017 +1) % circularBuffer.Count];
        }

        int FindValueAfterZero()
        {
            int currentPosition = 0;
            int positionOfZero = 0;
            int elementAfterZero = 0;
            int elementCount = 1;

            for (int currentValue = 1; currentValue <= 50000000; currentValue++)
            {
                currentPosition += skip;
                currentPosition %= elementCount;
                currentPosition++;

                if (currentPosition <= positionOfZero)
                    positionOfZero++;
                if (currentPosition == (positionOfZero + 1) % elementCount)
                    elementAfterZero = currentValue;
                elementCount++;
            }
            return elementAfterZero;
        }

        public int Solve(int part = 1)
            => part ==1 ? InsertValues() : FindValueAfterZero();
    }
}
