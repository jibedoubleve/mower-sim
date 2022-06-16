using FluentAssertions;
using MowerSim.Logic;
using Xunit;

namespace MowerSim.Tests
{
    public class IndexValidatorShould
    {
        #region Fields

        private readonly IndexValidator _validator = new(3);

        #endregion Fields
        [Theory]
        [InlineData(0)]
        [InlineData(6)]
        [InlineData(2)]
        [InlineData(8)]
        public void RecogniseIsCorner(int index)
        {
            _validator
                .IsCorner(index)
                .Should().BeTrue();
        }
        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(7)]
        [InlineData(3)]
        [InlineData(5)]
        public void RecogniseIsNotCorner(int index)
        {
            _validator
                .IsCorner(index)
                .Should().BeFalse();
        }
        #region Methods

        [Theory]
        [InlineData(-1)]
        [InlineData(100)]
        [InlineData(990)]
        public void HaveRangeInvalid(int index)
        {
            _validator
                .IsInRange(index)
                .Should().BeFalse();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public void HaveRangeValid(int index)
        {
            _validator
                .IsInRange(index)
                .Should().BeTrue();
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(3, true)]
        [InlineData(4, true)]
        [InlineData(5, true)]
        [InlineData(6, false)]
        [InlineData(7, false)]
        [InlineData(8, false)]
        [InlineData(101, false)]
        [InlineData(-1, false)]
        public void ValidateDown(int index, bool expected)
        {
            _validator
                .CanDown(index)
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(0, 6)]
        [InlineData(0, 7)]
        [InlineData(0, 8)]
        [InlineData(1, 6)]
        [InlineData(1, 7)]
        [InlineData(1, 8)]
        [InlineData(2, 6)]
        [InlineData(2, 7)]
        [InlineData(2, 8)]
        [InlineData(0, 2)]
        [InlineData(0, 5)]
        [InlineData(2, 0)]
        [InlineData(2, 3)]
        public void DetectTeleport(int oldIdx, int newIdx)
        {
            _validator
                .IsTeleport(oldIdx, newIdx)
                .Should().BeTrue();
        }
        [Theory]
        [InlineData(4, 0)]
        [InlineData(4, 1)]
        [InlineData(4, 2)]
        [InlineData(4, 5)]
        [InlineData(4, 8)]
        [InlineData(4, 7)]
        [InlineData(4, 6)]
        [InlineData(4, 3)]
        public void DetectNotTeleport(int oldIdx, int newIdx)
        {
            _validator
                .IsTeleport(oldIdx, newIdx)
                .Should().BeFalse();
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(3, false)]
        [InlineData(4, true)]
        [InlineData(5, true)]
        [InlineData(6, false)]
        [InlineData(7, false)]
        [InlineData(8, false)]
        [InlineData(101, false)]
        [InlineData(-1, false)]
        public void ValidateDownLeft(int index, bool expected)
        {
            _validator
                .CanDownLeft(index)
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, true)]
        [InlineData(2, false)]
        [InlineData(3, true)]
        [InlineData(4, true)]
        [InlineData(5, false)]
        [InlineData(6, false)]
        [InlineData(7, false)]
        [InlineData(8, false)]
        [InlineData(101, false)]
        [InlineData(-1, false)]
        public void ValidateDownRight(int index, bool expected)
        {
            _validator
                .CanDownRight(index)
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(3, true)]
        [InlineData(4, true)]
        [InlineData(5, true)]
        [InlineData(6, false)]
        [InlineData(7, false)]
        [InlineData(8, false)]
        [InlineData(101, false)]
        [InlineData(-1, false)]
        public void ValidateGenericOnDown(int index, bool expected)
        {
            _validator
                .Can(index, Directions.Down)
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(3, false)]
        [InlineData(4, true)]
        [InlineData(5, true)]
        [InlineData(6, false)]
        [InlineData(7, false)]
        [InlineData(8, false)]
        [InlineData(101, false)]
        [InlineData(-1, false)]
        public void ValidateGenericOnDownLeft(int index, bool expected)
        {
            _validator
                .Can(index, Directions.DownLeft)
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, true)]
        [InlineData(2, false)]
        [InlineData(3, true)]
        [InlineData(4, true)]
        [InlineData(5, false)]
        [InlineData(6, false)]
        [InlineData(7, false)]
        [InlineData(8, false)]
        [InlineData(101, false)]
        [InlineData(-1, false)]
        public void ValidateGenericOnDownRight(int index, bool expected)
        {
            _validator
                .Can(index, Directions.DownRight)
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(3, false)]
        [InlineData(4, true)]
        [InlineData(5, true)]
        [InlineData(6, false)]
        [InlineData(7, true)]
        [InlineData(8, true)]
        [InlineData(101, false)]
        [InlineData(-1, false)]
        public void ValidateGenericOnLeft(int index, bool expected)
        {
            _validator
                .Can(index, Directions.Left)
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, true)]
        [InlineData(2, false)]
        [InlineData(3, true)]
        [InlineData(4, true)]
        [InlineData(5, false)]
        [InlineData(6, true)]
        [InlineData(7, true)]
        [InlineData(8, false)]
        [InlineData(101, false)]
        [InlineData(-1, false)]
        public void ValidateGenericOnRight(int index, bool expected)
        {
            _validator
                .Can(index, Directions.Right)
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, false)]
        [InlineData(2, false)]
        [InlineData(3, true)]
        [InlineData(4, true)]
        [InlineData(5, true)]
        [InlineData(6, true)]
        [InlineData(7, true)]
        [InlineData(8, true)]
        [InlineData(101, false)]
        [InlineData(-1, false)]
        public void ValidateGenericOnUp(int index, bool expected)
        {
            _validator
                .Can(index, Directions.Up)
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, false)]
        [InlineData(2, false)]
        [InlineData(3, false)]
        [InlineData(4, true)]
        [InlineData(5, true)]
        [InlineData(6, false)]
        [InlineData(7, true)]
        [InlineData(8, true)]
        [InlineData(101, false)]
        [InlineData(-1, false)]
        public void ValidateGenericOnUpLeft(int index, bool expected)
        {
            _validator
                .Can(index, Directions.UpLeft)
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, false)]
        [InlineData(2, false)]
        [InlineData(3, true)]
        [InlineData(4, true)]
        [InlineData(5, false)]
        [InlineData(6, true)]
        [InlineData(7, true)]
        [InlineData(8, false)]
        [InlineData(101, false)]
        [InlineData(-1, false)]
        public void ValidateGenericOnUpRight(int index, bool expected)
        {
            _validator
                .Can(index, Directions.UpRight)
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(3, false)]
        [InlineData(4, true)]
        [InlineData(5, true)]
        [InlineData(6, false)]
        [InlineData(7, true)]
        [InlineData(8, true)]
        [InlineData(101, false)]
        [InlineData(-1, false)]
        public void ValidateLeft(int index, bool expected)
        {
            _validator
                .CanLeft(index)
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, true)]
        [InlineData(2, false)]
        [InlineData(3, true)]
        [InlineData(4, true)]
        [InlineData(5, false)]
        [InlineData(6, true)]
        [InlineData(7, true)]
        [InlineData(8, false)]
        [InlineData(101, false)]
        [InlineData(-1, false)]
        public void ValidateRight(int index, bool expected)
        {
            _validator
                .CanRight(index)
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, false)]
        [InlineData(2, false)]
        [InlineData(3, true)]
        [InlineData(4, true)]
        [InlineData(5, true)]
        [InlineData(6, true)]
        [InlineData(7, true)]
        [InlineData(8, true)]
        [InlineData(101, false)]
        [InlineData(-1, false)]
        public void ValidateUp(int index, bool expected)
        {
            _validator
                .CanUp(index)
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, false)]
        [InlineData(2, false)]
        [InlineData(3, false)]
        [InlineData(4, true)]
        [InlineData(5, true)]
        [InlineData(6, false)]
        [InlineData(7, true)]
        [InlineData(8, true)]
        [InlineData(101, false)]
        [InlineData(-1, false)]
        public void ValidateUpLeft(int index, bool expected)
        {
            _validator
                .CanUpLeft(index)
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, false)]
        [InlineData(2, false)]
        [InlineData(3, true)]
        [InlineData(4, true)]
        [InlineData(5, false)]
        [InlineData(6, true)]
        [InlineData(7, true)]
        [InlineData(8, false)]
        [InlineData(101, false)]
        [InlineData(-1, false)]
        public void ValidateUpRight(int index, bool expected)
        {
            _validator
                .CanUpRight(index)
                .Should().Be(expected);
        }

        #endregion Methods
    }
}