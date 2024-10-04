namespace CluelessControl
{
    public static class Extensions
    {
        public static decimal Product(this IEnumerable<decimal> numbers)
        {
            if (numbers is null)
                throw new ArgumentNullException(nameof(numbers));

            decimal result = 1;

            foreach (decimal number in numbers)
            {
                result *= number;
            }

            return result;
        }
    }
}
