using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyPostDevTools.models
{
    public class ExtendedEnum
    {
        public static List<T> Values<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }

        public static int Amount<T>()
        {
            return Enum.GetValues(typeof(T)).Length;
        }
    }
}