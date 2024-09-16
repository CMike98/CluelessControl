using CluelessControl.Cheques;

namespace CluelessControl.Envelopes
{
    public static class EnvelopeCalculator
    {
        public static decimal CalculateFinalPrize(GameSettings settings, EnvelopeSet envelopeSet, decimal contestantCash = 0)
        {
            if (settings is null)
                throw new ArgumentNullException(nameof(settings));
            if (envelopeSet is null)
                throw new ArgumentNullException(nameof(envelopeSet));
            if (contestantCash < 0)
                throw new ArgumentOutOfRangeException(nameof(contestantCash), $"Contestant cash mustn't be negative.");

            decimal cash = contestantCash;
            decimal multiplier = 1;

            IEnumerable<BaseCheque> cheques = envelopeSet.GetCheques();
            foreach (var cheque in cheques)
            {
                switch (cheque)
                {
                    case CashCheque cashCheque:
                        cash += cashCheque.CashAmount;
                        break;
                    case PercentageCheque percentageCheque:
                        multiplier *= percentageCheque.CashMultiplier;
                        break;
                    default:
                        throw new NotImplementedException($"Not implemented cheque type!");
                }
            }

            return cash * multiplier;
        }

        public static string CalculateValueInsideEnvelopes(EnvelopeSet envelopeSet)
        {
            if (envelopeSet is null)
                throw new ArgumentNullException(nameof(envelopeSet));

            var cheques = envelopeSet.GetCheques();
            var cashCheques = cheques.OfType<CashCheque>();
            var percentageCheques = cheques.OfType<PercentageCheque>();

            if (cashCheques.Any())
            {
                decimal sum = cashCheques.Sum(cheque => cheque.CashAmount);
                return Utils.AmountToString(sum);
            }
            else if (percentageCheques.Any())
            {
                decimal minPercentage = percentageCheques.Min(cheque => cheque.Percentage);
                return Utils.PercentageToString(minPercentage);
            }
            else
            {
                return Utils.AmountToString(amount: 0);
            }
        }
    }
}
