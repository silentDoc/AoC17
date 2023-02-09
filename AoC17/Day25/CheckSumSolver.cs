namespace AoC17.Day25
{
    enum Direction
    { 
        Left =0, 
        Right = 1,
    }

    record StateResolution
    {
        public int newValue;
        public Direction whereNext;
        public char nextState;
    }

    class State
    {
        char StateName = 'x';
        StateResolution resolution0;
        StateResolution resolution1;

        public State(char name, StateResolution res0, StateResolution res1)
        {
            StateName = name;
            resolution0 = res0;
            resolution1 = res1;
        }

        public StateResolution Resolve(int value)
            => value ==0 ? resolution0: resolution1;
    }

    internal class CheckSumSolver
    {
        char startingState = 'x';
        long steps = 0;
        Dictionary<char, State> states = new();
        Dictionary<long, int> values = new();

        void ParseState(List<string> lines)
        {
            var input = lines.Skip(1).ToList();
            char stateName = char.Parse( input[0].Replace("In state ", "").Replace(":", ""));
            int val0 = int.Parse(input[2].Replace("    - Write the value ","").Replace(".", ""));
            Direction dir0 = input[3].Replace("    - Move one slot to the ", "") == "right." ? Direction.Right : Direction.Left;
            char nextState0 = char.Parse(input[4].Replace("    - Continue with state ", "").Replace(".", ""));
            int val1= int.Parse(input[6].Replace("    - Write the value ", "").Replace(".", ""));
            Direction dir1 = input[7].Replace("    - Move one slot to the ", "") == "right." ? Direction.Right : Direction.Left;
            char nextState1 = char.Parse(input[8].Replace("    - Continue with state ", "").Replace(".", ""));

            StateResolution res0 = new StateResolution() { newValue = val0, nextState = nextState0, whereNext = dir0 };
            StateResolution res1 = new StateResolution() { newValue = val1, nextState = nextState1, whereNext = dir1 };

            State newState = new(stateName, res0, res1);
            states[stateName] = newState;
        }

        public void ParseInput(List<string> lines)
        {
            var header = lines.Take(2).ToList();
            startingState = header[0].Replace(".", "").Last();
            var stepsLine = header[1].Replace("Perform a diagnostic checksum after ", "").Replace(" steps.", "");
            steps = long.Parse(stepsLine);

            var turing = lines.Skip(2).ToList();

            var statesDescription = turing.Chunk(10);
            foreach (var stateDesc in statesDescription)
                ParseState(stateDesc.ToList());
        }

        int DiagnosticChecksum(int part = 1)
        {
            long currentPosition = 0;
            char currentState = startingState;
            values[0] = 0;
            for(long i =0; i<steps;i++)
            {
                if (!values.ContainsKey(currentPosition))
                    values[currentPosition] = 0;

                var value = values[currentPosition];
                var outcome = states[currentState].Resolve(value);

                values[currentPosition] = outcome.newValue;
                currentState = outcome.nextState;
                currentPosition += outcome.whereNext == Direction.Left ? -1 : +1;
            }
            return values.Values.Sum();
        }

        public int Solve(int part = 1)
            => DiagnosticChecksum(part);
    }
}
