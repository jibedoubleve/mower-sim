namespace MowerSim.Logic.MowingPolicies
{
    public class BackwardAnd45DegClockwisePolicy : IMowingPolicy
    {
        #region Methods

        public SquareIndex AvoidObstacle(IMowerAPI api)
        {
            var backIndex = api.Navigator.StepBack(api.Index, api.LastDirection);
            var coord = api.Navigator.Rotate(45, backIndex, api.LastDirection);
            return coord;
        }

        #endregion Methods
    }
}