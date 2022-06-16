using FluentAssertions;
using MowerSim.Logic;
using Xunit;

namespace MowerSim.Tests.Bugs
{
    public class IndexValidationShould
    {
        #region Methods

        [Theory]
        [InlineData(399, Directions.UpRight)]
        [InlineData(380, Directions.Left)]
        public void Invalidate(int index, Directions direction)
        {
            var validator = new IndexValidator(20);
            validator
                .Can(index, direction)
                .Should().BeFalse();
        }

        [Theory]
        [InlineData(380, Directions.UpRight)]
        public void Validate(int index, Directions direction)
        {
            var validator = new IndexValidator(20);
            validator
                .Can(index, direction)
                .Should().BeTrue();
        }

        #endregion Methods
    }
}