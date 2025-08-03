using System.Text.Json;

namespace CluelessControl.Cheques
{
    public static class ChequeFactory
    {
        /// <summary>
        /// Creators collection
        /// </summary>
        private static readonly Dictionary<string, Func<JsonElement, BaseCheque>> _creators = new();

        /// <summary>
        /// Static constructor - registers the creators
        /// </summary>
        static ChequeFactory()
        {
            Register(nameof(CashCheque).ToLowerInvariant(), element =>
            {
                decimal amount = element.GetProperty("CashAmount").GetDecimal();
                return CreateCashCheque(amount);
            });
            Register(nameof(PercentageCheque).ToLowerInvariant(), element =>
            {
                decimal percentage = element.GetProperty("Percentage").GetDecimal();
                return CreatePercentageCheque(percentage);
            });
            Register(nameof(PrizeCheque).ToLowerInvariant(), element =>
            {
                string code = element.GetProperty("Code").GetString() ?? throw new JsonException("Prize code is not present.");
                decimal quantity = element.GetProperty("Quantity").GetDecimal();
                return CreatePrizeCheque(code, quantity);
            });
        }

        /// <summary>
        /// Registers the creator in the factory
        /// </summary>
        /// <param name="type">String representing the cheque type</param>
        /// <param name="creator">Function representing the creator</param>
        /// <exception cref="ArgumentNullException">Thrown if the cheque type is null or white space or the creator function is null</exception>
        public static void Register(string type, Func<JsonElement, BaseCheque> creator)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type));
            if (creator == null)
                throw new ArgumentNullException(nameof(creator));

            _creators[type] = creator;
        }

        /// <summary>
        /// Creates the cheque
        /// </summary>
        /// <param name="type">Type of the cheque</param>
        /// <param name="element">JsonElement representing the cheque</param>
        /// <returns>Created cheque</returns>
        /// <exception cref="ArgumentNullException">If string representing cheque is null or white space</exception>
        /// <exception cref="InvalidOperationException">If there's no creator for a specified type</exception>
        public static BaseCheque CreateCheque(string type, JsonElement element)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type));
            if (!_creators.TryGetValue(type, out var creator))
                throw new InvalidOperationException($"Unrecognized type: '{type}'");

            return creator(element);
        }

        /// <summary>
        /// Creates a cash cheque
        /// </summary>
        /// <param name="amount">Amount on the cheque</param>
        /// <returns>Created cheque</returns>
        public static CashCheque CreateCashCheque(decimal amount)
        {
            return CashCheque.Create(amount);
        }

        /// <summary>
        /// Creates a percentage cheque
        /// </summary>
        /// <param name="percentage">Percentage on the cheque</param>
        /// <returns>Created cheque</returns>
        public static PercentageCheque CreatePercentageCheque(decimal percentage)
        {
            return PercentageCheque.Create(percentage);
        }

        /// <summary>
        /// Creates a prize cheque
        /// </summary>
        /// <param name="code">Prize code</param>
        /// <param name="quantity">Quantity of the prize</param>
        /// <returns>A created cheque</returns>
        public static PrizeCheque CreatePrizeCheque(string code, decimal quantity)
        {
            return PrizeCheque.Create(code, quantity);
        }
    }
}
