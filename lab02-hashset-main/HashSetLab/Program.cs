namespace HashSetLab;

class Program
{
    static void Main(string[] args)
    {
        TestHashSetAdd();
        TestHashSetDuplicates();
        TestHashSetContains();
        TestHashSetRemove();
        TestHashSetResize();

        Console.WriteLine(new string('─', 60));

        TestRemoveDuplicates();
        TestFindDuplicates();
        TestTextComparison();
    }

    static void TestHashSetAdd()
    {
        Console.WriteLine("Test: Add elements");
        var hashSet = new CustomHashSet();
        string[] words = { "apple", "banana", "cherry" };

        foreach (var word in words)
            hashSet.Add(word);

        Console.WriteLine($"Count: {hashSet.Count}, Capacity: {hashSet.GetCapacity()}, Load: {hashSet.GetLoadFactor():F2}");
        Console.WriteLine();
    }

    static void TestHashSetDuplicates()
    {
        Console.WriteLine("Test: Add duplicates");
        var hashSet = new CustomHashSet();
        hashSet.Add("apple");

        bool added1 = hashSet.Add("apple");
        bool added2 = hashSet.Add("banana");

        Console.WriteLine($"Add 'apple' again: {added1}, Add 'banana': {added2}");
        Console.WriteLine();
    }

    static void TestHashSetContains()
    {
        Console.WriteLine("Test: Contains");
        var hashSet = new CustomHashSet();
        hashSet.Add("apple");
        hashSet.Add("banana");

        Console.WriteLine($"Contains 'apple': {hashSet.Contains("apple")}");
        Console.WriteLine($"Contains 'grape': {hashSet.Contains("grape")}");
        Console.WriteLine();
    }

    static void TestHashSetRemove()
    {
        Console.WriteLine("Test: Remove");
        var hashSet = new CustomHashSet();
        hashSet.Add("apple");
        hashSet.Add("banana");

        bool removed1 = hashSet.Remove("banana");
        bool removed2 = hashSet.Remove("grape");

        Console.WriteLine($"Remove 'banana': {removed1}, Remove 'grape': {removed2}");
        Console.WriteLine($"Count after removal: {hashSet.Count}");
        Console.WriteLine();
    }

    static void TestHashSetResize()
    {
        Console.WriteLine("Test: Capacity increase");
        var hashSet = new CustomHashSet();

        Console.Write($"Initial capacity: {hashSet.GetCapacity()} → ");

        string[] words = { "grape", "honeydew", "kiwi", "lemon", "mango",
                          "nectarine", "orange", "papaya", "quince", "raspberry" };

        foreach (var word in words)
            hashSet.Add(word);

        Console.WriteLine($"After {words.Length} items: {hashSet.GetCapacity()}");
        Console.WriteLine();
    }


    static void TestRemoveDuplicates()
    {
        Console.WriteLine("Test: Remove duplicates");
        var detector = new DuplicateDetector();

        var unique = detector.RemoveDuplicates(TestData.DuplicateWords);

        Console.WriteLine($"Original: {TestData.DuplicateWords.Length} items");
        Console.WriteLine($"Unique: {unique.Length} items");
        Console.WriteLine($"Result: {string.Join(", ", unique)}");
        Console.WriteLine();
    }

    static void TestFindDuplicates()
    {
        Console.WriteLine("Test: Find duplicates");
        var detector = new DuplicateDetector();

        var duplicates = detector.FindDuplicates(TestData.WordsWithDuplicates);

        Console.WriteLine($"Found {duplicates.Length} duplicates: {string.Join(", ", duplicates)}");
        Console.WriteLine();
    }

    static void TestTextComparison()
    {
        Console.WriteLine("Test: Text comparison");
        var detector = new DuplicateDetector();

        var result = detector.CompareTexts(TestData.Text1, TestData.Text2);

        Console.WriteLine($"Text 1: {result.Text1UniqueWords} words");
        Console.WriteLine($"Text 2: {result.Text2UniqueWords} words");
        Console.WriteLine($"Common: {result.CommonWords} words");
        Console.WriteLine($"Similarity: {result.Similarity:F1}%");
        Console.WriteLine();
    }
}
