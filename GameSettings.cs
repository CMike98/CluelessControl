namespace CluelessControl
{
    public class GameSettings
    {
        public int DecimalPlaces
        {
            get;
        }

        public bool FireworksActive
        {
            get;
        }

        public decimal MinimumCashPrizeForFireworks
        {
            get;
        }

        private GameSettings(int decimalPlaces, bool fireworksActive, decimal minimumCashPrizeForFireworks)
        {
            DecimalPlaces = decimalPlaces;
            FireworksActive = fireworksActive;
            MinimumCashPrizeForFireworks = minimumCashPrizeForFireworks;
        }

        internal static GameSettings Create()
        {
            return new GameSettings(decimalPlaces: 0, fireworksActive: false, minimumCashPrizeForFireworks: 0);
        }

        internal static GameSettings Create(int decimalPlaces, bool fireworksActive, decimal minimumCashPrizeForFireworks)
        {
            if (decimalPlaces < 0)
                throw new ArgumentOutOfRangeException(nameof(decimalPlaces), $"Number of decimal places in the prize money must be non-negative.");
            if (minimumCashPrizeForFireworks < 0)
                throw new ArgumentOutOfRangeException(nameof(minimumCashPrizeForFireworks), $"Minimum cash prize for fireworks must be non-negative.");

            return new GameSettings(decimalPlaces, fireworksActive, minimumCashPrizeForFireworks);
        }
    }
}
