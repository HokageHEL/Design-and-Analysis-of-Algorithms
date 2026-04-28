namespace HashSetLab;

/// <summary>
/// A hash set implementation based on the .NET Framework 4.0 reference implementation.
/// Uses chaining for collision resolution and prime number sizing for optimal distribution.
/// </summary>
/// <remarks>
/// Reference: https://www.dotnetframework.org/default.aspx/4@0/4@0/DEVDIV_TFS/Dev10/Releases/RTMRel/ndp/fx/src/Core/System/Collections/Generic/HashSet@cs/1305376/HashSet@cs
/// </remarks>
public class CustomHashSet
{
    private struct Slot
    {
        internal int HashCode;    // Lower 31 bits of hash code, -1 if unused
        internal string? Value;   // The stored value
        internal int Next;        // Index of next slot in chain, -1 if last
    }

    // Constants
    private const int Lower31BitMask = 0x7FFFFFFF;

    // Fields
    private int[]? _buckets;      // Hash buckets (indices into _slots, stored as index+1, 0 means empty)
    private Slot[]? _slots;       // Storage for elements and collision chains
    private int _count;           // Number of elements in the set
    private int _lastIndex;       // Index of the last element in the slots array
    private int _freeList;        // Index of first free slot in free list, -1 if none

    public CustomHashSet()
    {
        _freeList = -1;
    }

    public int Count => _count;

    private void Initialize(int capacity)
    {
        int size = HashHelpers.GetPrime(capacity);
        _buckets = new int[size];
        _slots = new Slot[size];
        _freeList = -1;
    }

    private int InternalGetHashCode(string? item)
    {
        if (item == null)
            return 0;

        return item.GetHashCode() & Lower31BitMask;
    }

    public bool Add(string? value) => AddIfNotPresent(value);

    private bool AddIfNotPresent(string? value)
    {
        // Initialize on first add
        if (_buckets == null)
            Initialize(0);

        int hashCode = InternalGetHashCode(value);
        int bucket = hashCode % _buckets!.Length;

        // Check if item already exists in the collision chain
        for (int i = _buckets[bucket] - 1; i >= 0 && _slots != null; i = _slots[i].Next)
        {
            if (_slots[i].HashCode == hashCode && _slots[i].Value == value)
                return false; // Already exists
        }

        // Find a slot to insert into
        int index;
        if (_freeList >= 0)
        {
            // Use a slot from the free list
            index = _freeList;
            _freeList = _slots![index].Next;
        }
        else
        {
            // Check if we need to resize
            if (_lastIndex == _slots!.Length)
            {
                IncreaseCapacity();
                bucket = hashCode % _buckets!.Length;
            }

            index = _lastIndex;
            _lastIndex++;
        }

        // Insert the item at the head of the bucket's chain
        _slots![index].HashCode = hashCode;
        _slots[index].Value = value;
        _slots[index].Next = _buckets[bucket] - 1;
        _buckets[bucket] = index + 1;
        _count++;

        return true;
    }

    public bool Contains(string? item)
    {
        if (_buckets == null)
            return false;

        int hashCode = InternalGetHashCode(item);
        int bucket = hashCode % _buckets.Length;

        for (int i = _buckets[bucket] - 1; i >= 0 && _slots != null; i = _slots[i].Next)
        {
            if (_slots[i].HashCode == hashCode && _slots[i].Value == item)
                return true;
        }

        return false;
    }

    public bool Remove(string? item)
    {
        if (_buckets == null)
            return false;

        int hashCode = InternalGetHashCode(item);
        int bucket = hashCode % _buckets.Length;
        int last = -1;

        // Walk the collision chain to find the item
        for (int i = _buckets[bucket] - 1; i >= 0; last = i, i = _slots![i].Next)
        {
            if (_slots![i].HashCode == hashCode && _slots[i].Value == item)
            {
                // Remove from chain
                if (last < 0)
                {
                    // First item in chain
                    _buckets[bucket] = _slots[i].Next + 1;
                }
                else
                {
                    // Not first item
                    _slots[last].Next = _slots[i].Next;
                }

                // Add to free list
                _slots[i].HashCode = -1;
                _slots[i].Value = null;
                _slots[i].Next = _freeList;
                _freeList = i;
                _count--;

                return true;
            }
        }

        return false;
    }

    private void IncreaseCapacity()
    {
        int newSize = HashHelpers.ExpandPrime(_count);
        SetCapacity(newSize);
    }

    private void SetCapacity(int newSize)
    {
        var newBuckets = new int[newSize];
        var newSlots = new Slot[newSize];

        if (_slots != null)
        {
            Array.Copy(_slots, newSlots, _lastIndex);
        }

        // Rehash all items
        for (int i = 0; i < _lastIndex; i++)
        {
            int bucket = newSlots[i].HashCode % newSize;
            newSlots[i].Next = newBuckets[bucket] - 1;
            newBuckets[bucket] = i + 1;
        }

        _buckets = newBuckets;
        _slots = newSlots;
    }

    public void Clear()
    {
        if (_lastIndex > 0)
        {
            Array.Clear(_slots!, 0, _lastIndex);
            Array.Clear(_buckets!, 0, _buckets!.Length);
            _lastIndex = 0;
            _count = 0;
            _freeList = -1;
        }
    }

    public int GetCapacity() => _buckets?.Length ?? 0;

    public double GetLoadFactor()
    {
        int capacity = GetCapacity();
        return capacity == 0 ? 0 : (double)_count / capacity;
    }

    public CustomHashSet Intersection(CustomHashSet other)
    {
        var result = new CustomHashSet();

        if (_slots != null)
        {
            for (int i = 0; i < _lastIndex; i++)
            {
                if (_slots[i].HashCode >= 0 && _slots[i].Value != null && other.Contains(_slots[i].Value))
                    result.Add(_slots[i].Value);
            }
        }

        return result;
    }

    public string[] ToArray()
    {
        if (_count == 0)
            return Array.Empty<string>();

        var result = new string[_count];
        int index = 0;

        if (_slots != null)
        {
            for (int i = 0; i < _lastIndex && index < _count; i++)
            {
                if (_slots[i].HashCode >= 0 && _slots[i].Value != null)
                {
                    result[index++] = _slots[i].Value!;
                }
            }
        }

        return result;
    }
}
