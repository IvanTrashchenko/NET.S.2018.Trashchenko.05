using System;
using NUnit.Framework;

namespace StringExtensionLibrary.Tests
{
    [TestFixture]
    public class StringExtensionTests
    {
        [TestCase("0110111101100001100001010111111", 2, ExpectedResult = 934331071)]
        [TestCase("01101111011001100001010111111", 2, ExpectedResult = 233620159)]
        [TestCase("11101101111011001100001010", 2, ExpectedResult = 62370570)]       
        [TestCase("1AeF101", 16, ExpectedResult = 28242177)]
        [TestCase("1ACB67", 16, ExpectedResult = 1756007)]       
        [TestCase("764241", 8, ExpectedResult = 256161)]        
        [TestCase("10", 5, ExpectedResult = 5)]
        public int ConvertToDecimalTest_Success(string source, int @base)
        {
            return source.ConvertToDecimal(new Notation(@base));
        }

        [TestCase("987", 8)]
        [TestCase("97rt", 16)]
        [TestCase(null, 8)]
        [TestCase("", 16)]       
        [TestCase("SA123", 2)]
        [TestCase("1AeF101", 2)]
        public void ConvertToDecimalTest_ThrowArgumentException(string source, int @base)
        {
            Assert.Throws<ArgumentException>(() => source.ConvertToDecimal(new Notation(@base)));
        }

        [TestCase("764241", 20)]
        public void ConvertToDecimalTest_ThrowArgumentOutOfRangeException(string source, int @base)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => source.ConvertToDecimal(new Notation(@base)));
        }

        [TestCase("11111111111111111111111111111111", 2)]
         public void ConvertToDecimalTest_ThrowOverflowException(string source, int @base)
        {
            Assert.Throws<OverflowException>(() => source.ConvertToDecimal(new Notation(@base)));
        }
    }
}
