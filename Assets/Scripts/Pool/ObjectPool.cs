using System.Collections.Generic;
using UnityEngine;

public sealed class ObjectPool<T> where T : MonoBehaviour
{
    private readonly Stack<T> _stack = new Stack<T>();
    private readonly T _prefab;

    public ObjectPool(T prefab)
    {
        _prefab = prefab;
    }

    public void Push(T objectView)
    {
        _stack.Push(objectView);
        objectView.gameObject.SetActive(false);
    }

    public T Pop()
    {
        T objectView;
        if (_stack.Count == 0)
        {
            objectView = GameObject.Instantiate(_prefab);
            objectView.name = _prefab.name;
        }
        else
        {
            objectView = _stack.Pop();
        }
        objectView.gameObject.SetActive(true);

        return objectView;
    }
}