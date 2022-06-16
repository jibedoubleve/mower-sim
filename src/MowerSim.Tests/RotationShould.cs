using FluentAssertions;
using MowerSim.Logic;
using Xunit;

namespace MowerSim.Tests
{
    public class RotationShould
    {
        #region Methods

        [Theory]
        [InlineData(Directions.Up, 180, Directions.Down)]
        [InlineData(Directions.UpRight, 45, Directions.Right)]
        [InlineData(Directions.UpLeft, 0, Directions.UpLeft)]
        [InlineData(Directions.UpLeft, 90, Directions.UpRight)]
        [InlineData(Directions.Down, 45, Directions.DownLeft)]
        public void ProvideRightValue(Directions src, int angle, Directions expected)
        {
            Rotation
                .Rotate(angle, src)
                .Should().Be(expected);
        }

        [Theory]
        [InlineData(18)]
        [InlineData(11)]
        [InlineData(30)]
        [InlineData(110)]
        [InlineData(410)]
        public void FailOnWrongValueValue(int angle)
        {
            Assert.Throws<NotSupportedException>(() => Rotation.Rotate(angle, Directions.Left));
        }

        #endregion Methods
    }
}