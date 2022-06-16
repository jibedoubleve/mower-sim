namespace MowerSim.Logic
{
    public struct Rotation
    {
        #region Fields

        private static readonly Random _rand = new();

        private static readonly int[] _validRotations = new int[] { 0, 45, 90, 135, 180, 225, 270, 315 };

        #endregion Fields

        #region Properties

        public static int RandomRotation
        {
            get
            {
                var index = _rand.Next(0, _validRotations.Length);
                return _validRotations[index];
            }
        }

        #endregion Properties

        #region Methods

        public static int RandomIn(params int[] choiceList)
        {
            if (choiceList.Length == 0) { throw new NotSupportedException("Choice list cannot be empty."); }
            else if (choiceList is null) { throw new ArgumentNullException(nameof(choiceList), "Choice list cannot be null."); }
            else
            {
                return choiceList[_rand.Next(0, choiceList.Length)];
            }
        }

        public static Directions Rotate(int degree, Directions lastDirection)
        {
            if (!_validRotations.Contains(Math.Abs(degree)))
            {
                throw new NotSupportedException($"Rotation of {degree}° is not supported. Here are the supported values: {string.Join(", ", _validRotations)}");
            }
            else
            {
                var offset = degree / 45;
                offset = (degree < 0) ? 8 + offset : offset;
                return (Directions)(((int)lastDirection + offset) % 8);
            }
        }

        #endregion Methods
    }
}