namespace CluelessControl.Prizes
{
    public class PrizeList
    {
        public Dictionary<string, PrizeData> PrizeDictionary
        {
            get;
            private set;
        }

        private PrizeList()
        {
            PrizeDictionary = new Dictionary<string, PrizeData>();
        }

        private PrizeList(Dictionary<string, PrizeData> prizes)
        {
            if (prizes is null)
                throw new ArgumentNullException(nameof(prizes));
            if (prizes.Keys.Any(string.IsNullOrWhiteSpace))
                throw new ArgumentException("At least one prize key is null or empty.", nameof(prizes));
            if (prizes.Values.Any(val => val is null))
                throw new ArgumentException("At least one prize doesn't exist.", nameof(prizes));

            PrizeDictionary = prizes;
        }

        public static PrizeList Create()
        {
            return new PrizeList();
        }

        public static PrizeList Create(PrizeList prizeList)
        {
            if (prizeList is null)
                throw new ArgumentNullException(nameof(prizeList));

            return new PrizeList(prizeList.PrizeDictionary);
        }

        public static PrizeList Create(Dictionary<string, PrizeData> prizeDictionary)
        {
            if (prizeDictionary is null)
                throw new ArgumentNullException(nameof(prizeDictionary));
            if (prizeDictionary.Keys.Any(string.IsNullOrWhiteSpace))
                throw new ArgumentException("At least one key is missing.", nameof(prizeDictionary));
            if (prizeDictionary.Values.Any(prize => prize is null))
                throw new ArgumentException("At least one prize is null.", nameof(prizeDictionary));

            return new PrizeList(prizeDictionary);
        }

        public void ClearList()
        {
            PrizeDictionary.Clear();
        }

        public bool AddPrize(string key, PrizeData prize)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            if (prize is null)
                throw new ArgumentNullException(nameof(prize));

            if (PrizeDictionary.ContainsKey(key))
                return false;

            PrizeDictionary.Add(key, prize);
            return true;
        }

        public bool RemovePrize(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            if (!PrizeDictionary.ContainsKey(key))
                return false;

            return PrizeDictionary.Remove(key);
        }

        public bool ChangePrize(string key, PrizeData newPrize)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            if (newPrize is null)
                throw new ArgumentNullException(nameof(newPrize));
            if (!PrizeDictionary.ContainsKey(key))
                return false;

            PrizeDictionary[key] = newPrize;
            return true;
        }

        public PrizeData? GetPrizeByKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            if (!PrizeDictionary.TryGetValue(key, out PrizeData? value))
                return null;

            return value;
        }
    }
}
