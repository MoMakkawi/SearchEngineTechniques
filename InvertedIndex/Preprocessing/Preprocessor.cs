using System.Text.RegularExpressions;

using InvertedIndex.BooleanOperations;

using Porter2Stemmer;

using StopWord;

namespace InvertedIndex.Preprocessing;

internal static class Preprocessor
{
    internal static IEnumerable<(string Stem, string Id)> PreprocesseDocuments(this ICollection<(string Id, string Content)> docs)
        => docs
        .Select(Tokenizer)
        .Select(StopWordRemovel)
        .Select(Normalizer)
        .SelectMany(Stemmer);

    internal static IEnumerable<string> QueryPreprocesse(this string query)
    {
        EnglishPorter2Stemmer stemmer = new();

        return query
        .Tokenizer()
        .Normalizer()
        .Stemmer(stemmer)
        .RemoveFirstLastTokenIfBoolOp();
    }

    private static (string Id, string Tokens) Tokenizer
        (this (string Id, string Content) doc) => (doc.Id, doc.Content.Tokenizer());

    private static string Tokenizer(this string docContent)
    {
        Regex reNonLetters = RENormalizer.NonLettersRemovel();
        Regex reExtraSpace = RENormalizer.ExtraSpaceRemovel();

        string tokens = docContent;
        tokens = reNonLetters.Replace(tokens, " ");
        tokens = reExtraSpace.Replace(tokens, " ");

        return tokens;
    }

    private static (string Id, string Terms) Normalizer
        (this (string Id, string Words) doc) => (doc.Id, doc.Words.Normalizer());

    private static string Normalizer
        (this string Words) => Words.ToLowerInvariant();

    private static (string Id, string Words) StopWordRemovel
        (this (string Id, string Tokens) doc)
    {
        string words = doc.Tokens.RemoveStopWords("en");

        return (doc.Id, words);
    }

    private static ICollection<(string Stem, string Id)> Stemmer(this (string Id, string Terms) doc)
    {
        EnglishPorter2Stemmer stemmer = new();

        return doc
            .Terms
            .Stemmer(stemmer)
            .Select(stem => (stem, doc.Id))
            .ToList();
    }

    private static ICollection<string> Stemmer(this string terms, EnglishPorter2Stemmer stemmer)
    {
        return terms
            .Split()
            .Select(term => stemmer.Stem(term).Value)
            .ToList();
    }

    private static ICollection<string> RemoveFirstLastTokenIfBoolOp
    (this ICollection<string> stems)
    {
        var BooleanOperations = Enum.GetValues(typeof(BooleanOperation));

        var operations = Enumerable
            .Range(0, BooleanOperations.Length)
            .Select(op => BooleanOperations.GetValue(op)?.ToString()?.ToLowerInvariant());

        if (stems.ToArray() is [var first, .., var last])
        {
            if (operations.Any(op => op == first))
                stems = stems.ToArray()[1..];
            if (operations.Any(op => op == last))
                stems = stems.ToArray()[..^1];
        }

        return stems;
    }
}