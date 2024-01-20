using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public event Action EscapePressed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            EscapePressed?.Invoke();
    }
}
