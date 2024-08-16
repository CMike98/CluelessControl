namespace CluelessControl
{
    public sealed class CashCheque : BaseCheque
    {
        public decimal CashAmount
        {
            get;
        }

        private CashCheque(decimal amount)
            : base()
        {
            CashAmount = amount;
        }

        internal static CashCheque Create(decimal amount)
        {
            return new CashCheque(amount);
        }

        public override string ToValueString()
        {
            if (CashAmount % 1 == 0)
                return CashAmount.ToString("#,##0");
            else
                return CashAmount.ToString("#,##0.00");
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
