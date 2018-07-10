using System;
using NUnit.Framework;

namespace PolynomialLibrary.Tests
{
    [TestFixture]
    public class PolynomialTests
    {
        [TestCase(1, 2, 3, 4, ExpectedResult = "x^0 + 2x^1 + 3x^2 + 4x^3")]
        [TestCase(new double[] { 5, 0, 9, 45 }, ExpectedResult = "5x^0 + 9x^2 + 45x^3")]
        [TestCase(16, 25, 8, 0, 34, 7.3, 2334556, 12, 0, 0, 0, 1, 0, 0, 0, ExpectedResult = "16x^0 + 25x^1 + 8x^2 + 34x^4 + 7,3x^5 + 2334556x^6 + 12x^7 + x^11")]
        [TestCase(0d, ExpectedResult = "0")]
        public string Polynomial_DoubleParams_ToString_Test(params double[] coefficients)
        {
            Polynomial polynomial = new Polynomial(coefficients);

            return polynomial.ToString();
        }

        [TestCase(null)]
        public void Polynomial_DoubleParams_ThrowsArgumentNullException(double[] coefficients)
        {
            Assert.Throws<ArgumentNullException>(() => new Polynomial(coefficients));
        }

        [TestCase(0, ExpectedResult = "x^0")]
        [TestCase(5, ExpectedResult = "x^0 + x^1 + x^2 + x^3 + x^4 + x^5")]
        public string Polynomial_IntDegree_ToString_Test(int degree)
        {
            Polynomial polynomial = new Polynomial(degree);

            return polynomial.ToString();
        }

        [TestCase(-2)]
        public void Polynomial_IntDegree_ThrowsArgumentOutOfRangeException(int degree)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Polynomial(degree));
        }
       
        [TestCase(new double[] { 1, 3, 7, 9, -3, 8.32546, 1234, 0, 0, 3, 5, 7, 1 },
                  new double[] { 7, 3, 5, 9, 5.9, 2, 0, 32, 4567, 23, 09, 0d, 0, 0, 0, 4, 0 },
            ExpectedResult = "8x^0 + 6x^1 + 12x^2 + 18x^3 + 2,9x^4 + 10,32546x^5 + 1234x^6 + 32x^7 + 4567x^8 + 26x^9 + 14x^10 + 7x^11 + x^12 + 4x^15")]       
        public string Polynomial_Addition_Test(double[] leftCoeffs, double[] rightCoeffs)
        {
            Polynomial a = new Polynomial(leftCoeffs);
            Polynomial b = new Polynomial(rightCoeffs);

            return (a + b).ToString();
        }

        [TestCase(new double[] { 1, 3, 7, 9, -3, 8.32546, 1234, 0, 0, 3, 5, 7, 1 },
                  new double[] { 7, 3, 5, 9, 5.9, 2, 0, 32, 4567, 23, 09, 0d, 0, 0, 0, 4, 0 },
            ExpectedResult = "-6x^0 + 2x^2 + -8,9x^4 + 6,32546x^5 + 1234x^6 + -32x^7 + -4567x^8 + -20x^9 + -4x^10 + 7x^11 + x^12 + -4x^15")]
        public string Polynomial_Subtraction_Test(double[] leftCoeffs, double[] rightCoeffs)
        {
            Polynomial a = new Polynomial(leftCoeffs);
            Polynomial b = new Polynomial(rightCoeffs);

            return (a - b).ToString();
        }

        [TestCase(new double[] { 1, 3, 7, 9, -3, 8.32546 },
            new double[] { 7, 3, 9, 09, 0d, 0, 0, 0, 4, 0 },
            ExpectedResult = "7x^0 + 24x^1 + 67x^2 + 120x^3 + 96x^4 + 193,27822x^5 + 78,97638x^6 + 47,92914x^7 + 78,92914x^8 + 12x^9 + 28x^10 + 36x^11 + -12x^12 + 33,30184x^13")]
        public string Polynomial_Multiplication_Test(double[] leftCoeffs, double[] rightCoeffs)
        {
            Polynomial a = new Polynomial(leftCoeffs);
            Polynomial b = new Polynomial(rightCoeffs);

            return (a * b).ToString();
        }

        [TestCase(new double[] { 1, 2, 3 }, new double[] { 1, 2, 3 }, ExpectedResult = true)]
        [TestCase(new double[] { 4, 8, 5.8, 0, 12, 0, 0, 0 }, new double[] { 4, 8, 5.8, 0, 12 }, ExpectedResult = true)]
        [TestCase(new double[] { 1, 2 }, new double[] { 1, 2, 3 }, ExpectedResult = false)]
        public bool Polynomial_Equals_Test(double[] leftCoeffs, double[] rightCoeffs)
        {
            Polynomial a = new Polynomial(leftCoeffs);
            Polynomial b = new Polynomial(rightCoeffs);

            return a.Equals(b);
        }

        [Test]
        public void Polynomial_GetHashCode_Test()
        {
            Polynomial a = new Polynomial(1, 2, 3, 4);
            Polynomial b = new Polynomial(1, 2, 3, 4);
            Polynomial c = new Polynomial(1, 2, 3);

            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
            Assert.AreNotEqual(a.GetHashCode(), c.GetHashCode());
        }
    }
}
