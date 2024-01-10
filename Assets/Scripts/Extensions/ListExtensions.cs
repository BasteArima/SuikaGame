using System;
using System.Collections.Generic;

public static class ListExtensions
{
    public static T GetRandomElement<T>(this List<T> list)
    {
        int random = UnityEngine.Random.Range(0, list.Count);
        return list[random];
    }
    
    private static Random rng = new Random();  

    public static void Shuffle<T>(this IList<T> list)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  
    }
}

