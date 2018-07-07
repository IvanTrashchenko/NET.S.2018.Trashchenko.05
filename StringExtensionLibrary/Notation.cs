using System;
using System.Text;

namespace StringExtensionLibrary
{
    /// <summary>
    /// The notation class.
    /// </summary>
    public sealed class Notation
    {
        #region Constants

        /// <summary>
        /// The constraints on base of notation.
        /// </summary>
        private const int UpperValue = 16;
        private const int LowerValue = 2;

        #endregion

        #region Fields

        /// <summary>
        /// The base of notation.
        /// </summary>
        private int @base;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Notation"/> class.
        /// The base of notation - 10
        /// </summary>
        public Notation()
            : this(10)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Notation"/> class.
        /// </summary>
        /// <param name="base">
        /// The base of notation.
        /// </param>
        public Notation(int @base)
        {
            this.Base = @base;
            this.Alphabet = this.StringCreation();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the base.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if value is less than 2 or more than Constraint.
        /// </exception>
        public int Base
        {
            get => this.@base;

            set 
            {
                if (value < LowerValue || value > UpperValue)
                {
                    throw new ArgumentOutOfRangeException($"Base of Notation must be more or equal then {LowerValue} and less or equal then {UpperValue}!");
                }

                this.@base = value;
                this.Alphabet = this.StringCreation();
            }
        }

        /// <summary>
        /// Gets the alphabet string of notation.
        /// </summary>
        public string Alphabet { get; private set; }

        #endregion

        #region Private Methods

        private string StringCreation()
        {
            int @base = this.Base;

            StringBuilder sb = new StringBuilder(@base);

            int symbol = 'A';

            for (int i = 0; i < @base; i++)
            {
                if (i <= 9)
                {
                    sb.Append(i);
                }
                else
                {
                    sb.Append((char)symbol++);
                }
            }

            return sb.ToString();
        }

        #endregion
    }
}