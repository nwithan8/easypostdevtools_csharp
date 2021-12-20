using System;
using System.Collections.Generic;

namespace EasyPostDevTools.utils
{
    public class Random
    {
        private static System.Random random = new System.Random();

        public static bool RandomBool
        {
            get => random.Next(0, 2) == 0;
        }

        public static int RandomIntInRange(int min, int max)
        {
            return random.Next(min, max);
        }

        public static int RandomInt
        {
            get => random.Next();
        }

        public static double RandomDoubleInRange(double min, double max)
        {
            return random.NextDouble() * (max - min) + min;
        }

        public static double RandomDouble
        {
            get => random.NextDouble();
        }

        public static float RandomFloatInRange(float min, float max)
        {
            return (float)(random.NextDouble() * (max - min) + min);
        }

        public static float RandomFloat
        {
            get => (float)random.NextDouble();
        }

        public static char RandomChar
        {
            get => (char)(random.Next(0, 26) + 'a');
        }

        public static string RandomStringOfLength(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }

        public static string RandomString
        {
            get => RandomStringOfLength(RandomIntInRange(3, 10));
        }

        public static List<object> RandomItemsFromList<T>(List<T> list, int amount, bool allowDuplicates)
        {
            if (!allowDuplicates && amount > list.Count)
            {
                throw new Exception("Amount must be less than or equal to list size when unique is true");
            }

            var items = new List<object>();
            for (int i = 0; i < amount; i++)
            {
                var item = list[RandomIntInRange(0, list.Count - 1)];
                items.Add(item);
                if (!allowDuplicates)
                {
                    list.Remove(item);
                }
            }

            return items;
        }

        public static object RandomItemFromList<T>(List<T> list)
        {
            var items = RandomItemsFromList<T>(list, 1, false);
            return items[0];
        }
    }
}