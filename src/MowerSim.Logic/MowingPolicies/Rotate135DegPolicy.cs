namespace MowerSim.Logic.MowingPolicies
{
    public class Rotate135DegPolicy : IMowingPolicy
    {
        #region Methods

        public SquareIndex AvoidObstacle(IMowerAPI api)
        {
            return api.Navigator.Rotate(45, api.Index, Rotation.Rotate(135, api.LastDirection));
        }

        #endregion Methods
    }
}