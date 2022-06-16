namespace MowerSim.Logic
{
    public class IndexCalculator
    {
        #region Fields

        private readonly int _columns;
        private readonly int _length;
        private readonly int _rows;

        #endregion Fields

        #region Constructors

        public IndexCalculator(int rows, int columns)
        {
            _columns = columns;
            _rows = rows;
            _length = rows * columns;
        }

        public IndexCalculator(int side) : this(side, side)
        {
        }

        #endregion Constructors

        #region Methods

        private void ThrowIfLongJmp(int from, int to)
        {
            if ((to - from) > (_rows + 1))
            {
                throw new IndexOutOfRangeException(
                    $"Invalid rotation: the index jump length was {from - to} while the max jump length allowed is {_rows + 1}"
                );
            }
        }

        private void ThrowIfOutOfRange(int index)
        {
            if (index >= _length)
            {
                throw new IndexOutOfRangeException(
                    $"Impossible to rotate square {index}. This index is out of range when the board has {_length} squares"
                );
            }
        }

        public int GetColumn(int index) => index % _columns;

        public SquareIndex GetIndex(int index, Directions direction) => Rotate(0, index, direction);

        public int GetRow(int index) => (index - GetColumn(index)) / _rows;

        public SquareIndex Rotate(int angle, int index, Directions direction)
        {
            ThrowIfOutOfRange(index);
            var idx = index;
            direction = Rotation.Rotate(angle, direction);
            switch (direction)
            {
                case Directions.Up:
                    idx -= _rows;
                    break;

                case Directions.UpRight:
                    idx -= (_rows - 1);
                    break;

                case Directions.UpLeft:
                    idx -= (_rows + 1);
                    break;

                case Directions.Down:
                    idx += _rows;
                    break;

                case Directions.DownLeft:
                    idx += (_rows - 1);
                    break;

                case Directions.DownRight:
                    idx += (_rows + 1);
                    break;

                case Directions.Left:
                    idx--;
                    break;

                case Directions.Right:
                    idx++;
                    break;

                default: throw new NotSupportedException($"Direction '{direction}' is not supported.");
            }
            ThrowIfLongJmp(index, idx);
            return new(idx, direction);
        }

        public SquareIndex StepBack(int index, Directions direction) => Rotate(180, index, direction);

        #endregion Methods
    }
}