using System.Diagnostics;

namespace MowerSim.Logic
{
    public class IndexValidator
    {
        #region Fields

        private readonly IndexCalculator _calculator;
        private readonly int _columns;
        private readonly int _rows;
        private readonly int _size;

        #endregion Fields

        #region Constructors

        public IndexValidator(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
            _size = rows * columns;
            _calculator = new IndexCalculator(rows, columns);
        }

        public bool IsCorner(int index)
        {
            var row = _calculator.GetRow(index);
            var col = _calculator.GetColumn(index);

            return (row == 0 && col == 0)                     // upper left
                || (row == 0 && col == _columns - 1)          // upper right
                || (row == _rows - 1 && col == 0)             // lower left
                || (row == _rows - 1 && col == _columns - 1); // lower right
        }

        public IndexValidator(int side) : this(side, side)
        {
        }

        #endregion Constructors

        #region Methods

        public bool Can(int index, Directions direction)
        {
            var result = direction switch
            {
                Directions.Up => CanUp(index),
                Directions.UpRight => CanUpRight(index),
                Directions.Right => CanRight(index),
                Directions.DownRight => CanDownRight(index),
                Directions.Down => CanDown(index),
                Directions.DownLeft => CanDownLeft(index),
                Directions.Left => CanLeft(index),
                Directions.UpLeft => CanUpLeft(index),
                _ => throw new NotSupportedException($"Direction '{direction}' is not supported.")
            };
            return result;
        }

        public bool CanDown(int index) => _calculator.GetRow(index) < (_rows - 1) && IsInRange(index);

        public bool CanDownLeft(int index) => CanDown(index) && CanLeft(index) && IsInRange(index);

        public bool CanDownRight(int index) => CanDown(index) && CanRight(index) && IsInRange(index);

        public bool CanLeft(int index) => _calculator.GetColumn(index) > 0 && IsInRange(index);

        public bool CanRight(int index) => _calculator.GetColumn(index) < _columns - 1 && IsInRange(index);

        public bool CanUp(int index) => _calculator.GetRow(index) > 0 && IsInRange(index);

        public bool CanUpLeft(int index) => CanUp(index) && CanLeft(index) && IsInRange(index);

        public bool CanUpRight(int index) => CanUp(index) && CanRight(index) && IsInRange(index);

        public bool IsInRange(int index) => index < _size && index >= 0;

        public bool IsTeleport(int old, int @new)
        {
            var rowJump = Math.Abs(_calculator.GetRow(old) - _calculator.GetRow(@new));
            var colJump = Math.Abs(_calculator.GetColumn(old) - _calculator.GetColumn(@new));

            if (rowJump > 1) { return true; }
            else if (colJump > 1) { return true; }
            else { return false; }
        }

        #endregion Methods
    }
}