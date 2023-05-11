namespace InvertedIndex.Indexing;
internal record InvertedIndexValue(int DocFreq, List<string> PostingList)
{
    internal static InvertedIndexValue GetInvertedIndexValue
       (IGrouping<string, (string Id, string stem)> group)
       => new(GetDocFreq(group), GetPostingList(group));

    private static List<string> GetPostingList
        (IGrouping<string, (string Id, string stem)> group)
        => group
            .Select(pair => pair.stem)
            .ToList();

    private static int GetDocFreq
        (IGrouping<string, (string Id, string stem)> group)
        => group.Count();
}