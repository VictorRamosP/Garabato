using UnityEngine;
using UnityEngine.UI;

public class UICursorController : MonoBehaviour
{
    [Header("Cursor Settings")]
    public RectTransform cursorImage;
    public Canvas canvas;
    public GameObject manoRaton;
    public float cursorScale = 2f;
    public bool limitMovement = true;
    public Rect movementBounds = new Rect(0, 0, Screen.width, Screen.height);

    public float cursorWidth;
    public float cursorHeight;

    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
    }

    void Start()
    {
        if (cursorImage != null)
        {
            cursorImage.localScale = Vector3.one * cursorScale;

        }
    }

    void Update()
    {
        if (InputManager.Instance.currentInputSource == InputManager.InputSource.Joystick)
        {
            manoRaton.SetActive(false);
            return;
        }

        manoRaton.SetActive(true);
        if (cursorImage == null || canvas == null) return;

        Vector2 screenMousePos = Input.mousePosition;

        if (limitMovement)
        {
            screenMousePos.x = Mathf.Clamp(screenMousePos.x, movementBounds.xMin, movementBounds.xMax);
            screenMousePos.y = Mathf.Clamp(screenMousePos.y, movementBounds.yMin, movementBounds.yMax);
        }

        Vector2 localCursorPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            screenMousePos,
            canvas.worldCamera,
            out localCursorPos
        );

        cursorImage.anchoredPosition = localCursorPos + new Vector2(cursorWidth / 2f, -cursorHeight / 2f);
    }
}
