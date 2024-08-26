namespace CluelessControl
{
    public static class Utils
    {
        public static string AmountToString(decimal amount)
        {
            if (amount % 1 == 0)
                return amount.ToString("#,##0");
            else
                return amount.ToString("#,##0.00######");
        }

        public static string PercentageToString(decimal percentage)
        {
            decimal fraction = percentage / 100;
            return fraction.ToString("+#,##0.##########%;-#,##0.##########%");
        }
    }
}
