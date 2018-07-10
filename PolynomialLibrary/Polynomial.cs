using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace PolynomialLibrary
{
    /// <summary>
    /// Immutable polynomial class.
    /// </summary>
    public sealed class Polynomial : ICloneable, IEquatable<Polynomial>
    {

        #region Fields

        /// <summary>
        /// The precision of calculations when comparing two variables of the double type.
        /// </summary>
        private static readonly double PRECISION;

        /// <summary>
        /// Coefficients of polynomial.
        /// </summary>
        private readonly double[] coefficients;

        #endregion

        #region Constructors

        /// <summary>
        /// Static constructor for precision parsing from app.config.
        /// </summary>
        static Polynomial()
        {
            try
            {
                PRECISION = double.Parse(ConfigurationManager.AppSettings["precision"]);

            }
            catch (ArgumentNullException)
            {
                PRECISION = 0.0000001;
            }
        }

        /// <summary>
        /// Constructs a new polynomial of degree equal to 0.
        /// </summary>
        public Polynomial()
            : this(0)
        {
        }

        /// <summary>
        /// Constructs a new polynomial of the specified degree.
        /// </summary>
        /// <param name="degree">Degree of polynomial.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when degree value is negative.</exception>
        public Polynomial(int degree)
        {
            if (degree < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(degree)} can not be negative.");
            }

            this.Degree = degree;
            this.coefficients = new double[degree + 1];

            List<double> coeffsList = new List<double>();

            for (int i = 0; i <= degree; i++)
            {
                coeffsList.Add(1.0);
            }

            Array.Copy(coeffsList.ToArray(), this.coefficients, coeffsList.ToArray().Length);
        }

        /// <summary>
        /// Constructs a new polynomial with the specified coefficients.
        /// </summary>
        /// <param name="coefficients">Coefficients of polynomial.</param>
        /// <exception cref="ArgumentNullException">Thrown when coefficients array is equal to null.</exception>
        /// <exception cref="ArgumentException">Thrown when coefficients array is empty.</exception>
        public Polynomial(params double[] coefficients)
        {
            if (coefficients == null)
            {
                throw new ArgumentNullException($"{nameof(coefficients)} can not be equal to null.");
            }

            if (coefficients.Length == 0)
            {
                throw new ArgumentException($"{nameof(coefficients)} can not be empty.");
            }

            Stack<double> coeffStack = new Stack<double>(coefficients);

            for (int i = coefficients.Length - 1; i > 0; i--)
            {
                if (Math.Abs(coefficients[i]) < PRECISION)
                {
                    coeffStack.Pop();
                }
                else
                {
                    break;
                }
            }

            this.Degree = coeffStack.Count - 1;

            this.coefficients = new double[coeffStack.Count];

            Array.Copy(coeffStack.Reverse().ToArray(), this.coefficients, coeffStack.Count);
        }

        #endregion

        #region Public methods + properties

        /// <summary>
        /// Gets the degree of polynomial.
        /// </summary>
        public int Degree { get; }

        /// <summary>
        /// Returns copy of polymomial's coefficients array.
        /// </summary>
        /// <returns>Array of <see cref="double"/> type.</returns>
        public double[] ToArray()
        {
            var copyArray = new double[this.Degree + 1];

            Array.Copy(this.coefficients, copyArray, this.Degree + 1);

            return copyArray;
        }

        /// <summary>
        /// Indexer of <see cref="Polynomial"/> class.
        /// </summary>
        /// <param name="index">Current index.</param>       
        /// <returns>Coefficient of polynomial at the specified index.</returns>
        /// <exception cref="IndexOutOfRangeException">Throws when index is out of range.</exception>
        public double this[int index]
        {
            get
            {
                if (index < 0 || index > this.Degree)
                {
                    throw new IndexOutOfRangeException($"{nameof(index)} is out of range.");
                }

                return this.coefficients[index];
            }
        }

        #endregion

        #region Object overrided methods

        public override string ToString()
        {
            if (this.Degree == 0 && Math.Abs(this[0]) < PRECISION)
            {
                return "0";
            }

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i <= this.Degree; i++)
            {
                if (Math.Abs(this[i]) < PRECISION)
                {
                    continue;
                }

                if (Math.Abs(this[i] - 1) < PRECISION)
                {
                    if (i == this.Degree)
                    {
                        sb.Append("x^" + i);
                    }
                    else
                    {
                        sb.Append("x^" + i + " + ");
                    }
                }
                else
                {
                    if (i == this.Degree)
                    {
                        sb.Append(this[i] + "x^" + i);
                    }
                    else
                    {
                        sb.Append(this[i] + "x^" + i + " + ");
                    }
                }
            }

            return sb.ToString();
        }

        public override bool Equals(object other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (this.GetType() != other.GetType())
            {
                return false;
            }

            Polynomial polynomial = (Polynomial)other;

            return this.Equals(polynomial);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashResult = 397;

                foreach (var item in this.coefficients)
                {
                    hashResult += item.GetHashCode();
                }

                return hashResult ^ this.Degree;
            }
        }

        #endregion

        #region Operator methods

        public static Polynomial Add(Polynomial lhs, Polynomial rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException($"{nameof(lhs)} can not be null.");
            }

            if (rhs == null)
            {
                throw new ArgumentNullException($"{nameof(rhs)} can not be null.");
            }

            int maxDegree = (lhs.Degree > rhs.Degree) ? lhs.Degree : rhs.Degree;
            var resultCoeffs = new double[maxDegree + 1];
            Array.Copy(lhs.ToArray(), resultCoeffs, lhs.Degree + 1);

            for (int i = 0; i <= rhs.Degree; i++)
            {
                resultCoeffs[i] += rhs[i];
            }

            return new Polynomial(resultCoeffs);
        }

        public static Polynomial Substract(Polynomial lhs, Polynomial rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException($"{nameof(lhs)} can not be null.");
            }

            if (rhs == null)
            {
                throw new ArgumentNullException($"{nameof(rhs)} can not be null.");
            }

            int maxDegree = (lhs.Degree > rhs.Degree) ? lhs.Degree : rhs.Degree;

            var resultCoeffs = new double[maxDegree + 1];
            Array.Copy(lhs.ToArray(), resultCoeffs, lhs.Degree + 1);

            for (int i = 0; i <= rhs.Degree; i++)
            {
                resultCoeffs[i] -= rhs[i];
            }

            return new Polynomial(resultCoeffs);
        }

        public static Polynomial Multiply(Polynomial lhs, Polynomial rhs)
        {

            if (lhs == null)
            {
                throw new ArgumentNullException($"{nameof(lhs)} can not be null.");
            }

            if (rhs == null)
            {
                throw new ArgumentNullException($"{nameof(rhs)} can not be null.");
            }

            var resultCoeffs = new double[lhs.Degree + rhs.Degree + 2];

            for (int i = 0; i <= lhs.Degree; i++)
            {
                for (int j = 0; j <= rhs.Degree; j++)
                {
                    resultCoeffs[i + j] += lhs[i] * rhs[j];
                }
            }

            return new Polynomial(resultCoeffs);
        }

        #endregion

        #region Overloaded operators

        public static Polynomial operator +(Polynomial lhs, Polynomial rhs) => Add(lhs, rhs);

        public static bool operator ==(Polynomial lhs, Polynomial rhs) => lhs.Equals(rhs);

        public static bool operator !=(Polynomial lhs, Polynomial rhs) => !lhs.Equals(rhs);

        public static Polynomial operator *(Polynomial lhs, Polynomial rhs) => Multiply(lhs, rhs);

        public static Polynomial operator -(Polynomial lhs, Polynomial rhs) => Substract(lhs, rhs);

        #endregion

        #region Interface overrided methods

        public bool Equals(Polynomial other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (this.Degree != other.Degree)
            {
                return false;
            }

            for (int i = 0; i <= this.Degree; i++)
            {
                if (Math.Abs(this[i] - other[i]) > PRECISION)
                {
                    return false;
                }
            }

            return true;
        }

        object ICloneable.Clone() => this.Clone();

        public Polynomial Clone() => new Polynomial(this.coefficients);

        #endregion

    }
}