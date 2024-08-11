namespace DigiShop.Base.Helpers
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value?.Trim());
        }

        public static bool IsNotNullOrEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static bool IsNotNullOrEmpty(this Guid? value)
        {
            return value.HasValue && value != Guid.Empty;
        }

        public static bool IsNullOrEmpty(this Guid? value)
        {
            return !value.HasValue || value == Guid.Empty;
        }

        public static string IsNull(this string value, string value2)
        {
            return value.IsNotNullOrEmpty() ? value : value2;
        }

        public static double IsNull(this double value, double value2)
        {
            return value != 0 ? value : value2;
        }

        public static int IsNull(this int value, int value2)
        {
            return value != 0 ? value : value2;
        }

        public static int ToInt(this string number, int defaultInt = 0)
        {
            if (int.TryParse(number, out int resultNum))
            {
                return resultNum;
            }

            return defaultInt;
        }
    }
}