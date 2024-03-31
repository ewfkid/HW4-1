namespace HW4_1.Interfaces;

internal interface IArrayBase<T> : IEnumerable<T>
{
    void Add(T item);

    void Remove(int index);

    void Remove(T element);

    int GetSize();

    bool Contains(T item);

    void Reverse();

    int GetAmount(Func<T, bool> condition);

    T[] GetElementsWithCondition(Func<T, bool> condition);

    void Print();

    bool ConditionForOne(Func<T, bool> condition);

    bool ConditionForAll(Func<T, bool> condition);

    T GetFirstElWithCondition(Func<T, bool> condition);

    void ActionForAll(Func<T, T> action);

    T[] GetElementsByIndex(int index);

    void Sort();

    T Max();

    T Min();
}