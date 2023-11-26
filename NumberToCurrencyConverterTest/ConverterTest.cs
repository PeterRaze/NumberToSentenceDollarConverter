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

        [Theory]
        [InlineData("0", "zero dollars")]
        [InlineData("1", "one dollar")]
        [InlineData("2", "two dollars")]
        [InlineData("5", "five dollars")]
        [InlineData("7", "seven dollars")]
        public void Convert_One(string input, string expected)
        {
            string currency = Converter.ConvertNumberToSentece(input);
            Assert.Equal(expected, currency);
        }

        [Theory]
        [InlineData("10", "ten dollars")]
        [InlineData("26", "twenty-six dollars")]
        [InlineData("42", "forty-two dollars")]
        [InlineData("83", "eighty-three dollars")]
        [InlineData("91", "ninety-one dollars")]
        public void Convert_Ten(string input, string expected)
        {
            string currency = Converter.ConvertNumberToSentece(input);
            Assert.Equal(expected, currency);
        }

        [Theory]
        [InlineData("200", "two hundred dollars")]
        [InlineData("240", "two hundred forty dollars")]
        [InlineData("413", "four hundred thirteen dollars")]
        [InlineData("603", "six hundred three dollars")]
        [InlineData("780", "seven hundred eighty dollars")]
        public void Convert_Hundred(string input, string expected)
        {
            string currency = Converter.ConvertNumberToSentece(input);
            Assert.Equal(expected, currency);
        }

        [Theory]
        [InlineData("1 002", "one thousand two dollars")]
        [InlineData("3 000", "three thousand dollars")]
        [InlineData("4 150", "four thousand one hundred fifty dollars")]
        [InlineData("42 309", "forty-two thousand three hundred nine dollars")]
        [InlineData("732 015", "seven hundred thirty-two thousand fifteen dollars")]
        public void Convert_Thousand(string input, string expected)
        {
            string currency = Converter.ConvertNumberToSentece(input);
            Assert.Equal(expected, currency);
        }

        [Theory]
        [InlineData("1 000 020", "one million twenty dollars")]
        [InlineData("3 020 234", "three million twenty thousand two hundred thirty-four dollars")]
        [InlineData("50 000 000", "fifty million dollars")]
        [InlineData("66 301 233", "sixty-six million three hundred one thousand two hundred thirty-three dollars")]
        [InlineData("932 020 305", "nine hundred thirty-two million twenty thousand three hundred five dollars")]
        public void Convert_Million(string input, string expected)
        {
            string currency = Converter.ConvertNumberToSentece(input);
            Assert.Equal(expected, currency);
        }
    }
}