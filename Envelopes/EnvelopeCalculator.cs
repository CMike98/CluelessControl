using CluelessControl.Cheques;
using CluelessControl.Prizes;

namespace CluelessControl.Envelopes
{
    public static class EnvelopeCalculator
    {
        public static string DetermineTotalPrize(List<Envelope> envelopeSet, decimal contestantCash = 0)
        {
            if (envelopeSet is null)
                throw new ArgumentNullException(nameof(envelopeSet));
            if (envelopeSet.Any(envelope => envelope is null))
                throw new ArgumentException("The envelope set mustn't contain null!", nameof(envelopeSet));
            if (contestantCash < 0)
                throw new ArgumentOutOfRangeException(nameof(contestantCash), $"Contestant cash mustn't be negative.");

            GameSettings settings = GameState.Instance.GameSettings;

            decimal multiplier = CalculateMultiplier(settings, envelopeSet);
            decimal money = CalculateMoneyPrize(envelopeSet, multiplier, contestantCash, settings.DecimalPlaces, settings.Rounding);
            List<string> prizes = CalculateAllPrizes(envelopeSet, multiplier);
            string prizesTogether = string.Join(" + ", prizes);

            if (prizes.Count > 0)
            {
                return money > 0 ? string.Format("{0} + {1}", Utils.AmountToString(money), prizesTogether) : prizesTogether;
            }
            else
            {
                return Utils.AmountToString(money);
            }
        }

        private static decimal CalculateMoneyPrize(List<Envelope> envelopeSet, decimal multiplier, decimal contestantCash, int decimalPlaces, RoundingMethod roundingMethod )
        {
            IEnumerable<BaseCheque> allCheques = envelopeSet
                .Where(envelope => envelope.State != EnvelopeState.FOR_DESTRUCTION && envelope.State != EnvelopeState.DESTROYED)
                .Select(envelope => envelope.Cheque);
            
            decimal cashTotal = contestantCash;

            foreach (var cheque in allCheques)
            {
                switch (cheque)
                {
                    case CashCheque cashCheque:
                        cashTotal += cashCheque.CashAmount;
                        break;
                    case PercentageCheque percentageCheque:
                        break; // Intentional ignorance - already dealt with
                    case PrizeCheque prizeCheque:
                        break; // Intentional ignorance
                    default:
                        throw new NotImplementedException($"Not implemented cheque type!");
                }
            }

            decimal prize = multiplier * cashTotal;
            decimal pow10 = Pow10(decimalPlaces);

            switch (roundingMethod)
            {
                case RoundingMethod.MATHEMATICAL:
                    return Math.Round(prize, decimalPlaces, MidpointRounding.AwayFromZero);
                case RoundingMethod.ROUND_DOWN:
                    return Math.Floor(prize * pow10) / pow10;
                case RoundingMethod.ROUND_UP:
                    return Math.Ceiling(prize * pow10) / pow10;
                default:
                    throw new ArgumentException($"Undefined rounding method: {roundingMethod}.", nameof(roundingMethod));
            }
        }

        private static List<string> CalculateAllPrizes(List<Envelope> envelopeSet, decimal multiplier)
        {
            Prizes.PrizeList? prizeList = GameState.Instance.PrizeList;

            if (prizeList is null)
                return new List<string>(); ;

            var result = new List<string>();

            IEnumerable<BaseCheque> allCheques = envelopeSet
                .Where(envelope => envelope.State != EnvelopeState.FOR_DESTRUCTION && envelope.State != EnvelopeState.DESTROYED)
                .Select(envelope => envelope.Cheque);

            Dictionary<string, decimal> prizeCounters = new Dictionary<string, decimal>();

            foreach (var cheque in allCheques)
            {
                switch (cheque)
                {
                    case CashCheque cashCheque:
                        break; // Intentional ignorance
                    case PercentageCheque percentageCheque:
                        break; // Intentional ignorance - already dealt with
                    case PrizeCheque prizeCheque:
                        decimal totalQuantity = prizeCounters.GetValueOrDefault(prizeCheque.PrizeCode, 0) + prizeCheque.PrizeQuantity;
                        if (!prizeCounters.TryAdd(prizeCheque.PrizeCode, totalQuantity))
                            prizeCounters[prizeCheque.PrizeCode] = totalQuantity;
                        break;
                    default:
                        throw new NotImplementedException($"Not implemented cheque type!");
                }
            }

            foreach (var prize in prizeCounters)
            {
                string code = prize.Key;
                decimal totalQuantity = prize.Value * multiplier;
                PrizeData prizeData = prizeList.GetPrizeByKey(code)!;

                totalQuantity = prizeData.RoundingMethod switch
                {
                    RoundingMethod.MATHEMATICAL => Math.Round(totalQuantity / prizeData.RoundingUnit, 0, MidpointRounding.AwayFromZero) * prizeData.RoundingUnit,
                    RoundingMethod.ROUND_DOWN => Math.Floor(totalQuantity / prizeData.RoundingUnit) * prizeData.RoundingUnit,
                    RoundingMethod.ROUND_UP => Math.Ceiling(totalQuantity / prizeData.RoundingUnit) * prizeData.RoundingUnit,
                    _ => throw new NotImplementedException($"Undefined rounding method: {prizeData.RoundingMethod}."),
                };

                if (totalQuantity == 1)
                    result.Add(prizeData.PrizeName);
                else
                    result.Add(string.Format("{0} x {1}", totalQuantity, prizeData.PrizeName));
            }

            return result;
        }

        private static decimal CalculateMultiplier(GameSettings settings, List<Envelope> envelopeSet)
        {
            IEnumerable<BaseCheque> allCheques = envelopeSet
                .Where(envelope => envelope.State != EnvelopeState.FOR_DESTRUCTION && envelope.State != EnvelopeState.DESTROYED)
                .Select(envelope => envelope.Cheque);

            var positivePercentages = new List<decimal>();
            var negativePercentages = new List<decimal>();

            foreach (var cheque in allCheques)
            {
                switch (cheque)
                {
                    case CashCheque cashCheque:
                        break; // Intentional ignorance
                    case PercentageCheque percentageCheque:
                        if (percentageCheque.Percentage < 0)
                            negativePercentages.Add(percentageCheque.CashMultiplier);
                        else if (percentageCheque.Percentage > 0)
                            positivePercentages.Add(percentageCheque.CashMultiplier);
                        break;
                    case PrizeCheque prizeCheque:
                        break; // Intentional ignorance
                    default:
                        throw new NotImplementedException($"Not implemented cheque type!");
                }
            }

            decimal positiveMultiplier = settings.OnlyBestPlusCounts ? positivePercentages.Max() : positivePercentages.Product();
            decimal negativeMultiplier = settings.OnlyWorstMinusCounts ? negativePercentages.Min() : negativePercentages.Product();
            decimal multiplier = negativeMultiplier * positiveMultiplier;

            return multiplier;
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
