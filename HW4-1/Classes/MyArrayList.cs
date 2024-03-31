using System.Collections;
using HW4_1.Interfaces;

namespace HW4_1.Classes;

public sealed class MyArrayList<T> : IArrayBase<T>
    where T : IComparable<T>
{
    private T[] _array;

    private const int DefaultCapacity = 10;

    private int _size;

    public MyArrayList(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity);
        _array = new T[capacity];
        _size = 0;
    }

    public MyArrayList() : this(DefaultCapacity)
    {
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= _size)
            {
                throw new IndexOutOfRangeException("Index is out of range.");
            }

            return _array[index];
        }
        set
        {
            if (index < 0 || index >= _size)
            {
                throw new IndexOutOfRangeException("Index is out of range.");
            }

            _array[index] = value;
        }
    }

    public int Compare(T item1, T item2)
    {
        return item1.CompareTo(item2);
    }


    public T Min()
    {
        if (_size == 0)
        {
            return default(T);
        }

        T minItem = _array[0];
        for (int i = 1; i < _size; i++)
        {
            if (Compare(_array[i], minItem) < 0)
            {
                minItem = _array[i];
            }
        }

        return minItem;
    }

    public T Max()
    {
        if (_size == 0)
        {
            return default(T);
        }

        T maxItem = _array[0];
        for (int i = 1; i < _size; i++)
        {
            if (Compare(_array[i], maxItem) > 0)
            {
                maxItem = _array[i];
            }
        }

        return maxItem;
    }


    private void IncreaseCapacity()
    {
        var newSize = _array.Length * 2 + 1;
        Array.Copy(_array, _array, newSize);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new MyArrayListEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return new MyArrayListEnumerator(this);
    }

    public void Print()
    {
        for (int i = 0; i < _size; i++)
        {
            Console.Write(_array[i] + " ");
        }

        Console.WriteLine("");
    }

    public bool ConditionForOne(Func<T, bool> condition)
    {
        ArgumentNullException.ThrowIfNull(condition);
        return GetElementsWithCondition(condition).Length > 0;
    }

    public bool ConditionForAll(Func<T, bool> condition)
    {
        ArgumentNullException.ThrowIfNull(condition);
        return GetElementsWithCondition(condition).Length == _size;
    }

    public T GetFirstElWithCondition(Func<T, bool> condition)
    {
        ArgumentNullException.ThrowIfNull(condition);
        for (var i = 0; i < _size; i++)
        {
            if (condition(_array[i]))
            {
                return _array[i];
            }
        }

        throw new Exception("No elements found by provided condition");
    }

    public void ActionForAll(Func<T, T> action)
    {
        ArgumentNullException.ThrowIfNull(action);
        for (var i = 0; i < _size; i++)
        {
            action(_array[i]);
        }
    }

    public T[] GetElementsByIndex(int index)
    {
        if (index < 0 || index >= _size)
        {
            throw new IndexOutOfRangeException("Index is out of range.");
        }

        T[] result = new T[_size - index];
        Array.ConstrainedCopy(_array, index, result, 0, _size - index);
        return result;
    }

    public void Sort()
    {
        Array.Sort(_array, 0, _size);
    }

    public void Add(T item)
    {
        if (_size == _array.Length)
        {
            IncreaseCapacity();
        }

        _array[_size] = item;
        _size++;
    }

    public void Remove(int index)
    {
        if (index >= _size || index < 0)
        {
            throw new IndexOutOfRangeException("Index is out of range.");
        }

        Array.Copy(_array, index + 1, _array, index, _array.Length - (index + 1));
        _size--;
    }

    public void Remove(T item)
    {
        var index = Array.IndexOf(_array, item);
        if (index >= 0)
        {
            Remove(index);
        }
        else
        {
            throw new ArgumentException("No elements found");
        }
    }

    public int GetSize()
    {
        return _size;
    }

    public bool Contains(T item)
    {
        return Array.IndexOf(_array, item) != -1;
    }

    public void Reverse()
    {
        Array.Reverse(_array);
    }

    public int GetAmount(Func<T, bool> condition)
    {
        ArgumentNullException.ThrowIfNull(condition);
        return GetElementsWithCondition(condition).Length;
    }

    public T[] GetElementsWithCondition(Func<T, bool> condition)
    {
        ArgumentNullException.ThrowIfNull(condition);

        T[] newArray = new T[_size];
        var count = 0;
        for (var i = 0; i < _size; i++)
        {
            if (!condition(_array[i])) continue;
            newArray[count] = _array[i];
            count++;
        }

        Array.Resize(ref newArray, count);
        return newArray;
    }


    private class MyArrayListEnumerator : IEnumerator<T>
    {
        private MyArrayList<T> _array;

        private int _currentIndex;


        internal MyArrayListEnumerator(MyArrayList<T> array)
        {
            _array = array;
            _currentIndex = 0;
        }

        public bool MoveNext()
        {
            _currentIndex++;
            return _currentIndex < _array.GetSize();
        }

        public void Reset()
        {
            _currentIndex = -1;
        }

        public T Current
        {
            get
            {
                try
                {
                    return _array[_currentIndex];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}