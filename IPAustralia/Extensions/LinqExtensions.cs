namespace IPAustralia.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> collection) => 
            collection is null ? Enumerable.Empty<T>() : collection ;
    }
}
