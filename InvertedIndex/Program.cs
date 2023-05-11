using InvertedIndex.BooleanOperations;
using InvertedIndex.Indexing;
using InvertedIndex.Preprocessing;

using static InvertedIndex.Initializing.Initializer;

string query = "And Hi Or mAKKawi";

InitDocuments()
.PrintDocuments()
.PreprocesseDocuments()
.GetStemSequance()
.GetOrderedStemSequance()
.GetInvertedIndexes()
.PrintInvertedIndexes()
.ExecuteBooleanQuery(query)
.PrintExecuteBooleanQueryResult();