using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool mapAnimationActivated = false;
    public bool playerIntroPlayed = false;
    public bool playerCanMove = false;
    public bool IsPaused { get; private set; } = false;

    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
  

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        IsPaused = !IsPaused;

        // Detener el tiempo general
        //Time.timeScale = IsPaused ? 0f : 1f;
    }

    
}
