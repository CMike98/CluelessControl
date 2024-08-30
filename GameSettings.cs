using System.Text.Json;

namespace CluelessControl
{
    public class GameSettings
    {
        public int StartEnvelopeCount
        {
            get;
        }

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

        private GameSettings(int startEnvelopeCount, int decimalPlaces, bool onlyWorstMinusCounts, bool onlyBestPlusCounts)
        {
            StartEnvelopeCount = startEnvelopeCount;
            DecimalPlaces = decimalPlaces;
            OnlyWorstMinusCounts = onlyWorstMinusCounts;
            OnlyBestPlusCounts = onlyBestPlusCounts;
        }

        internal static GameSettings Create()
        {
            return new GameSettings(startEnvelopeCount: Constants.ENVELOPE_DEFAULT_COUNT, decimalPlaces: 0, onlyWorstMinusCounts: false, onlyBestPlusCounts: false);
        }

        internal static GameSettings Create(int startEnvelopeCount, int decimalPlaces, bool onlyWorstMinusCounts, bool onlyBestPlusCounts)
        {
            if (startEnvelopeCount < 1 || startEnvelopeCount > Constants.MAX_ENVELOPE_POSSIBLE_COUNT)
                throw new ArgumentOutOfRangeException(nameof(startEnvelopeCount), $"Number of envelopes to pick must be between 1 and {Constants.MAX_ENVELOPE_POSSIBLE_COUNT}.");
            if (decimalPlaces < 0)
                throw new ArgumentOutOfRangeException(nameof(decimalPlaces), $"Number of decimal places in the prize money must be non-negative.");

            return new GameSettings(startEnvelopeCount, decimalPlaces, onlyWorstMinusCounts, onlyBestPlusCounts);
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
