namespace ClinicApp.Models;

public class WaitingQueue<T>
{
    private readonly Queue<T> _items = new();

    public int Count => _items.Count;

    public bool IsEmpty => _items.Count == 0;

    public void Enqueue(T item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        _items.Enqueue(item);
    }

    public T Dequeue()
    {
        if (_items.Count == 0)
            throw new InvalidOperationException("Черга порожня: нікого приймати.");

        return _items.Dequeue();
    }

    public T Peek()
    {
        if (_items.Count == 0)
            throw new InvalidOperationException("Черга порожня: нікого показувати.");

        return _items.Peek();
    }

    public T[] ToArray()
    {
        return _items.ToArray();
    }
}