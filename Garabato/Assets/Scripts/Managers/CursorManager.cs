using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public bool showMouse;
    private void Start()
    {
        LockCursor();
    }

    private void Update()
    {
        if (GameManager.Instance.IsPaused || showMouse)
        {
            UnlockCursor();

        }
        else
        {
            LockCursor();
        }
    }


    public static void LockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    public static void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
