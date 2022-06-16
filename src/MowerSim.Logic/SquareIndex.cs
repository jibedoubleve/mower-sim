using System.Diagnostics;

namespace MowerSim.Logic
{
    [DebuggerDisplay("Index {Index}, {Direction}")]
    public record SquareIndex
    {
        public SquareIndex(int index, Directions direction)
        {
            Direction = direction;
            Index = index;
        }

        public static SquareIndex Empty { get => new(-1, Directions.Down); }

        public Directions Direction { get; }

        public int Index { get; }

        public static implicit operator int(SquareIndex coord) => coord.Index;
    }
}