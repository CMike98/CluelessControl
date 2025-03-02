using System.Text.Json;
using CluelessControl.Constants;

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

        public bool ShowAmountsOnTv
        {
            get;
        }

        public RoundingMethod Rounding
        {
            get;
        }

        private GameSettings(int startEnvelopeCount, int decimalPlaces, bool onlyWorstMinusCounts, bool onlyBestPlusCounts, bool showAmountsOnTv, RoundingMethod roundingMethod)
        {
            StartEnvelopeCount = startEnvelopeCount;
            DecimalPlaces = decimalPlaces;
            OnlyWorstMinusCounts = onlyWorstMinusCounts;
            OnlyBestPlusCounts = onlyBestPlusCounts;
            ShowAmountsOnTv = showAmountsOnTv;
            Rounding = roundingMethod;
        }

        internal static GameSettings Create()
        {
            return new GameSettings(
                startEnvelopeCount: GameConstants.ENVELOPE_DEFAULT_COUNT,
                decimalPlaces: GameConstants.DEFAULT_DECIMAL_PLACES,
                onlyWorstMinusCounts: false,
                onlyBestPlusCounts: false,
                showAmountsOnTv: true,
                roundingMethod: RoundingMethod.MATHEMATICAL);
        }

        internal static GameSettings Create(int startEnvelopeCount, int decimalPlaces, bool onlyWorstMinusCounts, bool onlyBestPlusCounts, bool showAmountsOnTv, RoundingMethod roundingMethod)
        {
            if (startEnvelopeCount < 1 || startEnvelopeCount > GameConstants.MAX_ENVELOPE_COUNT_PERSON)
                throw new ArgumentOutOfRangeException(nameof(startEnvelopeCount), $"Number of envelopes to pick must be between 1 and {GameConstants.MAX_ENVELOPE_COUNT_PERSON}.");
            if (decimalPlaces < 0)
                throw new ArgumentOutOfRangeException(nameof(decimalPlaces), $"Number of decimal places in the prize money must be non-negative.");

            return new GameSettings(startEnvelopeCount, decimalPlaces, onlyWorstMinusCounts, onlyBestPlusCounts, showAmountsOnTv, roundingMethod);
        }

        public static GameSettings LoadFromFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException(nameof(fileName));
            if (!File.Exists(fileName))
                throw new FileNotFoundException("File not found.", nameof(fileName));

            string json = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<GameSettings>(json, GameConstants.JSON_SERIALIZER_OPTIONS) ?? throw new FileFormatException("Game settings loading failed.");
        }

        public void SaveToFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException(nameof(fileName));

            string json = JsonSerializer.Serialize<GameSettings>(this, GameConstants.JSON_SERIALIZER_OPTIONS);
            File.WriteAllText(fileName, json);
        }
    }
}
