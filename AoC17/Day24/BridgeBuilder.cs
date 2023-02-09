namespace AoC17.Day24
{

    class Component
    {
        public int Left;
        public int Right;
        public int Free = -1;

        public bool Elegible(int pins)
            => Left == pins || Right == pins;

        public void Connect(int pins)
            => Free = (Left == pins) ? Right : Left;

        public Component()
            => Left = Right = Free = -1;

        public int Strength
            => Left + Right;
        
        public Component(string line)
        {
            var groups = line.Split("/", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            Left = int.Parse(groups[0]);
            Right = int.Parse(groups[1]);
        }

        public Component Clone()
        {
            Component retVal = new();
            retVal.Left = Left;
            retVal.Right = Right;
            retVal.Free = Free;
            return retVal;
        }
    }

    internal class BridgeBuilder
    {
        List<Component> components = new();

        public void ParseInput(List<string> lines)
            => lines.ForEach(x => components.Add(new Component(x)));

        IEnumerable<List<Component>> BuildBridge(List<Component> usedComponents, List<Component> avaialableComponents)
        {
            if (usedComponents.Count == 0)
            {
                var startingNodes = components.Where(x => x.Elegible(0)).ToList();
                foreach (var start in startingNodes)
                {
                    start.Connect(0);
                    var used = new List<Component>() { start.Clone() };
                    var rest = avaialableComponents.Where(x => x != start).ToList();
                    var restCopy = new List<Component>();
                    foreach (var comp2 in rest)
                        restCopy.Add(comp2.Clone());
                    foreach (var bridge in BuildBridge(used, restCopy))
                        yield return bridge;
                }
            }
            else
            {

                var lastComponent = usedComponents.Last();
                var validNextComponents = avaialableComponents.Where(x => x.Elegible(lastComponent.Free)).ToList();

                if (validNextComponents.Count == 0)     // No more components available, we've reached the bridge end
                    yield return usedComponents;
                else
                {
                    // We're not returning ALL the bridges, only the ones that have no more elements to be added. It does not affect the solutions
                    foreach (var nextComponent in validNextComponents)
                    {
                        nextComponent.Connect(lastComponent.Free);
                       
                        var newList = new List<Component>();
                        foreach (var comp in usedComponents)
                            newList.Add(comp.Clone());
                        newList.Add(nextComponent.Clone());

                        var rest = avaialableComponents.Where(x => x != nextComponent).ToList();
                        var restCopy = new List<Component>();
                        foreach (var comp2 in rest)
                            restCopy.Add(comp2.Clone());
                        foreach (var bridge in BuildBridge(newList, restCopy))
                            yield return bridge;
                    }
                }
            }
        }

        int BridgeStrength(List<Component> bridge)
            => bridge.Sum( x => x.Strength);

        int FindStrongestBridge(int part = 1)
        {
            var bridges = BuildBridge(new List<Component>(), components).ToList();

            var longestBridgeCount = bridges.Max(bridge => bridge.Count);

            return (part == 1) ? bridges.Max(bridge => BridgeStrength(bridge)) 
                               : bridges.Where(x => x.Count == longestBridgeCount).Max(x => BridgeStrength(x));
        }

        public int Solve(int part = 1)
            => FindStrongestBridge(part);
    }
}
