namespace IPAustralia.Extensions
{
    public static class StringExtensions
    {
        public static bool IsMissing(this string value) => string.IsNullOrWhiteSpace(value);
    }
}
