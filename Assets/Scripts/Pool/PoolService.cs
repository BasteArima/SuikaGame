using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PoolServices<T> : IEnumerable where T : MonoBehaviour
{
    private readonly Dictionary<string, ObjectPool<T>> _poolCache = new Dictionary<string, ObjectPool<T>>();

    public T Create(T gameObject)
    {
        if (!_poolCache.TryGetValue(gameObject.name, out ObjectPool<T> viewPool))
        {
            viewPool = new ObjectPool<T>(gameObject);
            _poolCache[gameObject.name] = viewPool;
        }

        return viewPool.Pop();
    }

    public void Destroy(T gameObject)
    {
        if (!_poolCache.TryGetValue(gameObject.name, out ObjectPool<T> viewPool))
        {
            viewPool = new ObjectPool<T>(gameObject);
            _poolCache[gameObject.name] = viewPool;
        }

        viewPool.Push(gameObject);
    }

    public IEnumerator GetEnumerator()
    {
        throw new System.NotImplementedException();
    }
}