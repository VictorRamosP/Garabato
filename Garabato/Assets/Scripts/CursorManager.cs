using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private void Start()
    {
        LockCursor();
    }

    private void Update()
    {
        if (GameManager.Instance.IsPaused)
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
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public static void UnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
    }
}
