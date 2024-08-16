using System.Text.Json.Serialization;

namespace CluelessControl
{
    public sealed class PercentageEnvelope : BaseEnvelope
    {
        public decimal Percentage
        {
            get;
        }

        [JsonIgnore]
        public decimal Multiplier => 1 + (Percentage / 100);

        private PercentageEnvelope(decimal percentage)
            : base()
        {
            Percentage = percentage;
        }

        internal static PercentageEnvelope Create(decimal percentage)
        {
            return new PercentageEnvelope(percentage);
        }

        public override string ToValueString()
        {
            decimal fraction = Percentage / 100;
            return fraction.ToString("+0.##############################%;-0.##############################%");
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
