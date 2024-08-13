using System.Text.Json;

namespace CluelessControl
{
    public static class EnvelopeFactory
    {
        private static readonly Dictionary<string, Func<JsonElement, BaseEnvelope>> _creators = new();

        static EnvelopeFactory()
        {
            Register(nameof(CashEnvelope).ToLowerInvariant(), element =>
            {
                var amountElement = element.GetProperty("CashAmount");
                decimal amount = amountElement.GetDecimal();
                return CreateCashEnvelope(amount);
            });
            Register(nameof(PercentageEnvelope).ToLowerInvariant(), element =>
            {
                var percentageElement = element.GetProperty("Percentage");
                decimal percentage = percentageElement.GetDecimal();
                return CreatePercentageEnvelope(percentage);
            });
        }

        public static void Register(string type, Func<JsonElement, BaseEnvelope> creator)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type));
            if (creator == null)
                throw new ArgumentNullException(nameof(creator));

            _creators[type] = creator;
        }

        public static BaseEnvelope CreateEnvelope(string type, JsonElement element)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type));
            if (!_creators.TryGetValue(type, out var creator))
                throw new InvalidOperationException($"Unrecognized type: '{type}'");

            return creator(element);
        }

        public static CashEnvelope CreateCashEnvelope(decimal amount)
        {
            return CashEnvelope.Create(amount);
        }

        public static PercentageEnvelope CreatePercentageEnvelope(decimal percentage)
        {
            return PercentageEnvelope.Create(percentage);
        }
    }
}
