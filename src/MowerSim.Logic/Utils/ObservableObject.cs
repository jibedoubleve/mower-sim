using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MowerSim.Logic.Utils
{
    public class ObservableObject : INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Methods

        protected bool Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) { return false; }
            else
            {
                storage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }
        }

        #endregion Methods
    }
}