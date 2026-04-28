namespace HashSetLab;

public static class TestData
{
    public static readonly string[] DuplicateWords = new[]
    {
        "hello", "world", "hello", "test", "world", "test", "hello"
    };
    
    public static readonly string[] WordsWithDuplicates = new[]
    {
        "apple", "banana", "cherry", "apple", "date", "elderberry",
        "banana", "fig", "grape", "apple", "honeydew", "cherry",
        "kiwi", "lemon", "banana", "mango", "apple", "nectarine",
        "orange", "cherry", "papaya", "quince", "raspberry", "date"
    };
    
    public static readonly string Text1 = @"
        Hash tables are fundamental data structures in computer science.
        They provide efficient lookup, insertion, and deletion operations.
        A hash function maps keys to indices in an array.
        Collision resolution is essential when multiple keys hash to the same index.
        Common techniques include chaining and open addressing.
    ";
    
    public static readonly string Text2 = @"
        Data structures are the foundation of efficient algorithms.
        Hash tables use a hash function to compute array indices.
        They offer fast lookup and insertion capabilities.
        Handling collisions is crucial for maintaining performance.
        Popular methods are separate chaining and linear probing.
    ";

    public static readonly string Text3 = @"
        To be or not to be, that is the question.
        Whether it is nobler in the mind to suffer
        the slings and arrows of outrageous fortune,
        or to take arms against a sea of troubles.
    ";

    public static readonly string Text4 = @"
        It was the best of times, it was the worst of times.
        It was the age of wisdom, it was the age of foolishness.
        It was the season of light, it was the season of darkness.
    ";
}
