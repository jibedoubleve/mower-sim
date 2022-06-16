using Humanizer;
using MowerSim.Logic.MowingPolicies;
using System.Reflection;

namespace MowerSim.Logic.Utils
{
    public class PolicyCollection : IEnumerable<PolicyDescription>
    {
        #region Fields

        private readonly List<PolicyDescription> _policies = new();

        #endregion Fields

        #region Constructors

        public PolicyCollection()
        {
            _policies = new List<PolicyDescription>(GetPolicies());
            Default = _policies.ElementAt(0);
        }

        #endregion Constructors

        #region Properties

        public PolicyDescription Default { get; }
        public IEnumerable<PolicyDescription> Policies => _policies;

        #endregion Properties

        #region Methods

        private static IEnumerable<PolicyDescription> GetPolicies()
        {
            var asm = Assembly.GetAssembly(typeof(IMowingPolicy));
            var result = new List<PolicyDescription>();

            var policies = from t in asm.GetTypes()
                           where t.IsAssignableTo(typeof(IMowingPolicy))
                              && t.IsClass
                           orderby t.Name
                           select t;
            foreach (var policy in policies)
            {
                var instance = Activator.CreateInstance(policy) as IMowingPolicy;

                var name = policy.Name
                    .Replace("Policy", "")
                    .Humanize();

                result.Add(new(name, instance));
            }
            return result;
        }

        public IEnumerator<PolicyDescription> GetEnumerator() => _policies.GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion Methods
    }
}