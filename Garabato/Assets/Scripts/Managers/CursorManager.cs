using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public bool showMouse = false;

    private void Start()
    {
        ApplyCursorState();
    }

    private void Update()
    {
        ApplyCursorState();
    }

    private void ApplyCursorState()
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
