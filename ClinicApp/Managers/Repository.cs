using ClinicApp.Interfaces;

namespace ClinicApp.Managers;

public class Repository<T> where T : IIdentifiable
{
    private readonly List<T> _items = new();

    public int Count => _items.Count;

    public void Add(T item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        _items.Add(item);
    }

    // null, якщо елемент не знайдено
    public T? GetById(int id)
    {
        foreach (T item in _items)
        {
            if (item.Id == id)
                return item;
        }

        return default;
    }

    // true, якщо видалення вдалося
    public bool Remove(int id)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].Id == id)
            {
                _items.RemoveAt(i);
                return true;
            }
        }

        return false;
    }

    public T[] GetAll()
    {
        return _items.ToArray();
    }
}