namespace MowerSim.Logic
{
    public interface IMowerAPI
    {
        #region Properties

        int Index { get; }
        Directions LastDirection { get; }
        IndexCalculator Navigator { get; }
        IndexValidator Validator { get; }

        #endregion Properties
    }
}