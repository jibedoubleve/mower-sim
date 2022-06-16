namespace MowerSim.Logic.Utils
{
    public class DataEventArgs<T> : EventArgs
    {
        #region Constructors

        public DataEventArgs(T data)
        {
            Data = data;
        }

        #endregion Constructors

        #region Properties

        public T Data { get; }

        #endregion Properties
    }
}