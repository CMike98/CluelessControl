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

        public static string AnswerToLetter(int answer)
        {
            char letter = (char)(answer + 'A' - 1);
            return letter.ToString();
        }

        public static float Clamp(float val, float min, float max)
        {
            if (val < min)
                return min;
            if (val > max)
                return max;
            return val;
        }

        public static int Clamp(int val, int min, int max)
        {
            if (val < min)
                return min;
            if (val > max)
                return max;
            return val;
        }

        public static float Lerp(float a, float b, float t)
        {
            return a * (1 - t) + b * t;
        }
    }
}
