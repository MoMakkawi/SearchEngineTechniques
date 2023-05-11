namespace InvertedIndex.Indexing;

internal static class Indexer
{
    internal static List<(string Stem, string Id)> GetStemSequance
        (this IEnumerable<(string Stem, string Id)> StemSequance)
        => StemSequance.ToList();

    internal static List<(string Stem, string Id)> GetOrderedStemSequance
        (this List<(string Stem, string Id)> StemSequance)
        => StemSequance
            .OrderBy(pair => pair.Stem)
            .ThenBy(pair => pair.Id)
            .ToList();

    internal static Dictionary<string, InvertedIndexValue> GetInvertedIndexes
        (this List<(string Stem, string Id)> OrderedStemSequance)
        => OrderedStemSequance
            .GroupBy(pair => pair.Stem)
            .ToDictionary(groub => groub.Key, InvertedIndexValue.GetInvertedIndexValue);

    internal static Dictionary<string, InvertedIndexValue> PrintInvertedIndexes
        (this Dictionary<string, InvertedIndexValue> InvertedIndexes)
    {
        Console.WriteLine("_____________ Inverted Indexes _______________");
        foreach (var InvertedIndex in InvertedIndexes)
        {
            Console.WriteLine(InvertedIndex.ConvertToString());
        }

        return InvertedIndexes;
    }

    internal static string ConvertToString
        (this KeyValuePair<string, InvertedIndexValue> InvertedIndex) =>
        $"stem = {InvertedIndex.Key} | " +
        $"DocFreq = {InvertedIndex.Value.DocFreq} | " +
        $"PostingList = {string.Join(", ", InvertedIndex.Value.PostingList)} ";
}