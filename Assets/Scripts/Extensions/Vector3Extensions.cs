using UnityEngine;

public static class Vector3Extensions
{
    public static Vector3 GetRandomShift(this Vector3 vector3, float offset)
    {
        float xOffset = Random.value * offset * 2 - offset;
        float yOffset = Random.value * offset * 2 - offset;
        return new Vector3()
        {
            x = vector3.x + xOffset,
            y = vector3.y + yOffset,
            z = vector3.z
        };
    }
}

