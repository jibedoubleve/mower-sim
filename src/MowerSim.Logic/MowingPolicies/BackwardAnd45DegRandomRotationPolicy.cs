namespace MowerSim.Logic.MowingPolicies
{
    public class BackwardAnd45DegRandomRotationPolicy : IMowingPolicy
    {
        #region Fields

        private static readonly Random _random = new();

        #endregion Fields

        #region Methods

        public SquareIndex AvoidObstacle(IMowerAPI api)
        {
            var backIndex = api.Navigator.StepBack(api.Index, api.LastDirection);

            var angle = Rotation.RandomIn(45, -45);
            return api.Navigator.Rotate(angle, backIndex, api.LastDirection);
        }

        #endregion Methods
    }
}