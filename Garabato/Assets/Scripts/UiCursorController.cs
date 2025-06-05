using UnityEngine;
using UnityEngine.UI;

public class UICursorController : MonoBehaviour
{
    [Header("Cursor Settings")]
    public RectTransform cursorImage;       // Imagen del cursor en UI
    public Canvas canvas;                   // Canvas donde está
    public float cursorScale = 2f;          // Escala del cursor
    public bool limitMovement = false;      // Limitar movimiento
    public Rect movementBounds;             // Límite en pantalla (en píxeles)

    public float cursorWidth;
    public float cursorHeight;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;

        if (cursorImage != null)
        {
            cursorImage.localScale = Vector3.one * cursorScale;
            cursorImage.gameObject.SetActive(true);

            cursorWidth = cursorImage.rect.width * cursorScale;
            //cursorHeight = cursorImage.rect.height * cursorScale;
        }
    }
    void Update()
    {
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

    public void ShowSystemCursor()
    {
        Cursor.visible = true;
        if (cursorImage != null)
            cursorImage.gameObject.SetActive(false);
    }
}
