namespace CluelessControl
{
    public sealed class CashCheque : BaseCheque
    {
        /// <summary>
        /// The cash amount on the cheque
        /// </summary>
        public decimal CashAmount
        {
            get;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="amount">Amount on the cash cheque</param>
        private CashCheque(decimal amount)
            : base()
        {
            CashAmount = amount;
        }

        /// <summary>
        /// Creates the cash cheque with a specified amount
        /// </summary>
        /// <param name="amount">The amount on the cheque</param>
        /// <returns>The created cheque</returns>
        internal static CashCheque Create(decimal amount)
        {
            return new CashCheque(amount);
        }

        public override string ToValueString()
        {
            if (CashAmount % 1 == 0)
                return CashAmount.ToString("#,##0");
            else
                return CashAmount.ToString("#,##0.00######");
        }

        public override Color GetTextColor()
        {
            if (CashAmount < 0)
                return Color.Red;
            else
                return Color.Black;
        }
    }
}
