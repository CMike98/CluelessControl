namespace CluelessControl.Cheques
{
    public abstract class BaseCheque
    {
        /// <summary>
        /// Get the color of the amount text
        /// </summary>
        public abstract Color DefaultTextColor
        {
            get;
            init;
        }

        /// <summary>
        /// Get the string representing the amount
        /// </summary>
        public abstract string ValueString
        {
            get;
            init;
        }

        /// <summary>
        /// Creates a new cheque with the same values as the cloned cheque
        /// </summary>
        /// <returns></returns>
        public abstract BaseCheque CloneCheque();
    }
}
