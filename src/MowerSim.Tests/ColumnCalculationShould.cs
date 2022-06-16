using FluentAssertions;
using MowerSim.Logic;
using Xunit;

namespace MowerSim.Tests
{
    public class ColumnCalculationShould
    {
        #region Fields

        private readonly IndexCalculator _calculator = new(20);

        #endregion Fields

        #region Constructors

        [Theory]
        [InlineData(380, 0)]
        public void HandleSpecialCases(int index, int expected)
        {
            _calculator
                .GetColumn(index)
                .Should().Be(expected);
        }

        #endregion Constructors
    }
}