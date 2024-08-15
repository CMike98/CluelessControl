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

        public override string ToHostString()
        {
            decimal fraction = Percentage / 100;
            return fraction.ToString("+0.##############################%;-0.##############################%");
        }
    }
}
