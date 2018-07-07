using System;

namespace StringExtensionLibrary
{
    /// <summary>
    /// Class of coverting method.
    /// </summary>
    public static class StringExtention
    {
        /// <summary>
        /// Method which converts string presentation number in specific notation to decimal notation.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <param name="notation">The notation.</param>
        /// <returns>The result number in decimal notation.</returns>
        /// <exception cref="ArgumentException">Thrown when the source string is null or empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the notation is null.</exception>
        public static int ConvertToDecimal(this string source, Notation notation) 
        {
            if (string.IsNullOrEmpty(source))
            {
                throw new ArgumentException($"The string {nameof(source)} can not be null or empty.");
            }

            if (notation == null)
            {
                throw new ArgumentNullException($"The object {nameof(notation)} can not be null.");
            }

            int result = 0, temp = 1;

            string upperString = source.ToUpper();

            for (int i = source.Length - 1; i >= 0; i--)
            {
                checked
                {
                    int symbolValue = ConvertToValue(upperString[i], notation.Alphabet);

                    if (symbolValue == -1)
                    {
                        throw new ArgumentException($"Invalid symbol {source[i]} in string.");
                    }

                    result += temp * symbolValue;

                    temp *= notation.Base;
                }
            }

            return result;
        }

        private static int ConvertToValue(char symbol, string alphabet) => alphabet.IndexOf(symbol);      
    }
}
