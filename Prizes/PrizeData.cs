namespace CluelessControl.Prizes
{
    public class PrizeData
    {
        public string PrizeName
        {
            get;
        }

        public decimal RoundingUnit
        {
            get;
        }

        public RoundingMethod RoundingMethod
        {
            get;
        }

        private PrizeData(string prizeName, decimal roundingUnit, RoundingMethod roundingMethod)
        {
            PrizeName = prizeName.Trim();
            RoundingUnit = roundingUnit;
            RoundingMethod = roundingMethod;
        }

        public static PrizeData CreatePrize(string name, decimal roundingUnit, RoundingMethod roundingMethod)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return decimal.Sign(roundingUnit) switch
            {
                < 0 => throw new ArgumentOutOfRangeException(nameof(roundingUnit), "Rounding unit must not be negative!"),
                0 => throw new ArgumentException("Rounding unit must not be zero! (rounding to nearest zero?)", nameof(roundingUnit)),
                _ => new PrizeData(name, roundingUnit, roundingMethod),
            };
        }
    }
}
