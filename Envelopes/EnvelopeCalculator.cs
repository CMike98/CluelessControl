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

            IEnumerable<BaseCheque> allCheques = envelopeSet.GetCheques();
            
            decimal cashTotal = contestantCash;
            var positivePercentages = new List<decimal>();
            var negativePercentages = new List<decimal>();

            foreach (var cheque in allCheques)
            {
                switch (cheque)
                {
                    case CashCheque cashCheque:
                        cashTotal += cashCheque.CashAmount;
                        break;
                    case PercentageCheque percentageCheque:
                        if (percentageCheque.Percentage < 0)
                            negativePercentages.Add(percentageCheque.CashMultiplier);
                        else if (percentageCheque.Percentage > 0)
                            positivePercentages.Add(percentageCheque.CashMultiplier);
                        break;
                    default:
                        throw new NotImplementedException($"Not implemented cheque type!");
                }
            }

            decimal positiveMultiplier = settings.OnlyBestPlusCounts   ? positivePercentages.Max() : positivePercentages.Product();
            decimal negativeMultiplier = settings.OnlyWorstMinusCounts ? negativePercentages.Min() : negativePercentages.Product();
            decimal multiplier = positiveMultiplier * negativeMultiplier;

            return Math.Round(cashTotal * multiplier, settings.DecimalPlaces, MidpointRounding.AwayFromZero);
        }

        public static string CalculateValueInsideEnvelopes(EnvelopeSet envelopeSet)
        {
            if (envelopeSet is null)
                throw new ArgumentNullException(nameof(envelopeSet));

            IEnumerable<BaseCheque> cheques = envelopeSet.GetCheques();
            IEnumerable<CashCheque> cashCheques = cheques.OfType<CashCheque>();
            IEnumerable<PercentageCheque> percentageCheques = cheques.OfType<PercentageCheque>();

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
