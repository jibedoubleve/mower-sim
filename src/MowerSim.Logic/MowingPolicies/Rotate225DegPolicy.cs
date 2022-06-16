namespace MowerSim.Logic.MowingPolicies
{
    public class Rotate225DegPolicy : IMowingPolicy
    {
        #region Methods

        public SquareIndex AvoidObstacle(IMowerAPI api)
        {
            return api.Navigator.Rotate(45, api.Index, Rotation.Rotate(225, api.LastDirection));
        }

        #endregion Methods
    }
}