using MowerSim.Logic.MowingPolicies;

namespace MowerSim.Logic.Utils
{
    public class PolicyDescription
    {
        #region Constructors

        public PolicyDescription(string description, IMowingPolicy policy)
        {
            Description = description;
            Policy = policy;
        }

        #endregion Constructors

        #region Properties

        public string Description { get; }
        public IMowingPolicy Policy { get; }

        #endregion Properties
    }
}