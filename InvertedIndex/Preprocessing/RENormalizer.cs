using System.Text.RegularExpressions;

namespace InvertedIndex.Preprocessing;

internal partial class RENormalizer
{
    [GeneratedRegex("\\s+")]
    internal static partial Regex ExtraSpaceRemovel();

    [GeneratedRegex("[^a-zA-Z']")]
    internal static partial Regex NonLettersRemovel();
}