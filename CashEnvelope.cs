namespace CluelessControl
{
    public sealed class CashEnvelope : BaseEnvelope
    {
        public decimal CashAmount
        {
            get;
        }

        private CashEnvelope(decimal amount)
            : base()
        {
            CashAmount = amount;
        }

        internal static CashEnvelope Create(decimal amount)
        {
            return new CashEnvelope(amount);
        }

        public override string ToValueString()
        {
            if (CashAmount % 1 == 0)
                return CashAmount.ToString("#,##0");
            else
                return CashAmount.ToString("#,##0.00");
        }
    }
}
