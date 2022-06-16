using FluentAssertions;
using MowerSim.Logic;
using Xunit;

namespace MowerSim.Tests
{
    public class IndexCalculatorShould
    {
        #region Fields

        private readonly IndexCalculator _navigator = new(3);

        #endregion Fields

        #region Methods

        [Theory]
        [InlineData(0, 3)]
        [InlineData(1, 4)]
        [InlineData(3, 6)]
        [InlineData(4, 7)]
        public void GoDown(int index, int expected)
        {
            _navigator
                .GetIndex(index, Directions.Down)
                .Index
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(1, 3)]
        [InlineData(4, 6)]
        [InlineData(5, 7)]
        [InlineData(2, 4)]
        public void GoDownLeft(int index, int expected)
        {
            _navigator
                .GetIndex(index, Directions.DownLeft)
                .Index
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(0, 4)]
        [InlineData(3, 7)]
        [InlineData(1, 5)]
        [InlineData(4, 8)]
        public void GoDownRight(int index, int expected)
        {
            _navigator
                .GetIndex(index, Directions.DownRight)
                .Index
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(4, 3)]
        [InlineData(7, 6)]
        [InlineData(2, 1)]
        public void GoLeft(int index, int expected)
        {
            _navigator
                .GetIndex(index, Directions.Left)
                .Index
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(4, 5)]
        [InlineData(7, 8)]
        public void GoRight(int index, int expected)
        {
            _navigator
                .GetIndex(index, Directions.Right)
                .Index
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(3, 0)]
        [InlineData(4, 1)]
        [InlineData(5, 2)]
        [InlineData(6, 3)]
        public void GoUp(int index, int expected)
        {
            _navigator
                .GetIndex(index, Directions.Up)
                .Index
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(7, 3)]
        [InlineData(4, 0)]
        [InlineData(5, 1)]
        [InlineData(8, 4)]
        public void GoUpLeft(int index, int expected)
        {
            _navigator
                .GetIndex(index, Directions.UpLeft)
                .Index
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(3, 1)]
        [InlineData(4, 2)]
        [InlineData(6, 4)]
        [InlineData(7, 5)]
        public void GoUpRight(int index, int expected)
        {
            _navigator
                .GetIndex(index, Directions.UpRight)
                .Index
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(3, 0)]
        [InlineData(6, 0)]
        [InlineData(1, 1)]
        [InlineData(4, 1)]
        [InlineData(7, 1)]
        [InlineData(2, 2)]
        [InlineData(5, 2)]
        [InlineData(8, 2)]
        public void ReturnColumn(int index, int expectedColumn)
        {
            _navigator
                .GetColumn(index)
                .Should().Be(expectedColumn);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(3, 1)]
        [InlineData(4, 1)]
        [InlineData(5, 1)]
        [InlineData(6, 2)]
        [InlineData(7, 2)]
        [InlineData(8, 2)]
        public void ReturnRow(int index, int expectedRow)
        {
            _navigator
                .GetRow(index)
                .Should().Be(expectedRow);
        }

        [Theory]
        [InlineData(Directions.Up, 2)]
        [InlineData(Directions.UpRight, 5)]
        [InlineData(Directions.Right, 8)]
        [InlineData(Directions.DownRight, 7)]
        [InlineData(Directions.Down, 6)]
        [InlineData(Directions.DownLeft, 3)]
        [InlineData(Directions.Left, 0)]
        [InlineData(Directions.UpLeft, 1)]
        public void RotateClockwise(Directions direction, int expected)
        {
            _navigator
                .Rotate(45, 4, direction)
                .Index
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(Directions.Up)]
        [InlineData(Directions.UpRight)]
        [InlineData(Directions.Right)]
        [InlineData(Directions.DownRight)]
        [InlineData(Directions.Down)]
        [InlineData(Directions.DownLeft)]
        [InlineData(Directions.Left)]
        public void FailWhenRotateClockwiseOutOfScope(Directions direction)
        {
            Assert.Throws<IndexOutOfRangeException>(() => _navigator.Rotate(45, 9, direction));
        }
        [Theory]
        [InlineData(Directions.Up)]
        [InlineData(Directions.UpRight)]
        [InlineData(Directions.Right)]
        [InlineData(Directions.DownRight)]
        [InlineData(Directions.Down)]
        [InlineData(Directions.DownLeft)]
        [InlineData(Directions.Left)]
        public void FailWhenRotateCounterClockwiseOutOfScope(Directions direction)
        {
            Assert.Throws<IndexOutOfRangeException>(() => _navigator.Rotate(-45, 9, direction));
        }

        [Theory]
        [InlineData(Directions.Up, Directions.UpRight)]
        [InlineData(Directions.UpRight, Directions.Right)]
        [InlineData(Directions.Right, Directions.DownRight)]
        [InlineData(Directions.DownRight, Directions.Down)]
        [InlineData(Directions.Down, Directions.DownLeft)]
        [InlineData(Directions.DownLeft, Directions.Left)]
        [InlineData(Directions.Left, Directions.UpLeft)]
        [InlineData(Directions.UpLeft, Directions.Up)]
        public void RotateClockwiseWithCorrectDirection(Directions direction, Directions expected)
        {
            _navigator
                .Rotate(45, 4, direction)
                .Direction
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(Directions.Up, 0)]
        [InlineData(Directions.UpRight, 1)]
        [InlineData(Directions.Right, 2)]
        [InlineData(Directions.DownRight, 5)]
        [InlineData(Directions.Down, 8)]
        [InlineData(Directions.DownLeft, 7)]
        [InlineData(Directions.Left, 6)]
        [InlineData(Directions.UpLeft, 3)]
        public void RotateCounterClockwise(Directions direction, int expected)
        {
            _navigator
                .Rotate(-45, 4, direction)
                .Index
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(Directions.Up, Directions.UpLeft)]
        [InlineData(Directions.UpRight, Directions.Up)]
        [InlineData(Directions.Right, Directions.UpRight)]
        [InlineData(Directions.DownRight, Directions.Right)]
        [InlineData(Directions.Down, Directions.DownRight)]
        [InlineData(Directions.DownLeft, Directions.Down)]
        [InlineData(Directions.Left, Directions.DownLeft)]
        [InlineData(Directions.UpLeft, Directions.Left)]
        public void RotateCounterlockwiseWithCorrectDirection(Directions direction, Directions expected)
        {
            _navigator
                .Rotate(-45, 4, direction)
                .Direction
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(Directions.DownRight, 0)]
        [InlineData(Directions.DownLeft, 2)]
        [InlineData(Directions.Down, 1)]
        [InlineData(Directions.UpRight, 6)]
        [InlineData(Directions.UpLeft, 8)]
        [InlineData(Directions.Up, 7)]
        [InlineData(Directions.Left, 5)]
        [InlineData(Directions.Right, 3)]
        public void StepBack(Directions direction, int expected)
        {
            _navigator
                .StepBack(4, direction)
                .Index
                .Should().Be(expected);
        }

        #endregion Methods
    }
}