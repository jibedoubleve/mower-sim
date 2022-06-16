namespace MowerSim.Logic.MowingPolicies
{
    public interface IMowingPolicy
    {
        #region Methods

        /// <summary>
        /// Get the next index after the mower encountered an obstacle.
        /// </summary>
        /// <param name="api">The api of the mower</param>
        /// <returns>The new index and the new direction after boucing on the obsatcle</returns>
        SquareIndex AvoidObstacle(IMowerAPI api);

        #endregion Methods
    }
}