using System.Text.Json.Serialization;

namespace CluelessControl
{
    public sealed class PercentageCheque : BaseCheque
    {
        /// <summary>
        /// The percentage on the cheque
        /// </summary>
        public decimal Percentage
        {
            get;
        }

        /// <summary>
        /// The multiplier to the prize money - created from the percentage
        /// </summary>
        [JsonIgnore]
        public decimal CashMultiplier => 1 + (Percentage / 100);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="percentage">Percentage on the cheque</param>
        private PercentageCheque(decimal percentage)
            : base()
        {
            Percentage = percentage;
        }

        /// <summary>
        /// Creates the percentage cheque with a specified percentage
        /// </summary>
        /// <param name="percentage">The percentage on the cheque</param>
        /// <returns>The created cheque</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the percentage is less than -100%.</exception>
        internal static PercentageCheque Create(decimal percentage)
        {
            if (percentage < -100)
                throw new ArgumentOutOfRangeException(nameof(percentage), "Minus percentage mustn't be less than -100%!");

            return new PercentageCheque(percentage);
        }

        public override string ToValueString()
        {
            decimal fraction = Percentage / 100;
            return fraction.ToString("+0.##########%;-0.##########%");
        }

        public override Color GetTextColor()
        {
            if (Percentage < 0)
                return Color.Red;
            else if (Percentage > 0)
                return Color.Green;
            else
                return Color.Black;
        }
    }
}
