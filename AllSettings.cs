using CluelessControl.Cheques;
using CluelessControl.Constants;
using CluelessControl.Prizes;
using System.Text.Json;

namespace CluelessControl
{
    public class AllSettings
    {
        public PrizeList? PrizeList
        {
            get;
            private set;
        }

        public ChequeSettings? ChequeSettings
        {
            get;
            private set;
        }

        private AllSettings(ChequeSettings chequeSettings, PrizeList prizeList)
        {
            if (chequeSettings is null)
                throw new ArgumentNullException(nameof(chequeSettings));
            if (prizeList is null)
                throw new ArgumentNullException(nameof(prizeList));

            ChequeSettings = chequeSettings;
            PrizeList = prizeList;
        }

        private AllSettings(ChequeSettings chequeSettings)
        {
            if (chequeSettings is null)
                throw new ArgumentNullException(nameof(chequeSettings));

            ChequeSettings = chequeSettings;
            PrizeList = null;
        }

        public static AllSettings Create(ChequeSettings chequeSettings, PrizeList? prizeList = null)
        {
            if (chequeSettings is null)
                throw new ArgumentNullException(nameof(chequeSettings));

            AllSettings settings;
            if (prizeList is null)
                settings = new AllSettings(chequeSettings);
            else
                settings = new AllSettings(chequeSettings, prizeList);

            return settings;
        }

        public static AllSettings LoadFromFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException(nameof(fileName));
            if (!File.Exists(fileName))
                throw new FileNotFoundException("File not found.", nameof(fileName));

            string json = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<AllSettings>(json, GameConstants.JSON_SERIALIZER_OPTIONS) ?? throw new FileFormatException("All settings loading failed.");
        }

        public void SaveToFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException(nameof(fileName));

            string json = JsonSerializer.Serialize(this, GameConstants.JSON_SERIALIZER_OPTIONS);
            File.WriteAllText(fileName, json);
        }
    }
}
