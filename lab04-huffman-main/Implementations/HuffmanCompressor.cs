using System.Text;
using HuffmanCoding.DataStructures;

namespace HuffmanCoding.Implementations;

public class HuffmanCompressor : ICompressor
{
    private Dictionary<char, int> _frequencyTable;
    private Dictionary<char, string> _huffmanCodes;
    private HuffmanNode? _root;

    public HuffmanCompressor()
    {
        _frequencyTable = new Dictionary<char, int>();
        _huffmanCodes = new Dictionary<char, string>();
        _root = null;
    }

    public string Compress(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return string.Empty;
        }

        _frequencyTable = BuildFrequencyTable(text);
        _root = BuildHuffmanTree(_frequencyTable);
        _huffmanCodes = new Dictionary<char, string>();
        GenerateCodes(_root, "", _huffmanCodes);

        var compressed = new StringBuilder();
        foreach (var c in text)
        {
            compressed.Append(_huffmanCodes[c]);
        }

        return compressed.ToString();
    }

    public string Decompress(string compressedText)
    {
        if (string.IsNullOrEmpty(compressedText) || _root == null)
            return string.Empty;

        var result = new StringBuilder();
        var current = _root;

        foreach (char bit in compressedText)
        {
            current = bit == '0' ? current.Left : current.Right;

            if (current!.IsLeaf)
            {
                result.Append(current.Character);
                current = _root;
            }
        }

        return result.ToString();
    }

    private Dictionary<char, int> BuildFrequencyTable(string text)
    {
        var frequencies = new Dictionary<char, int>();
        foreach (char c in text)
        {
            if (frequencies.ContainsKey(c))
                frequencies[c]++;
            else
                frequencies[c] = 1;
        }
        return frequencies;
    }

    private HuffmanNode BuildHuffmanTree(Dictionary<char, int> frequencies)
    {
        var priorityQueue = new PriorityQueue<HuffmanNode, int>();

        foreach (var kvp in frequencies)
            priorityQueue.Enqueue(new HuffmanNode(kvp.Key, kvp.Value), kvp.Value);

        while (priorityQueue.Count > 1)
        {
            var left = priorityQueue.Dequeue();
            var right = priorityQueue.Dequeue();

            var parent = new HuffmanNode(null, left.Frequency + right.Frequency)
            {
                Left = left,
                Right = right
            };

            priorityQueue.Enqueue(parent, parent.Frequency);
        }

        return priorityQueue.Dequeue();
    }

    private void GenerateCodes(HuffmanNode? node, string code, Dictionary<char, string> codes)
    {
        if (node == null) return;

        if (node.IsLeaf && node.Character.HasValue)
        {
            codes[node.Character.Value] = code.Length > 0 ? code : "0";
            return;
        }

        GenerateCodes(node.Left, code + "0", codes);
        GenerateCodes(node.Right, code + "1", codes);
    }

    public Dictionary<char, string> GetHuffmanCodes()
    {
        return new Dictionary<char, string>(_huffmanCodes);
    }

    public Dictionary<char, int> GetFrequencyTable()
    {
        return new Dictionary<char, int>(_frequencyTable);
    }

    public HuffmanNode? GetHuffmanTree()
    {
        return _root;
    }

    public void Reset()
    {
        _frequencyTable.Clear();
        _huffmanCodes.Clear();
        _root = null;
    }
}
