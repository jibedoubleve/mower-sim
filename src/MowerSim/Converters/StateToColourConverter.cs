using MowerSim.Logic;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MowerSim.Converters
{
    internal class StateToColourConverter : IValueConverter
    {
        #region Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SquareState state)
            {
                return state switch
                {
                    SquareState.Empty => Brushes.Green,
                    SquareState.Mowed => Brushes.LightGreen,
                    SquareState.Mowing => Brushes.Crimson,
                    _ => Brushes.Yellow,
                };
            }
            else { return value; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

        #endregion Methods
    }
}