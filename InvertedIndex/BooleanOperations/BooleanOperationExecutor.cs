using InvertedIndex.Indexing;
using InvertedIndex.Preprocessing;

namespace InvertedIndex.BooleanOperations;

internal static class BooleanOperationExecutor
{
    internal static List<string> ExecuteBooleanQuery
        (this Dictionary<string, InvertedIndexValue> invertedIndexes, string booleanQuery)
    {
        Console.WriteLine();
        Console.WriteLine("_____________ The input Boolean Query  ____________");
        Console.WriteLine($"Input Boolean Query : {booleanQuery}");

        var queryTerms = booleanQuery
            .QueryPreprocesse()
            .ToArray();

        Console.WriteLine();
        Console.WriteLine("_____________ The Boolean Query After Preprocessing ____________");
        Console.WriteLine("Modifed Boolean Query : " + string.Join(' ', queryTerms));

        var result = invertedIndexes[queryTerms.First()].PostingList;
        for (int i = 1; i < queryTerms.Length - 1; i += 2)
        {
            var postingList2 = invertedIndexes[queryTerms[i + 1]].PostingList;
            var operation = queryTerms[i];
            result = BooleanOperationHandler.Handler(result, operation, postingList2);
        }

        return result.Order().ToList();
    }

    internal static void PrintExecuteBooleanQueryResult
        (this List<string> postingList)
    {
        Console.WriteLine();
        Console.WriteLine("_____________ Boolean Query Result__________");
        Console.WriteLine("result : " + string.Join(", ", postingList));
    }
}