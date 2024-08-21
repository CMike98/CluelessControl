using System.Text.Json;

namespace CluelessControl
{
    public class GameSettings
    {
        public int DecimalPlaces
        {
            get;
        }

        public bool OnlyWorstMinusCounts
        {
            get;
        }

        public bool OnlyBestPlusCounts
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

        private GameSettings(int decimalPlaces, bool onlyWorstMinusCounts, bool onlyBestPlusCounts, bool fireworksActive, decimal minimumCashPrizeForFireworks)
        {
            DecimalPlaces = decimalPlaces;
            OnlyWorstMinusCounts = onlyWorstMinusCounts;
            OnlyBestPlusCounts = onlyBestPlusCounts;
            FireworksActive = fireworksActive;
            MinimumCashPrizeForFireworks = minimumCashPrizeForFireworks;
        }

        internal static GameSettings Create()
        {
            return new GameSettings(decimalPlaces: 0, onlyWorstMinusCounts: true, onlyBestPlusCounts: true, fireworksActive: false, minimumCashPrizeForFireworks: 0);
        }

        internal static GameSettings Create(int decimalPlaces, bool onlyWorstMinusCounts, bool onlyBestPlusCounts, bool fireworksActive, decimal minimumCashPrizeForFireworks)
        {
            if (decimalPlaces < 0)
                throw new ArgumentOutOfRangeException(nameof(decimalPlaces), $"Number of decimal places in the prize money must be non-negative.");
            if (minimumCashPrizeForFireworks < 0)
                throw new ArgumentOutOfRangeException(nameof(minimumCashPrizeForFireworks), $"Minimum cash prize for fireworks must be non-negative.");

            return new GameSettings(decimalPlaces, onlyWorstMinusCounts, onlyBestPlusCounts, fireworksActive, minimumCashPrizeForFireworks);
        }

        public static GameSettings LoadFromFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException(nameof(fileName));
            if (!File.Exists(fileName))
                throw new FileNotFoundException("File not found.", nameof(fileName));

            string json = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<GameSettings>(json, Constants.JSON_SERIALIZER_OPTIONS) ?? throw new FileFormatException("Game settings loading failed.");
        }

        public void SaveToFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException(nameof(fileName));

            string json = JsonSerializer.Serialize<GameSettings>(this, Constants.JSON_SERIALIZER_OPTIONS);
            File.WriteAllText(fileName, json);
        }
    }
}
