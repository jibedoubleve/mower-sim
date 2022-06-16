using System.Diagnostics;

namespace MowerSim.Logic.MowingPolicies
{
    public class RandomWithStepBackPolicy : IMowingPolicy
    {
        #region Methods

        public SquareIndex AvoidObstacle(IMowerAPI api)
        {
            SquareIndex coord = SquareIndex.Empty;
            bool can = false;

            while (can == false)
            {
                var rotation = Rotation.RandomRotation;

                coord = api.Navigator.StepBack(api.Index,api.LastDirection);
                coord = api.Navigator.Rotate(rotation, coord.Index, api.LastDirection);
                can = api.Validator.Can(api.Index, coord.Direction);

                Trace.Write($"Index: {api.Index,3} - Rotation: {rotation,3}° for direction {api.LastDirection,-10} [{coord.Direction,-10}]");
                Trace.WriteLine($" --> {(can ? "Valid" : "Invalid"),7} to go on {coord.Index,3}");
            }

            return coord;
        }

        #endregion Methods
    }
}