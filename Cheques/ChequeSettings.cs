using System.Text.Json;
using CluelessControl.Constants;

namespace CluelessControl.Cheques
{
    public class ChequeSettings
    {
        public List<BaseCheque> ChequeList
        {
            get;
            private set;
        }

        private ChequeSettings(List<BaseCheque> chequeList)
        {
            ChequeList = chequeList;
        }

        internal static ChequeSettings Create()
        {
            return new ChequeSettings(new List<BaseCheque>());
        }

        internal static ChequeSettings Create(List<BaseCheque> chequeList)
        {
            if (chequeList == null)
                throw new ArgumentNullException(nameof(chequeList));
            if (chequeList.Any(cheque => cheque == null))
                throw new ArgumentException($"At least one of the cheques on the list is null.", nameof(chequeList));
            return new ChequeSettings(chequeList);
        }

        public void ClearChequeList()
        {
            ChequeList.Clear();
        }

        public void Randomise(Random? rand = null)
        {
            rand ??= new Random();

            for (int i = ChequeList.Count - 1; i > 0; --i)
            {
                int randomIndex = rand.Next(i + 1);
                (ChequeList[i], ChequeList[randomIndex]) = (ChequeList[randomIndex], ChequeList[i]);
            }
        }

        public static ChequeSettings LoadFromFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException(nameof(fileName));
            if (!File.Exists(fileName))
                throw new FileNotFoundException("File not found.", nameof(fileName));

            string json = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<ChequeSettings>(json, GameConstants.JSON_SERIALIZER_OPTIONS) ?? throw new FileFormatException("Envelope settings loading failed.");
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
