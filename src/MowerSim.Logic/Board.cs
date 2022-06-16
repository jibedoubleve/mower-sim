using MowerSim.Logic.MowingPolicies;
using MowerSim.Logic.Utils;

namespace MowerSim.Logic
{
    public class Board : ObservableObject
    {
        #region Fields

        private readonly int _delay;
        private readonly Mower _mower;
        private readonly List<Square> _squares;
        private int _cellCount;
        private int _columnCount;
        private bool _continue = false;
        private PolicyDescription _currentPolicy;
        private int _moveCounter;
        private int _rowCount;

        #endregion Fields

        #region Constructors

        public Board(int height = 8, int width = 8, int delay = 250)
        {
            _mower = new Mower(width, height);
            _delay = delay;
            Columns = width;
            Rows = height;
            _squares = BuildBoard();
            Policies = new PolicyCollection();
            CurrentPolicy = Policies.Default;
        }

        #endregion Constructors

        #region Events

        public event EventHandler<DataEventArgs<string>> MessageSent;

        #endregion Events

        #region Properties

        public int Columns
        {
            get => _columnCount;
            set => Set(ref _columnCount, value);
        }

        public bool Continue
        {
            get => _continue;
            set => Set(ref _continue, value);
        }

        public int Count
        {
            get => _cellCount;
            private set => Set(ref _cellCount, value);
        }

        public PolicyDescription CurrentPolicy
        {
            get => _currentPolicy;
            set => Set(ref _currentPolicy, value);
        }

        public int MoveCounter
        {
            get => _moveCounter;
            set => Set(ref _moveCounter, value);
        }

        public PolicyCollection Policies { get; }

        public int Rows
        {
            get => _rowCount;
            set => Set(ref _rowCount, value);
        }

        public IEnumerable<Square> Squares
        {
            get => _squares;
        }

        #endregion Properties

        #region Methods

        private List<Square> BuildBoard()
        {
            ResetCounter();
            var squares = new List<Square>();
            for (int i = 0; i < Columns * Rows; i++)
            {
                squares.Add(new Square(i));
            }
            Count = squares.Count;
            return squares;
        }

        private bool IsAllMowed()
        {
            return !(from s in Squares
                     where s.State == SquareState.Empty
                     select s).Any();
        }

        private async Task MowAsync(int index)
        {
            if (index >= _squares.Count) { throw new IndexOutOfRangeException($"Cannot mow at square with index '{index}'"); }
            else
            {
                var square = _squares[index];
                square.State = SquareState.Mowing;
                square.Counter++;
                await Task.Delay(_delay);
                _squares[index].State = SquareState.Mowed;
            }
        }

        private void OnSendMessage(string message)
        {
            var eventArgs = new DataEventArgs<string>(message);
            MessageSent?.Invoke(this, eventArgs);
        }

        private void ResetCounter() => MoveCounter = 0;

        public void Reset()
        {
            if (Continue == false)
            {
                ResetCounter();
                foreach (var item in Squares)
                {
                    item.Reset();
                }
            }
        }

        public async Task StartMowingAsync(int startIndex)
        {
            if (startIndex >= _cellCount) { OnSendMessage($"Start index {startIndex} is out of range, the board has {_cellCount} squares!"); }
            else
            {
                Continue = true;

                _mower.Init(startIndex, Directions.DownRight);
                await MowAsync(_mower.Index);

                while (!IsAllMowed() && Continue)
                {
                    _mower.GoToNextSquare(CurrentPolicy?.Policy);
                    await MowAsync(_mower.Index);
                    MoveCounter++;
                }

                if (Continue) { OnSendMessage("The garden is mowed!"); }
            }
        }

        public void StopMowing() => Continue = false;

        #endregion Methods
    }
}