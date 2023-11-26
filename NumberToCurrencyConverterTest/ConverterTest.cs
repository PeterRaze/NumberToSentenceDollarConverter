using NumberToCurrencyConverter.Server.Core;

namespace NumberToCurrencyConverterTest
{
    public class ConverterTest
    {
        [Theory]
        [InlineData("1 0", "ten dollars")]
        [InlineData("1 000", "one thousand dollars")]
        [InlineData("990 000", "nine hundred ninety thousand dollars")]
        [InlineData("10 000, 01", "ten thousand dollars and one cent")]
        public void Convert_Blank_Spaces(string input, string expected)
        {
            string currency = Converter.ConvertNumberToSentece(input);
            Assert.Equal(expected, currency);
        }

        [Theory]
        [InlineData("10", "ten dollars")]
        [InlineData("1000", "one thousand dollars")]
        [InlineData("990000", "nine hundred ninety thousand dollars")]
        [InlineData("10000", "ten thousand dollars")]
        public void Convert_No_Blank_Spaces(string input, string expected)
        {
            string currency = Converter.ConvertNumberToSentece(input);
            Assert.Equal(expected, currency);
        }

        [Theory]
        [InlineData("0,1", "zero dollars and ten cents")]
        [InlineData("495,27", "four hundred ninety-five dollars and twenty-seven cents")]
        [InlineData("999 999 999,99", "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents")]
        [InlineData("34000,01", "thirty-four thousand dollars and one cent")]
        public void Convert_With_Cents(string input, string expected)
        {
            string currency = Converter.ConvertNumberToSentece(input);
            Assert.Equal(expected, currency);
        }

        [Theory]
        [InlineData("0", "zero dollars")]
        [InlineData("1", "one dollar")]
        [InlineData("25,1", "twenty-five dollars and ten cents")]
        [InlineData("0,01", "zero dollars and one cent")]
        [InlineData("45 100", "forty-five thousand one hundred dollars")]
        [InlineData("999 999 999,9", "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety cents")]
        public void Convert_Misc(string input, string expected)
        {
            string currency = Converter.ConvertNumberToSentece(input);
            Assert.Equal(expected, currency);
        }

    }
}