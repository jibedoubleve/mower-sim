using FluentAssertions;
using MowerSim.Logic;
using MowerSim.Logic.MowingPolicies;
using Xunit;

namespace MowerSim.Tests
{
    public class PolicyShould
    {
        #region Methods

        private static Mower GetMower(SquareIndex index)
        {
            return new Mower(
                side: 3,
                index: index,
                lastDirection: index.Direction
            );
        }

        [Theory]
        [InlineData(Directions.Right, Directions.DownRight)]
        public void GoBackwardAnd45DegClockwise_Dir(Directions actual, Directions expected)
        {
            var policy = new BackwardAnd45DegClockwisePolicy();
            var index = new SquareIndex(1, actual);
            var coord = policy.AvoidObstacle(GetMower(index));
            coord.Direction.Should().Be(expected);
        }

        [Theory]
        [InlineData(Directions.Right, 4)]
        public void GoBackwardAnd45DegClockwise_Idx(Directions actual, int expected)
        {
            var policy = new BackwardAnd45DegClockwisePolicy();
            var index = new SquareIndex(1, actual);
            var coord = policy.AvoidObstacle(GetMower(index));

            coord.Index.Should().Be(expected);
        }

        [Theory]
        [InlineData(Directions.Right, Directions.Down)]
        public void GoBackwardAnd90DegClockwise_Dir(Directions actual, Directions expected)
        {
            var policy = new BackwardAnd90DegClockwisePolicy();
            var index = new SquareIndex(4, actual);
            var coord = policy.AvoidObstacle(GetMower(index));

            coord.Direction.Should().Be(expected);
        }

        [Theory]
        [InlineData(Directions.Right, 6)]
        public void GoBackwardAnd90DegClockwise_Idx(Directions actual, int expected)
        {
            var policy = new BackwardAnd90DegClockwisePolicy();
            var index = new SquareIndex(4, actual);
            var coord = policy.AvoidObstacle(GetMower(index));

            coord.Index.Should().Be(expected);
        }

        #endregion Methods
    }
}