using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public bool showMouse = false;

    private void Start()
    {
        ApplyCursorState();
    }
    void Update()
    {
        if (GameManager.Instance.IsPaused)
        {
            UnlockCursor();
            return;
        }
        if (!GameManager.Instance.AllowCursorLock || showMouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ApplyCursorState()
    {
        if (!GameManager.Instance.AllowCursorLock || GameManager.Instance.IsPaused || showMouse)
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public static void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
