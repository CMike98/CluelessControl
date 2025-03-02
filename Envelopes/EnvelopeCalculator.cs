using CluelessControl.Cheques;

namespace CluelessControl.Envelopes
{
    public static class EnvelopeCalculator
    {
        public static decimal CalculateFinalPrize(GameSettings settings, List<Envelope> envelopeSet, decimal contestantCash = 0)
        {
            if (settings is null)
                throw new ArgumentNullException(nameof(settings));
            if (envelopeSet is null)
                throw new ArgumentNullException(nameof(envelopeSet));
            if (envelopeSet.Any(envelope => envelope is null))
                throw new ArgumentException("The envelope set mustn't contain null!", nameof(envelopeSet));
            if (contestantCash < 0)
                throw new ArgumentOutOfRangeException(nameof(contestantCash), $"Contestant cash mustn't be negative.");

            IEnumerable<BaseCheque> allCheques = envelopeSet
                .Where(envelope => envelope.State != EnvelopeState.FOR_DESTRUCTION && envelope.State != EnvelopeState.DESTROYED)
                .Select(envelope => envelope.Cheque);
            
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
            decimal multiplier = negativeMultiplier * positiveMultiplier;

            decimal prize = multiplier * cashTotal;
            decimal pow10 = Pow10(settings.DecimalPlaces);

            switch (settings.Rounding)
            {
                case RoundingMethod.MATHEMATICAL:
                    return Math.Round(prize, settings.DecimalPlaces, MidpointRounding.AwayFromZero);
                case RoundingMethod.ROUND_DOWN:
                    return Math.Floor(prize * pow10) / pow10;
                case RoundingMethod.ROUND_UP:
                    return Math.Ceiling(prize * pow10) / pow10;
                default:
                    throw new ArgumentException($"Undefined rounding method: {settings.Rounding}.", nameof(settings));
            }
        }

        private static decimal Pow10(int exponent)
        {
            if (exponent < 0)
                return 1.0m / Pow10(-exponent);
            else if (exponent == 0)
                return 1;

            decimal result = 1;

            for (int i = 0; i < exponent; ++i)
            {
                result *= 10;
            }

            return result;
        }
    }
}
