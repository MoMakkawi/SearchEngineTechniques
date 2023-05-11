namespace InvertedIndex.Initializing;

internal static class Initializer
{
    internal static List<(string Id, string Content)> InitDocuments()
        => new()
        {
            ( "doc1", "Hello this is a test , makkawi"),
            ( "doc2", "Hello Dr.Marieam"),
            ( "doc3" , "Hi IR Sys")
        };

    internal static ICollection<(string Id, string Content)> PrintDocuments
    (this ICollection<(string Id, string Content)> docs)
    {
        Console.WriteLine("_________ Documents ______________");
        foreach ((string Id, string Content) in docs)
            Console.WriteLine("Id = " + Id + " | Content { " + Content + " } ");
        Console.WriteLine();

        return docs;
    }
}