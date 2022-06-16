using System.Diagnostics;

namespace MowerSim.Logic.MowingPolicies
{
    public class RandomPolicy : IMowingPolicy
    {
        #region Fields

        private static readonly Random _random = new();

        #endregion Fields

        #region Properties

        public bool RandomBool => _random.Next(0, 2) == 1;

        #endregion Properties

        #region Methods

        public SquareIndex AvoidObstacle(IMowerAPI api)
        {
            SquareIndex coord = SquareIndex.Empty;
            bool can = false;
            while (can == false)
            {
                var rotation = Rotation.RandomRotation;
                if (RandomBool)
                {
                    Trace.WriteLine("-> WITHOUT stepback");
                    coord = api.Navigator.Rotate(rotation, api.Index, api.LastDirection);
                    can = api.Validator.Can(api.Index, coord.Direction);
                }
                else
                {
                    Trace.WriteLine("-> WITH stepback");
                    var back = api.Navigator.StepBack(api.Index, api.LastDirection);
                    coord = api.Navigator.Rotate(rotation, back.Index, api.LastDirection);
                    can = api.Validator.Can(back.Index, coord.Direction);
                }

                Trace.Write($"Index: {api.Index,3} - Rotation: {rotation,3}° for direction {api.LastDirection,-10} [{coord.Direction,-10}]");
                Trace.WriteLine($" --> {(can ? "Valid" : "Invalid"),7} to go on {coord.Index,3}");
            }

            return coord;
        }

        #endregion Methods
    }
}