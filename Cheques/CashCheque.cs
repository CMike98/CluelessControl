namespace CluelessControl.Cheques
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
        /// <param name="amount">Amount on the cash cheque</param>
        private CashCheque(decimal amount)
            : base()
        {
            CashAmount = amount;

            ValueString = Utils.AmountToString(CashAmount);

            if (CashAmount < 0)
                DefaultTextColor = Color.Red;
            else
                DefaultTextColor = Color.Black;
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

        public override BaseCheque CloneCheque()
        {
            return new CashCheque(CashAmount);
        }
    }
}
