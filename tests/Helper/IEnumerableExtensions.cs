namespace Yadelib.Tests.Helper;

internal static class IEnumerableExtensions
{
    internal static bool None<T>(this IEnumerable<T> sequence) => !sequence.Any();
}