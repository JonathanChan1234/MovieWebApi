using System;

namespace NetApi.Utils
{
    public static class StringConversionUtils
    {
        public static int stringToInt(string stringNumber)
        {
            if (string.IsNullOrEmpty(stringNumber)) return 0;
            try
            {
                if (Int32.Parse(stringNumber) > 0) return Int32.Parse(stringNumber);
                else throw new ArgumentException("Invalid integer");
            }
            catch (FormatException)
            {
                throw;
            }
        }
    }
}