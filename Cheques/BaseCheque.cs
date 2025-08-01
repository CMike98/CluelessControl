namespace CluelessControl.Cheques
{
    public abstract class BaseCheque
    {
        /// <summary>
        /// Get the color of the amount text
        /// </summary>
        /// <returns>The color of the cheque</returns>
        public abstract Color GetDefaultTextColor();

        /// <summary>
        /// Get the string representing the amount
        /// </summary>
        /// <returns>String representing the amount</returns>
        public abstract string ToValueString();

        /// <summary>
        /// Creates a new cheque with the same values as the cloned cheque
        /// </summary>
        /// <returns></returns>
        public abstract BaseCheque CloneCheque();
    }
}
