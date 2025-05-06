using UnityEngine;

public class TitleCycler : MonoBehaviour
{
    [Header("Títulos individuales")]
    [SerializeField] private Sprite title1;
    [SerializeField] private Sprite title2;
    [SerializeField] private Sprite title3;
    [SerializeField] private Sprite title4;
    [SerializeField] private Sprite title5;

    [SerializeField] private float changeInterval = 5f;

    private SpriteRenderer spriteRenderer;
    private Sprite[] titles;
    private int currentIndex = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Construir array ignorando nulos
        titles = new Sprite[] { title1, title2, title3, title4, title5 };
        titles = System.Array.FindAll(titles, s => s != null);

        if (titles.Length > 0)
        {
            spriteRenderer.sprite = titles[0];
            InvokeRepeating(nameof(ChangeTitle), changeInterval, changeInterval);
        }
        else
        {
            Debug.LogWarning("No hay títulos asignados.");
        }
    }

    void ChangeTitle()
    {
        currentIndex = (currentIndex + 1) % titles.Length;
        spriteRenderer.sprite = titles[currentIndex];
    }
}