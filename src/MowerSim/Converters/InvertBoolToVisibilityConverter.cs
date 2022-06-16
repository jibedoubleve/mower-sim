using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MowerSim.Converters
{
    internal class InvertBoolToVisibilityConverter : IValueConverter
    {
        #region Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isNotVisible)
            {
                return isNotVisible
                    ? Visibility.Collapsed
                    : Visibility.Visible;
            }
            else { return value; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

        #endregion Methods
    }
}