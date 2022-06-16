using System.Diagnostics;

namespace MowerSim.Logic.MowingPolicies
{
    internal class BackwardOnlyInCornerAnd45DegClockwisePolicy : IMowingPolicy
    {
        #region Methods

        public SquareIndex AvoidObstacle(IMowerAPI api)
        {
            SquareIndex coord = SquareIndex.Empty;
            bool can = false;

            while (can == false)
            {
                var rotation = Rotation.RandomIn(-45, 45, 135, 225);

                if (api.Validator.IsCorner(api.Index))
                {
                    var back = api.Navigator.StepBack(api.Index, api.LastDirection);

                    Trace.WriteLine($"-> Is Corner, step back to {back.Index}");
                    coord = api.Navigator.Rotate(rotation, back.Index, back.Direction);
                    can = api.Validator.Can(back.Index, coord.Direction);
                }
                else
                {
                    Trace.WriteLine("-> Is NOT Corner");
                    coord = api.Navigator.Rotate(rotation, api.Index, api.LastDirection);
                    can = api.Validator.Can(api.Index, coord.Direction);
                }

                Trace.Write($"Index: {api.Index,3} - Rotation: {rotation,3}° for direction {api.LastDirection,-10} [{coord.Direction,-10}]");
                Trace.WriteLine($" --> {(can ? "Valid" : "Invalid"),7} to go on {coord.Index,3}");
            }

            return coord;
        }

        #endregion Methods
    }
}