using CluelessControl.Prizes;

namespace CluelessControl.Cheques
{
    public class PrizeCheque : BaseCheque
    {
        /// <summary>
        /// Prize inside the envelope
        /// </summary>
        public Prize PrizeInside
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

        /// <summary>
        /// Constructor
        /// </summary>
        private PrizeCheque(Prize prize, decimal prizeQuantity)
            : base()
        {
            PrizeInside = prize;
            PrizeQuantity = prizeQuantity;
        }

        /// <summary>
        /// Creates a new prize cheque
        /// </summary>
        /// <returns>The created cheque</returns>
        internal static PrizeCheque Create(string prizeCode, decimal prizeQuantity)
        {
            if (string.IsNullOrWhiteSpace(prizeCode))
                throw new ArgumentNullException(nameof(prizeCode));
            if (prizeQuantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(prizeQuantity), "Prize quantity must be greater than zero!");

            Prize? prize = GameState.Instance.PrizeList.GetPrizeByKey(prizeCode);

            if (prize is null)
                throw new ArgumentException("Prize doesn't correspond to any prize on the list.", nameof(prizeCode));

            return new PrizeCheque(prize, prizeQuantity);
        }

        public override string ToValueString()
        {
            if (PrizeQuantity == 1)
                return PrizeInside.PrizeName;
            else
                return string.Format("{0} x {1}", PrizeQuantity, PrizeInside.PrizeName);
        }

        public override Color GetDefaultTextColor()
        {
            return Color.Black;
        }

        public override BaseCheque CloneCheque()
        {
            return new PrizeCheque(PrizeInside, PrizeQuantity);
        }
    }
}
