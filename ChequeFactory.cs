using System.Text.Json;

namespace CluelessControl
{
    public static class ChequeFactory
    {
        private static readonly Dictionary<string, Func<JsonElement, BaseCheque>> _creators = new();

        static ChequeFactory()
        {
            Register(nameof(CashCheque).ToLowerInvariant(), element =>
            {
                var amountElement = element.GetProperty("CashAmount");
                decimal amount = amountElement.GetDecimal();
                return CreateCashCheque(amount);
            });
            Register(nameof(PercentageCheque).ToLowerInvariant(), element =>
            {
                var percentageElement = element.GetProperty("Percentage");
                decimal percentage = percentageElement.GetDecimal();
                return CreatePercentageCheque(percentage);
            });
        }

        public static void Register(string type, Func<JsonElement, BaseCheque> creator)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type));
            if (creator == null)
                throw new ArgumentNullException(nameof(creator));

            _creators[type] = creator;
        }

        public static BaseCheque CreateCheque(string type, JsonElement element)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type));
            if (!_creators.TryGetValue(type, out var creator))
                throw new InvalidOperationException($"Unrecognized type: '{type}'");

            return creator(element);
        }

        public static CashCheque CreateCashCheque(decimal amount)
        {
            return CashCheque.Create(amount);
        }

        public static PercentageCheque CreatePercentageCheque(decimal percentage)
        {
            return PercentageCheque.Create(percentage);
        }
    }
}
