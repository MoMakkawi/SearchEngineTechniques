namespace InvertedIndex.BooleanOperations;

internal class BooleanOperationHandler
{
    internal static dynamic Handler
        (List<string> postingList1, string op, List<string> postingList2)
        => op.ToUpper() switch
        {
            nameof(BooleanOperation.AND) => AndHandler(postingList1, postingList2),
            nameof(BooleanOperation.OR) => ORHandler(postingList1, postingList2),
            _ => throw new NotImplementedException()
        };

    private static List<string> AndHandler
        (List<string> postingList1, List<string> postingList2)
        => postingList1
        .Intersect(postingList2)
        .ToList();

    private static List<string> ORHandler
        (List<string> postingList1, List<string> postingList2)
        => postingList1
        .Concat(postingList2)
        .ToList();

    private static List<string> NOTHandler
        (List<string> allPostingList, List<string> notPostingList)
        => allPostingList
        .Except(notPostingList)
        .ToList();
}