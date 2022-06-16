using MowerSim.Logic.MowingPolicies;

namespace MowerSim.Logic
{
    public class Mower : IMowerAPI
    {
        #region Fields

        private static readonly object locker = new();
        private int _index;

        #endregion Fields

        #region Constructors

        public Mower(int rows, int columns)
        {
            Validator = new(rows, columns);
            Navigator = new(rows, columns);
        }

        public Mower(int side, int index, Directions lastDirection) : this(side)
        {
            _index = index;
            LastDirection = lastDirection;
        }

        public Mower(int side) : this(side, side)
        {
        }

        #endregion Constructors

        #region Properties

        private Func<int, bool> LastValidation { get; set; }
        public int Index => _index;
        public Directions LastDirection { get; private set; }
        public IndexCalculator Navigator { get; }
        public IndexValidator Validator { get; }

        #endregion Properties

        #region Methods

        private int AvoidObstacle(IMowingPolicy policy)
        {
            var coord = policy.AvoidObstacle(this);
            SaveState(coord.Direction);
            return coord;
        }

        private bool CanRepeat()
        {
            return LastValidation is not null
                && LastValidation(Index);
        }

        private Func<int, bool> GetValidation(Directions direction)
        {
            return direction switch
            {
                Directions.Up => Validator.CanUp,
                Directions.UpLeft => Validator.CanUpLeft,
                Directions.UpRight => Validator.CanUpRight,
                Directions.Down => Validator.CanDown,
                Directions.DownRight => Validator.CanDownRight,
                Directions.DownLeft => Validator.CanDownLeft,
                Directions.Left => Validator.CanLeft,
                Directions.Right => Validator.CanRight,
                _ => throw new NotSupportedException($"Direction '{direction}' is not supported.")
            };
        }

        private int Repeat() => Navigator.GetIndex(Index, LastDirection);

        private void SaveState(Directions direction)
        {
            lock (locker)
            {
                LastDirection = direction;
                LastValidation = GetValidation(direction);
            }
        }

        public void GoToNextSquare(IMowingPolicy policy)
        {
            lock (locker)
            {
                var cache = _index;
                _index = CanRepeat()
                    ? Repeat()
                    : AvoidObstacle(policy);
                //if (Validator.IsTeleport(cache, _index)) { throw new InvalidOperationException($"The mower tried to teleport from index {cache} to index {_index}!"); }
            }
        }

        public void Init(int startIndex, Directions direction)
        {
            SaveState(direction);
            _index = Navigator.GetIndex(startIndex, direction);
        }

        #endregion Methods
    }
}