namespace HashSetLab;

public class DuplicateDetector
{
    private char[] _separators = new[] { ' ', ',', '.', '!', '?', ';', ':', '\n', '\r', '\t' };

    public string[] RemoveDuplicates(string[] words)
    {
        CustomHashSet uniqueSet = new CustomHashSet();
        foreach (string word in words)
        {
            uniqueSet.Add(word);
        }
        return uniqueSet.ToArray();
    }
    
    public string[] FindDuplicates(string[] words)
    {
        // Example:
        // Input: ["apple", "banana", "apple", "cherry", "banana", "apple"]
        // Output: ["apple", "banana"]
        var seen = new CustomHashSet();
        var duplicates = new CustomHashSet();

        foreach (string word in words)
        {
            if (seen.Contains(word))
                duplicates.Add(word);
            else
                seen.Add(word);
        }

        return duplicates.ToArray();
    }

    public void PrintDuplicateStats(string[] original, string[] unique)
    {
        Console.WriteLine($"Original array size: {original.Length}");
        Console.WriteLine($"Unique elements: {unique.Length}");
        Console.WriteLine($"Duplicates removed: {original.Length - unique.Length}");
        Console.WriteLine($"Duplicate percentage: {((original.Length - unique.Length) / (double)original.Length * 100):F1}%");
    }

    public CustomHashSet GetWordsFromText(string text)
    {
        CustomHashSet wordSet = new CustomHashSet();
        string[] words = text.Split(_separators, StringSplitOptions.RemoveEmptyEntries);
        foreach (string word in words)
        {
            wordSet.Add(word);
        }
        return wordSet;
    }

    public string[] FindCommonWords(string text1, string text2)
    {
        // Example:
        // Text 1: "The quick brown fox"
        // Text 2: "The lazy brown dog"
        // Output: ["the", "brown"]
        var words1 = GetWordsFromText(text1.ToLower());
        var words2 = GetWordsFromText(text2.ToLower());
        return words1.Intersection(words2).ToArray();
    }

    public TextComparisonResult CompareTexts(string text1, string text2)
    {
        // Example output:
        // Text 1 unique words: 25
        // Text 2 unique words: 30
        // Common words: 15
        // Similarity: 50.0%
        //
        // Common words:
        // the, and, is, of, to...
        var words1 = GetWordsFromText(text1.ToLower());
        var words2 = GetWordsFromText(text2.ToLower());
        var common = words1.Intersection(words2);

        int count1 = words1.Count;
        int count2 = words2.Count;
        int commonCount = common.Count;
        int union = count1 + count2 - commonCount;
        double similarity = union == 0 ? 0 : (double)commonCount / union * 100;

        return new TextComparisonResult
        {
            Text1UniqueWords = count1,
            Text2UniqueWords = count2,
            CommonWords = commonCount,
            Similarity = similarity,
            CommonWordsList = common.ToArray()
        };
    }
}

public class TextComparisonResult
{
    public int Text1UniqueWords { get; set; }
    public int Text2UniqueWords { get; set; }
    public int CommonWords { get; set; }
    public double Similarity { get; set; }
    public required string[] CommonWordsList { get; set; }
}