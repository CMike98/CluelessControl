using CluelessControl.Prizes;
using System.Text.Json.Serialization;

namespace CluelessControl.Cheques
{
    public class PrizeCheque : BaseCheque
    {
        /// <summary>
        /// Code of the prize inside
        /// </summary>
        public string PrizeCode
        {
            get;
            private set;
        }

        /// <summary>
        /// Prize inside the envelope
        /// </summary>
        [JsonIgnore]
        public PrizeData PrizeInside
        {
            get;
        }

        /// <summary>
        /// Quantity of the prize
        /// </summary>
        public decimal PrizeQuantity
        {
            get;
        }

        public override Color DefaultTextColor
        {
            get;
            init;
        }

        public override string ValueString
        {
            get;
            init;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        private PrizeCheque(string prizeCode, PrizeData prize, decimal prizeQuantity)
            : base()
        {
            PrizeCode = prizeCode;
            PrizeInside = prize;
            PrizeQuantity = prizeQuantity;

            if (PrizeQuantity == 1)
                ValueString = PrizeInside.PrizeName;
            else
                ValueString = string.Format("{0} x {1}", PrizeQuantity, PrizeInside.PrizeName);

            DefaultTextColor = Color.Black;
        }

        /// <summary>
        /// Creates a new prize cheque
        /// </summary>
        /// <returns>The created cheque</returns>
        internal static PrizeCheque Create(string prizeCode, decimal prizeQuantity, PrizeList? prizeList = null)
        {
            if (string.IsNullOrWhiteSpace(prizeCode))
                throw new ArgumentNullException(nameof(prizeCode));
            if (prizeQuantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(prizeQuantity), "Prize quantity must be greater than zero!");

            prizeList ??= GameState.Instance.PrizeList;
            PrizeData? prize = prizeList?.GetPrizeByKey(prizeCode);

            if (prize is null)
                throw new ArgumentException("Prize doesn't correspond to any prize on the list.", nameof(prizeCode));

            return new PrizeCheque(prizeCode, prize, prizeQuantity);
        }

        public override BaseCheque CloneCheque()
        {
            return new PrizeCheque(PrizeCode, PrizeInside, PrizeQuantity);
        }
    }
}
