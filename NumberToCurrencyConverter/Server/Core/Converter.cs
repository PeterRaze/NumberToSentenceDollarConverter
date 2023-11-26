using System.Text.RegularExpressions;

namespace NumberToCurrencyConverter.Server.Core
{

    public static class Converter
    {
        #region Consts

        private static readonly IReadOnlyDictionary<string, string> numberToWord = new Dictionary<string, string>()
        {
            { "0", "zero" },
            { "1", "one" },
            { "2", "two" },
            { "3", "three" },
            { "4", "four" },
            { "5", "five" },
            { "6", "six" },
            { "7", "seven" },
            { "8", "eight" },
            { "9", "nine" },
            { "10", "ten" },
            { "11", "eleven" },
            { "12", "twelve" },
            { "13", "thirteen" },
            { "14", "fourteen" },
            { "15", "fifteen" },
            { "16", "sixteen" },
            { "17", "seventeen" },
            { "18", "eighteen" },
            { "19", "nineteen" },
            { "20", "twenty" },
            { "30", "thirty" },
            { "40", "forty" },
            { "50", "fifty" },
            { "60", "sixty" },
            { "70", "seventy" },
            { "80", "eighty" },
            { "90", "ninety" },
        };

        /*
         * Use this dict to add support to other large conventions such as Billion, Trillion, etc...
         * It is just necessary to add the name convention and the equivalent number of zeros.
         */
        private static readonly IReadOnlyDictionary<int, string> largeNameConventionBasedOnZeros = new Dictionary<int, string>()
        {
            { 3, "thousand" },
            { 6, "million" },
        };

        private const string _regexPattern = @"^[1-9]+([0-9]+)?(,{1}[0-9]{1,2}?|[0-9]+)$|^0,[0-9]{1,2}$|^[0-9]$";
        private const int _offset = 3;

        #endregion

        public static string ConvertNumberToSentece(string input)
        {

            input = input.Replace(" ", "");
            string cents = "";
            string dollars = input;
            int commaIndex = input.IndexOf(',');

            if (!IsInputValid(input))
            {
                throw new Exception("Invalid number entrance");
            }
            if (commaIndex != -1)
            {
                dollars = input[..commaIndex];
                cents = input[(commaIndex + 1)..];
                cents = ConvertCents(cents);
                if (!string.IsNullOrEmpty(cents))
                {
                    string centWord = cents == numberToWord["1"] ? "cent" : "cents";
                    cents = $" and {cents} {centWord}";
                }
            }

            int dollarLen = dollars.Length;

            if (dollarLen == 1)
            {
                dollars = ConvertOne(dollars);
            }
            else if (dollarLen == 2)
            {
                dollars = ConvertTen(dollars);
            }
            else if (dollarLen == 3)
            {
                dollars = ConvertHundred(dollars);
            }
            else
            {
                // Larger Numbers
                int zeros = dollars.Length - 1;
                int offsetChecker = 0;

                for (int i = zeros; i >= 0; i--)
                {
                    // Limit to check if the number is allowed
                    if (offsetChecker == _offset)
                    {
                        throw new Exception($"The larger number conversion supported is {largeNameConventionBasedOnZeros.MaxBy(x => x.Key).Value.ToUpper()}");
                    }
                    if (largeNameConventionBasedOnZeros.ContainsKey(i))
                    {
                        dollars = ConvertLarge(dollars, i);
                        break;
                    }
                    offsetChecker++;
                }
            }

            string dollarWord = dollars == numberToWord["1"] ? "dollar" : "dollars";

            return $"{dollars} {dollarWord}{cents}";
        }

        private static bool IsInputValid(string input)
        {
            if (string.IsNullOrEmpty(input)) return false;
            return Regex.IsMatch(input, _regexPattern);
        }

        private static string ConvertCents(string number)
        {
            if (number.Length == 1) number += "0";
            return ConvertTen(number);
        }

        private static string ConvertOne(string number)
        {
            return numberToWord[number];
        }

        private static string ConvertTen(string number)
        {
            var firstDigit = number.First();
            var secondDigit = number[1];

            if (firstDigit == '0')
            {
                return secondDigit == '0' ? "" : ConvertOne(secondDigit.ToString());
            }
            else if (firstDigit == '1' || secondDigit == '0')
            {
                return numberToWord[number];
            }
            else
            {
                string temp = $"{firstDigit}0";
                return $"{numberToWord[temp]}-{ConvertOne(secondDigit.ToString())}";
            }
        }

        private static string ConvertHundred(string number)
        {
            int zeros = 2;
            int index = number.Length - zeros;

            if (number.First() == '0')
                return ConvertTen(number[index..]);

            string prefix = $"{ConvertOne(number.First().ToString())} hundred";
            string sufix = $"{ConvertTen(number[index..])}";

            sufix = (!string.IsNullOrEmpty(sufix) && sufix.First() != ' ') ? sufix.Insert(0, " ") : sufix;

            return prefix + sufix;
        }

        private static string ConvertLarge(string number, int zeros)
        {
            int index = number.Length - zeros;

            string prefix = "";
            string sufix = "";

            if (!number[..index].All(x => x == '0'))
            {
                prefix = $"{GetPrefixByIndex(number, index)} {largeNameConventionBasedOnZeros[zeros]}";
            }

            if (zeros > _offset)
            {
                zeros -= _offset;
                sufix = $"{ConvertLarge(number[index..], zeros)}";
            }
            else
            {
                sufix = $"{ConvertHundred(number[index..])}";
            }

            sufix = (!string.IsNullOrEmpty(sufix) && sufix.First() != ' ') ? sufix.Insert(0, " ") : sufix;

            return prefix + sufix;
        }

        private static string GetPrefixByIndex(string number, int index)
        {
            switch (index)
            {
                case 1:
                    return $"{ConvertOne(number[..index])}";
                case 2:
                    return $"{ConvertTen(number[..index])}";
                case 3:
                    return $"{ConvertHundred(number[..index])}";
                default:
                    return "";
            }
        }
    }
}
