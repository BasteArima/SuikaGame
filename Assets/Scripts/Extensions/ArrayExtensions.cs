using System;
using System.Collections.Generic;
using System.Linq;

public static class ArrayExtensions
{
    public static IEnumerable<T> ToEnumerable<T>(this Array target)
    {
        foreach (var item in target)
            yield return (T)item;
    }

    public static T GetRandomElement<T>(this T[] array)
    {
        int random = UnityEngine.Random.Range(0, array.Length);
        return array[random];
    }

    public static T[] GetRandomElements<T>(this T[] array, int count)
    {
        if (array.Length < count)
            throw new ArgumentException($"Count less then array length. Count: {count}, Lenght: {array.Length}");
        T[] randoms = new T[count];
        var heap = array.ToList();
        for (int i = 0; i < count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, heap.Count);
            randoms[i] = heap[randomIndex];
            heap.Remove(heap[randomIndex]);
        }
        return randoms;
    }
}

