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

        public static decimal Product<T>(this IEnumerable<T> items, Func<T, decimal> converter)
        {
            if (items is null)
                throw new ArgumentNullException(nameof(items));
            if (converter is null)
                throw new ArgumentNullException(nameof(converter));

            decimal result = 1;

            foreach (T item in items)
            {
                result *= converter(item);
            }

            return result;
        }
    }
}
