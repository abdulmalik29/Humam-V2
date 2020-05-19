using System;
using System.Security.Cryptography;

namespace myGame
{
    public static class Random_Number
    {
        private static readonly RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();

        public static int random_number_between(int minimumValue, int maximumValue)
        {
            byte[] randomNumber = new byte[1];

            random.GetBytes(randomNumber);

            double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);


            double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);

            // We need to add one to the range, to allow for the rounding done with Math.Floor
            int range = maximumValue - minimumValue + 1;

            double randomValueInRange = Math.Floor(multiplier * range);

            return (int)(minimumValue + randomValueInRange);
        }
    }
}

