using MowerSim.Logic.Utils;
using System.Diagnostics;

namespace MowerSim.Logic
{
    [DebuggerDisplay("State: {State}")]
    public class Square : ObservableObject
    {
        #region Fields

        private int _counter;
        private int _index;
        private SquareState _state;

        #endregion Fields

        #region Constructors

        public Square(int index)
        {
            _state = SquareState.Empty;
            Index = index;
            Counter = 0;
        }

        #endregion Constructors

        #region Properties

        public int Counter
        {
            get => _counter;
            set => Set(ref _counter, value);
        }

        public int Index
        {
            get => _index;
            set => Set(ref _index, value);
        }

        public SquareState State
        {
            get => _state;
            set => Set(ref _state, value);
        }

        #endregion Properties

        #region Methods

        public void Reset()
        {
            State = SquareState.Empty;
        }

        #endregion Methods
    }
}